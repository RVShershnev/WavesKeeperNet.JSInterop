namespace WavesKeeperNet.JSInterop
{
    public interface IWavesKeeper
    {
        Task InitialPromise();
        ValueTask<Root> PublicState();
        ValueTask<string> EncryptMessage(string message, string publicKey, string appName);
        ValueTask<string> DecryptMessage(string message, string publicKey, string appName);
        Task<string> Auth(AuthData data);
        Task<string> SignTransaction(Transaction tx);
        Task<string> SignAndPublishTransaction(Transaction tx);
        ValueTask<bool> ResourceIsApproved();
        ValueTask<bool> ResourceIsBlocked();

        Task Notification(string title, string message);

        ValueTask<CustomData> SignCustomData(CustomData data);
        ValueTask<bool> VerifyCustomData(CustomData data);
    }
}