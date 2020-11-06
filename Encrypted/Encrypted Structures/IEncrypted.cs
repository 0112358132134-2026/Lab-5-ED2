namespace Encrypted_Structures
{
    public interface IEncrypted
    {
        public string Cesar(Key key, string message, int type);
        public string Zig_Zag(Key key, string message);
        public string Decrypted_Zig_Zag(Key key, string message, int originalLength);
        public string Route(Key key, string message);
        public string DecryptedRoute(Key key, string message, int originalLength);
    }
}