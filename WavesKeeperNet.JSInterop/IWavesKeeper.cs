namespace WavesKeeperNet.JSInterop
{
    public interface IWavesKeeper
    {
        Task InitialPromise();

        /// <summary>
        /// If a website is trusted, Waves Keeper public data are returned.
        /// </summary>
        /// <returns></returns>
        ValueTask<Root> PublicState();

        /// <summary>
        /// Encrypt string messages to account in Waves network. You need have recipient publicKey.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="publicKey"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        ValueTask<string> EncryptMessage(string message, string publicKey, string appName);

        /// <summary>
        /// Decrypt string messages from account in Waves network to you. You need have sender publicKey and encrypted message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="publicKey">Public key of sender.</param>
        /// <param name="appName">Application name.</param>
        /// <returns></returns>
        ValueTask<string> DecryptMessage(string message, string publicKey, string appName);

        /// <summary>
        /// This is a method for obtaining a signature of authorization data while verifying Waves' user. It works the same way as Web Auth API.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<AuthDataResponce> Auth(AuthData data);
        Task<string> SignTransaction(Transaction tx);
        Task<string> SignAndPublishTransaction(Transaction tx);
        ValueTask<bool> ResourceIsApproved();
        ValueTask<bool> ResourceIsBlocked();

        /// <summary>
        /// Send message to keeper. Ypu can send message only 1 time in 30 sec for trusted sites with send permission.
        /// </summary>
        /// <param name="title">string (20 chars max) (required field)</param>
        /// <param name="message">string (250 chars max) (optional field)</param>
        /// <returns></returns>
        Task Notification(string title, string? message);

        ValueTask<CustomData> SignCustomData(CustomData data);
        ValueTask<bool> VerifyCustomData(CustomData data);
    }
}