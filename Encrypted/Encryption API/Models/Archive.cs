﻿namespace Encryption_API.Models
{
    public class Archive
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }    
    }
}
