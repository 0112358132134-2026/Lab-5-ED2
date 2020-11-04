using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Encrypted_Structures;
using Encryption_API.Models;
using System.Collections.Generic;

namespace Encryption_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class api : ControllerBase
    {
        Encrypted Encrypted = new Encrypted();

        [HttpPost]
        [Route("cipher/{method}/{key}")]
        public async Task<ActionResult> Encryption([FromForm] IFormFile file, string method, string key)
        {
            string fileName = file.FileName.Remove(file.FileName.Length - 4, 4);
            string extension = "", auxExtension = "";

            byte[] result = null;
            using (var memory = new MemoryStream())
            {
                await file.CopyToAsync(memory);
                byte[] byteArray = memory.ToArray();

                string message = Encrypted.BytesToString(byteArray); 
                string resultAux;

                switch (method)
                {
                    case "cesar":                        
                        resultAux = Encrypted.Cesar(key, message, 1);
                        result = Encrypted.StringToBytes(resultAux);
                        extension = "compressedFile / csr";
                        auxExtension = ".csr";
                        break;
                    case "zigzag":
                        resultAux = Encrypted.Zig_Zag(key, message);
                        result = Encrypted.StringToBytes_MetaData(resultAux, message.Length);
                        extension = "compressedFile / zz";
                        auxExtension = ".zz";
                        break;
                    case "ruta":
                        resultAux = Encrypted.Route(key, message);
                        result = Encrypted.StringToBytes_MetaData(resultAux, message.Length);
                        extension = "compressedFile / rt";
                        auxExtension = ".rt";
                        break;                    
                }               
            }
            Archive response = new Archive
            {
                Content = result,
                ContentType = extension,
                FileName = fileName
            };
            return File(response.Content, response.ContentType, response.FileName + auxExtension);
        }

        [HttpPost]
        [Route("decipher/{method}/{key}")]
        public async Task<ActionResult> Decoded([FromForm] IFormFile file, string method, string key)
        {
            string fileName = "";
            byte[] result = null;
            using (var memory = new MemoryStream())
            {
                await file.CopyToAsync(memory);
                byte[] byteArray = memory.ToArray();

                string message;
                string resultAux;
                List<int> originalLength = new List<int>();

                switch (method)
                {
                    case "cesar":
                        message = Encrypted.BytesToString(byteArray);
                        resultAux = Encrypted.Cesar(key, message, 2);
                        result = Encrypted.StringToBytes(resultAux);
                        fileName = file.FileName.Remove(file.FileName.Length - 4, 4);
                        break;
                    case "zigzag":
                        message = Encrypted.BytesToString_MetaData(byteArray, originalLength);
                        resultAux = Encrypted.Decrypted_Zig_Zag(key, message, originalLength[0]);
                        result = Encrypted.StringToBytes(resultAux);
                        fileName = file.FileName.Remove(file.FileName.Length - 3, 3);
                        break;
                    case "ruta":
                        message = Encrypted.BytesToString_MetaData(byteArray, originalLength);
                        resultAux = Encrypted.DecryptedRoute(key, message, originalLength[0]);
                        result = Encrypted.StringToBytes(resultAux);
                        fileName = file.FileName.Remove(file.FileName.Length - 3, 3);
                        break;
                }                
            }
            Archive response = new Archive
            {
                Content = result,
                ContentType = "compressedFile / txt",
                FileName = fileName
            };
            return File(response.Content, response.ContentType, response.FileName + ".txt");
        }
    }
}