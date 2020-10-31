using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encrypted_Structures
{
    public class Encrypted
    {
        #region "Global_Variables"
        public Dictionary<string, string> alphabet = new Dictionary<string, string>();
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
    }
}