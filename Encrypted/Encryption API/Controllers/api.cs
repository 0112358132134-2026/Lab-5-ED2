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
        Encrypted encrypted = new Encrypted();

        [HttpPost]
        [Route("cipher/{method}")]
        public async Task<ActionResult> Encryption([FromForm] IFormFile file, string method, [FromForm] Key key)
        {
            try
            {
                string fileName = file.FileName.Remove(file.FileName.Length - 4, 4);
                string extension = "", auxExtension = "";

                byte[] result = null;
                using (var memory = new MemoryStream())
                {
                    await file.CopyToAsync(memory);
                    byte[] byteArray = memory.ToArray();

                    string message = encrypted.BytesToString(byteArray);
                    string resultAux;

                    switch (method)
                    {
                        case "César":
                            resultAux = encrypted.Cesar(key, message, 1);
                            if (resultAux == "") return StatusCode(500);                            
                            result = encrypted.StringToBytes(resultAux);
                            extension = "compressedFile / csr";
                            auxExtension = ".csr";
                            break;
                        case "ZigZag":
                            resultAux = encrypted.Zig_Zag(key, message);
                            if (resultAux == "") return StatusCode(500);
                            result = encrypted.StringToBytes_MetaData(resultAux, message.Length);
                            extension = "compressedFile / zz";
                            auxExtension = ".zz";
                            break;
                        case "Ruta":
                            resultAux = encrypted.Route(key, message);
                            if (resultAux == "") return StatusCode(500);
                            result = encrypted.StringToBytes_MetaData(resultAux, message.Length);
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
            catch (System.Exception)
            {
                return StatusCode(500);                
            }            
        }

        [HttpPost]
        [Route("decipher")]
        public async Task<ActionResult> Decoded([FromForm] IFormFile file, [FromForm] Key key)
        {
            try
            {
                string extension = file.FileName.Substring(file.FileName.Length - 3, 3), method;
                if (extension == ".zz") method = "ZigZag"; else if (extension == ".rt") method = "Ruta"; else method = "César";

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
                        case "César":
                            message = encrypted.BytesToString(byteArray);
                            resultAux = encrypted.Cesar(key, message, 2);
                            if (resultAux == "") return StatusCode(500);
                            result = encrypted.StringToBytes(resultAux);
                            fileName = file.FileName.Remove(file.FileName.Length - 4, 4);
                            break;
                        case "ZigZag":
                            message = encrypted.BytesToString_MetaData(byteArray, originalLength);
                            resultAux = encrypted.Decrypted_Zig_Zag(key, message, originalLength[0]);
                            if (resultAux == "") return StatusCode(500);
                            result = encrypted.StringToBytes(resultAux);
                            fileName = file.FileName.Remove(file.FileName.Length - 3, 3);
                            break;
                        case "Ruta":
                            message = encrypted.BytesToString_MetaData(byteArray, originalLength);
                            resultAux = encrypted.DecryptedRoute(key, message, originalLength[0]);
                            if (resultAux == "") return StatusCode(500);
                            result = encrypted.StringToBytes(resultAux);
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
            catch (System.Exception)
            {
                return StatusCode(500);                
            }            
        }
    }
}