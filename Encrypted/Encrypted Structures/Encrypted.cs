using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encrypted_Structures
{
    public class Encrypted : IEncrypted
    {
        #region "Global_Variables"
        public Dictionary<string, string> alphabet = new Dictionary<string, string>();
        public List<string>[] listsVector;
        #endregion       
        #region "César"
        public string Cesar(string key, string message, int type)
        {
            bool oka;
            if (type == 1) oka = FillDictionary(key, 1); else oka = FillDictionary(key, 2);

            if (oka)
            {
                StringBuilder encryptedMessage = new StringBuilder();
                for (int i = 0; i < message.Length; i++)
                {
                    if (alphabet.ContainsKey(message[i].ToString()))
                    {
                        encryptedMessage.Append(alphabet[message[i].ToString()]);
                    }
                    else if (alphabet.ContainsKey(message[i].ToString().ToUpper()))
                    {
                        string ok = alphabet[message[i].ToString().ToUpper()];
                        encryptedMessage.Append(ok.ToLower());
                    }
                    else
                    {
                        encryptedMessage.Append(message[i].ToString());
                    }
                }
                return encryptedMessage.ToString();
            }
            else
            {
                return "Is not a valid password...";
            }
        }
        public bool Repeated(string key)
        {
            bool result = false;
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (i != j)
                    {
                        if (key[i].ToString().ToUpper() == key[j].ToString().ToUpper())
                        {
                            result = true;
                        }
                    }                                  
                }                
            }
            return result;
        }
        public bool FillDictionary(string key, int type)
        {
            bool repeated = Repeated(key);
            if ((repeated) || (key.Length >= 14))
            {
                return false;
            }
            else
            {
                string[] aux = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                List<string> alphabetAux = aux.OfType<string>().ToList();

                List<string> listAux = new List<string>();
                for (int i = 0; i < key.Length; i++)
                {
                    listAux.Add(key[i].ToString().ToUpper());
                }

                for (int i = 0; i < alphabetAux.Count; i++)
                {
                    if (!listAux.Contains(alphabetAux[i]))
                    {
                        listAux.Add(alphabetAux[i]);
                    }
                }

                if (type == 1)
                {
                    for (int i = 0; i < alphabetAux.Count; i++)
                    {
                        alphabet.Add(alphabetAux[i], listAux[i]);
                    }
                }
                else if (type == 2)
                {
                    for (int i = 0; i < alphabetAux.Count; i++)
                    {
                        alphabet.Add(listAux[i], alphabetAux[i]);
                    }
                }                
                return true;
            }            
        }
        #endregion
        #region "Zig_Zag"
        public string Zig_Zag(string key, string message)
        {
            int keyAux = int.Parse(key);
            if (keyAux >= 2)
            {
                listsVector = new List<string>[keyAux];
                for (int i = 0; i < listsVector.Length; i++)
                {
                    listsVector[i] = new List<string>(); 
                }

                StringBuilder messageAux = new StringBuilder();
                messageAux.Append(message);

                int textLength = (keyAux * 2) - 2;

                while (messageAux.Length > 0)
                {
                    for (int i = 0; i < textLength / 2; i++)
                    {
                        if (messageAux.Length != 0)
                        {
                            listsVector[i].Add(messageAux.ToString(0, 1).ToString());
                            messageAux.Remove(0, 1);
                        }
                        else
                        {
                            listsVector[i].Add("$");
                        }
                    }
                    int counter = keyAux - 1;
                    for (int i = 0; i < textLength / 2; i++)
                    {
                        if (messageAux.Length != 0)
                        {
                            listsVector[counter].Add(messageAux.ToString(0, 1).ToString());
                            messageAux.Remove(0, 1);
                        }
                        else
                        {
                            listsVector[counter].Add("$");
                        }
                        counter--;
                    }
                }

                StringBuilder result = new StringBuilder();
                for (int i = 0; i < listsVector.Length; i++)
                {
                    for (int j = 0; j  < listsVector[i].Count; j ++)
                    {
                        result.Append(listsVector[i][j]);
                    }
                }
                return result.ToString();
            }
            else
            {
                return "The key must be a number greater than or equal to two...";
            }
        }
        public string Decrypted_Zig_Zag(string key, string message, int originalLength)
        {
            int keyAux = int.Parse(key);
            if (keyAux >= 2)
            {
                listsVector = new List<string>[keyAux];
                for (int i = 0; i < listsVector.Length; i++)
                {
                    listsVector[i] = new List<string>();
                }

                int numberQueues = message.Length / (2* (1 + (keyAux - 2)));
                int externalLevels = numberQueues;
                int middleLevels = 2 * numberQueues;

                StringBuilder messageAux = new StringBuilder();
                messageAux.Append(message);

                for (int i = 0; i < listsVector.Length; i++)
                {
                    if ((i == 0) || (i == (listsVector.Length - 1)))
                    {
                        for (int j = 0; j < externalLevels; j++)
                        {
                            listsVector[i].Add(messageAux.ToString(0, 1));
                            messageAux.Remove(0, 1);
                        }                       
                    }
                    else
                    {
                        for (int j = 0; j < middleLevels; j++)
                        {
                            listsVector[i].Add(messageAux.ToString(0, 1));
                            messageAux.Remove(0, 1);
                        }
                    }                    
                }
                             
                StringBuilder result = new StringBuilder();
                int auxCounter = 0;
                bool okey = false;

                for (int i = 0; i < originalLength; i++)
                {
                    result.Append(listsVector[auxCounter][0]);
                    listsVector[auxCounter].RemoveAt(0);
                    if ((auxCounter < (keyAux - 1)) && (!okey))
                    {
                        auxCounter++;
                        if (auxCounter == (keyAux - 1))
                        {
                            okey = true;
                        }
                    }
                    else
                    {
                        auxCounter--;
                        if (auxCounter == 0)
                        {
                            okey = false;
                        }
                    }                   
                }                
                return result.ToString();
            }
            else
            {
                return "The key must be a number greater than or equal to two...";
            }
        }
        #endregion
        #region "Ruta"
        public string Route(string key, string message)
        {
            string[] nxm = key.Split(",");
            int n = int.Parse(nxm[0]);
            int m = int.Parse(nxm[1]);
            string[,] matrix = new string[n,m];

            StringBuilder messageAuxiliar = new StringBuilder();
            messageAuxiliar.Append(message);

            StringBuilder result = new StringBuilder();

            bool allInserted = false;
            while (!allInserted)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (messageAuxiliar.Length != 0)
                        {
                            matrix[j, i] = messageAuxiliar.ToString(0, 1);
                            messageAuxiliar.Remove(0, 1);
                        }
                        else
                        {
                            matrix[j, i] = "$";
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        result.Append(matrix[i, j]);
                    }
                }
                if (messageAuxiliar.Length == 0)
                {
                    allInserted = true;
                }
            }
            return result.ToString();
        }
        public string DecryptedRoute(string key, string message, int originalLength)
        {
            string[] nxm = key.Split(",");
            int n = int.Parse(nxm[0]);
            int m = int.Parse(nxm[1]);
            string[,] matrix = new string[n, m];

            StringBuilder messageAuxiliar = new StringBuilder();
            messageAuxiliar.Append(message);

            StringBuilder result = new StringBuilder();

            bool allInserted = false;
            while (!allInserted)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (messageAuxiliar.Length != 0)
                        {
                            matrix[i, j] = messageAuxiliar.ToString(0, 1);
                            messageAuxiliar.Remove(0, 1);
                        }                        
                    }
                }
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (matrix[j, i] != null)
                        {
                            result.Append(matrix[j, i]);
                        }
                    }
                }
                if (messageAuxiliar.Length == 0)
                {
                    allInserted = true;
                }
            }

            int remove = result.Length - originalLength;
            result.Remove(result.Length - remove, remove);
            return result.ToString();
        }
        #endregion
        #region "Auxiliaries"
        public string BytesToString(byte[] byteArray)
        {
            char[] charrArray = new char[byteArray.Length];
            for (int i = 0; i < byteArray.Length; i++)
            {
                charrArray[i] = (char)byteArray[i];
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < charrArray.Length; i++)
            {
                result.Append(charrArray[i].ToString());
            }
            return result.ToString();
        }
        public byte[] StringToBytes(string message)
        {
            char[] charArray = message.ToCharArray();
            byte[] result = new byte[charArray.Length];
            for (int i = 0; i < charArray.Length; i++)
            {
                result[i] = (byte)charArray[i];
            }            
            return result;
        }

        public string BytesToString_MetaData(byte[] byteArray, List<int> listAux)
        {
            int numberOfBytes = (int)byteArray[0];
            List<int> decimals = new List<int>();
            for (int i = 1; i <= numberOfBytes; i++)
            {
                decimals.Add((int)byteArray[i]);
            }
            StringBuilder binaryAux = new StringBuilder();
            for (int i = 0; i < decimals.Count; i++)
            {
                string convert = ConvertDecimalToBinary(decimals[i]);
                StringBuilder convertAux = new StringBuilder();
                convertAux.Append(convert);

                int length = 8 - convertAux.Length;

                if (length < 8)
                {
                    for (int j = 0; j < length; j++)
                    {
                        convertAux.Insert(0, "0");
                    }
                }
                binaryAux.Append(convertAux.ToString());
            }
            listAux.Add(ConvertBinaryToDecimal(binaryAux.ToString()));

            char[] charrArray = new char[byteArray.Length - (1 + decimals.Count)];
            int counterAux = 0;
            for (int i = (1 + decimals.Count()); i < byteArray.Length; i++)
            {
                charrArray[counterAux] = (char)byteArray[i];
                counterAux++;
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < charrArray.Length; i++)
            {
                result.Append(charrArray[i].ToString());
            }
            return result.ToString();
        }
        public byte[] StringToBytes_MetaData(string message, int originalLength)
        {
            string binaryLength = ConvertDecimalToBinary(originalLength);
            string binaryLengthAux = LargeBinary(binaryLength);
            List<string> bytes = SeparateBytes(binaryLengthAux, 8);
            List<int> decimals = new List<int>();
            for (int i = 0; i < bytes.Count; i++)
            {
                decimals.Add(ConvertBinaryToDecimal(bytes[i]));
            }
            int bytesForMetadata = decimals.Count();

            char[] charArray = message.ToCharArray();
            byte[] result = new byte[charArray.Length + 1 + decimals.Count()];

            int counter = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (i == 0)
                {
                    result[i] = (byte)bytesForMetadata;
                }
                else if ((i > 0) && (i <= decimals.Count()))
                {
                    result[i] = (byte)decimals[0];
                    decimals.RemoveAt(0);
                }
                else
                {
                    result[i] = (byte)charArray[counter];
                    counter++;
                }
            }
            return result;
        }

        public int ConvertBinaryToDecimal(string binary)
        {
            int exponent = binary.Length - 1;
            int decimalNumber = 0;

            for (int i = 0; i < binary.Length; i++)
            {
                if (int.Parse(binary.Substring(i, 1)) == 1)
                {
                    decimalNumber += int.Parse(System.Math.Pow(2, double.Parse(exponent.ToString())).ToString());
                }
                exponent--;
            }
            return decimalNumber;
        }
        public string ConvertDecimalToBinary(int number)
        {
            string result = "";
            while (number > 0)
            {
                if (number % 2 == 0)
                {
                    result = "0" + result;
                }
                else
                {
                    result = "1" + result;
                }
                number = (int)(number / 2);
            }
            return result;
        }
        public List<string> SeparateBytes(string largeBinary, int length)
        {
            StringBuilder copy = new StringBuilder();
            copy.Append(largeBinary);
            List<string> result = new List<string>();
            bool OK = false;
            while (!OK)
            {
                if (copy.Length >= length)
                {
                    result.Add(copy.ToString(0, length));
                    copy.Remove(0, length);
                }
                else
                {
                    if (copy.Length > 0)
                    {
                        for (int i = copy.Length; i < length; i++)
                        {
                            copy.Append("0");
                        }
                        result.Add(copy.ToString());
                    }
                    OK = true;
                }
            }
            return result;
        }
        public string LargeBinary(string binary)
        {
            bool isDivisible = false;

            StringBuilder aux = new StringBuilder();
            aux.Append(binary);

            while (!isDivisible)
            {
                if (aux.Length % 8 == 0)
                {
                    isDivisible = true;
                }
                else
                {
                    aux.Insert(0, "0");
                }
            }
            return aux.ToString();
        }
        #endregion
    }
}