using Microsoft.JSInterop;
using System.Dynamic;
using System.Text.Json;

namespace WavesKeeperNet.JSInterop
{
    public class WavesKeeper : IWavesKeeper, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        /// <summary>
        /// Subscribe to updates of the state.
        /// </summary>
        public event Action<Root> Update;           
        public WavesKeeper(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/WavesKeeperNet.JSInterop/WavesKeeperJSInterop.js").AsTask());
            // On();
            // InitialPromise();
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        public async void On()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("on");
        }

        [JSInvokable]
        public void OnUpdate(Root change)
        {
         
            Update?.Invoke(change);
        }
        
        public async Task InitialPromise()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("initialPromise");
        }

        /// <summary>
        /// If a website is trusted, Waves Keeper public data wiil be returned.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<Root> PublicState()
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<Root>("publicState");                 
            }
            catch
            {
                throw;
            }
           
        }


        /// <summary>
        /// Send message to Waves Keeper. 
        /// </summary>
        /// <remarks>
        /// You can send message only 1 time in 30 sec for trusted sites with send permission.
        /// </remarks>
        /// <param name="title">String, 20 chars max (required field).</param>
        /// <param name="message">String, 250 chars max (optional field).</param>
        /// <returns>Returns Promise.</returns>
        public async Task Notification(string title, string message) 
        {
            try
            {
                var module = await moduleTask.Value;
                var responce = await module.InvokeAsync<IDictionary<string, object>>("notification", title, message);
                object result;
                responce.TryGetValue("message", out result).ToString();
                if (result is not null)
                {
                    throw WavesKeeperException.Parse(result.ToString());
                }                 
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// You can encrypt string messages to account in Waves network. You need to have recipient publicKey.
        /// </summary>
        /// <remarks>
        /// WavesKeeper.encryptMessage(*string to encrypt*, *public key in base58 string*, *prefix is secret app string need for encoding*).
        /// </remarks>
        /// <param name="message">Message.</param>
        /// <param name="publicKey">Public key of sender.</param>
        /// <param name="app">Name of app.</param>
        /// <returns></returns>
        public async ValueTask<string> EncryptMessage(string message, string publicKey, string appName)
        {
            try
            {
                var module = await moduleTask.Value;                
                var responce = await module.InvokeAsync<IDictionary<string, object>>("encryptMessage", message, publicKey, appName);
                object result;
                responce.TryGetValue("message", out result).ToString();
                if (result is not null)
                {
                    throw WavesKeeperException.Parse(result.ToString());
                }
                responce.TryGetValue("result", out result);                
                return result.ToString();                
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// You can decrypt string messages from account in Waves network. You need to have sender publicKey and the encrypted message..
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="publicKey">Public key of sender.</param>
        /// <param name="app">Name of app.</param>
        /// <returns></returns>
        public async ValueTask<string> DecryptMessage(string message, string publicKey, string appName)
        {
            try
            {
                var module = await moduleTask.Value;               
                var responce = await module.InvokeAsync<IDictionary<string, object>>("decryptMessage", message, publicKey, appName);
                object result;
                responce.TryGetValue("message", out result).ToString();
                if (result is not null)
                {
                    throw WavesKeeperException.Parse(result.ToString());
                }
                responce.TryGetValue("result", out result);
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// This is a method for obtaining a signature of some arbitrary data. 
        /// If the the signature is valid, be sure that the given blockchain account belongs to that user.
        /// </summary>
        /// <returns></returns>
        public async Task<AuthDataResponce> Auth(AuthData data)         
        {
            try
            {// 2EDKCrb2K1RJLXdiCdpmSxGp9yAwpNvrLhzLxdDdD16XggxFSv5W9G1UG86Satr2Rd6vpX1txBFWnYZRr4RnP7KX
             // 4GNJiER6HidQqnzD2JzZtSLeRerLoxCC8YUDUmEeBYUoQgAzkbx5sFfAvZ2ZtKE9Sqa76sPEBnmAcVeRcG4py8Wi
                var module = await moduleTask.Value;
                dynamic d = new ExpandoObject();             
                dynamic json =  await module.InvokeAsync<object>("auth", data);
                dynamic config = System.Text.Json.JsonSerializer.Deserialize<ExpandoObject>(json);
                var ee = JsonSerializer.Serialize(config);
                var Responce = JsonSerializer.Deserialize<AuthDataResponce>(ee);             
                return Responce;
            }
            catch
            {
                throw new Exception("Auth is falled");
            }
        }

        /// <summary>
        /// A method for signing transactions. See Transaction Format https://docs.waves.tech/en/ecosystem/waves-keeper/transaction.
        /// </summary>
        /// <returns></returns>
        public async Task<string> SignTransaction(Transaction tx) 
        {
            try
            {
                var module = await moduleTask.Value;
                var responce = await module.InvokeAsync<IDictionary<string, object>>("signTransaction", tx);
                object result;
                responce.TryGetValue("message", out result).ToString();                
                if (result is not null)
                {                
                    throw WavesKeeperException.Parse(result.ToString());                    
                }
                responce.TryGetValue("result", out result);
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Похоже на signTransaction, но также отправляет транзакцию на блокчейн. Описание поддерживаемых транзакций см. в разделе Формат транзакций.
        /// </summary>
        /// <returns></returns>
        public async Task<string> SignAndPublishTransaction(Transaction tx) 
        {
            try
            {
                var module = await moduleTask.Value;           
                var responce = await module.InvokeAsync<IDictionary<string, object>>("signAndPublishTransaction", tx);
                object result;
                responce.TryGetValue("message", out result).ToString();
                if (result is not null)
                {
                    throw WavesKeeperException.Parse(result.ToString());
                }
                responce.TryGetValue("result", out result);
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txs"></param>
        /// <returns>A line containing the entire past transaction.</returns>
        public async Task<string> SignTransactionPackage(IEnumerable<Transaction> txs) 
        {
            try
            {
                var module = await moduleTask.Value;             
                var responce = await module.InvokeAsync<IDictionary<string, object>>("signTransactionPackage", txs);
                object result;
                responce.TryGetValue("message", out result).ToString();
                if (result is not null)
                {
                    throw WavesKeeperException.Parse(result.ToString());
                }
                responce.TryGetValue("result", out result);
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> SignOrder() 
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<string>("signOrder");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> SignAndPublishOrder() 
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<string>("signAndPublishOrder");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Метод Waves Keeper для подписания отмены ордера для матчера. В качестве входных данных он принимает объект, похожий на транзакцию, подобную этой:
        /// </summary>
        /// <returns>A data line for sending to the matcher.</returns>
        public async Task<string> SignCancelOrder() 
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<string>("signCancelOrder");
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Метод Waves Keeper для отмены ордера. Он работает идентично signCancelOrder, но также пытается отправить данные матчеру. Для API нужно знать также 2 поля: priceAsset и amountAsset ордера.
        /// </summary>
        /// <returns>Data that came from the matcher.</returns>
        public async Task<string> SignAndPublishCancelOrder() 
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<string>("signAndPublishCancelOrder");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Метод Waves Keeper для подписи типизированных данных, для подписания запросов на различные сервисы. В качестве входных данных он принимает объект, похожий на транзакцию, подобную этой:
        /// </summary>
        /// <returns>A line with a signature in base58.</returns>
        public async Task<string> SignRequest() 
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<string>("signRequest");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Метод Waves Keeper для подписи пользовательских данных для разных сервисов.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async ValueTask<CustomData> SignCustomData(CustomData data)
        {
            try
            {
                var module = await moduleTask.Value;              
                var responce = await module.InvokeAsync<JsonDocument>("signCustomData", data.Version, data.Binary);
                var f = JsonSerializer.Deserialize<CustomData>(responce.RootElement .ToString());
                //var f = responce.RootElement.Deserialize<CustomData>(typeof(CustomData));
                //responce.TryGetValue("message", out result).ToString();
                //if (result is not null)
                //{
                //    throw WavesKeeperException.Parse(result.ToString());
                //}
                //responce.TryGetValue("result", out result);
                
                return f;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Validate custom data.
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>true/false.</returns>
        public async ValueTask<bool> VerifyCustomData(CustomData data)
        {
            try
            {
                var module = await moduleTask.Value;

                var responce = await module.InvokeAsync<IDictionary<string, object>>("verifyCustomData", data.Version, data.Binary, data.PublicKey, data.Signature);
                object result;
                responce.TryGetValue("message", out result);
                if (result is not null)
                {
                    throw WavesKeeperException.Parse(result.ToString());
                }
                responce.TryGetValue("result", out result);
                if(result is not null)
                {
                    return Convert.ToBoolean(result.ToString());
                }
                throw new Exception("Value is null");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Check that user allowed your website to access Waves Keeper.
        /// </summary>
        /// <returns>true/false</returns>
        public async ValueTask<bool> ResourceIsApproved()
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<bool>("resourceIsApproved");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Check that user blocked your website in Waves Keeper.
        /// </summary>
        /// <returns>true/false</returns>
        public async ValueTask<bool> ResourceIsBlocked()
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<bool>("resourceIsBlocked");
            }
            catch
            {
                throw;
            }
        }
    }
}