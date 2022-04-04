namespace WavesKeeperNet.JSInterop
{
    public class AuthDataResponce
    {
        /// <summary>
        /// A host that requested a signature.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The name of an application that requested a signature.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// a prefix participating in the signature.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// An address in Waves network.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The user's public key.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Signature.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// API version.
        /// </summary>
        public int Version { get; set; }
    }
}
