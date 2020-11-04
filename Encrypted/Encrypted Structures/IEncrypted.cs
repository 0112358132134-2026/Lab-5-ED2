namespace Encrypted_Structures
{
    public interface IEncrypted
    {
        public string Cesar(string key, string message, int type);
        public string Zig_Zag(string key, string message);
        public string Decrypted_Zig_Zag(string key, string message, int originalLength);
        public string Route(string key, string message);
        public string DecryptedRoute(string key, string message, int originalLength);
    }
}