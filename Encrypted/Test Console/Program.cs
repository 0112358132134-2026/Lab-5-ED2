using System;
using Encrypted_Structures;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Encrypted ruta = new Encrypted();
            string re = ruta.Route("5,4", "este es un mensaje");
            Console.WriteLine(re);
            Console.WriteLine("--------------------------------------");
            string op = ruta.DecryptedRoute("5,4", re, 18);
            Console.WriteLine(op);
            Console.ReadLine();
                
            Console.WriteLine("Ejemplo Zig_Zag:");
            Console.WriteLine("--------------------------------------");
            Encrypted zig_zag = new Encrypted();
            string cifrado = zig_zag.Zig_Zag("5", "Cómo estás amigo");
            Console.WriteLine("Mensaje original: Cómo estás amigo");
            Console.WriteLine("Mensaje cifrado: " + cifrado);
            Console.WriteLine("--------------------------------------");
            string descifrado = zig_zag.Decrypted_Zig_Zag("5", cifrado, 16);
            Console.WriteLine("El mensaje descifrado es: " + descifrado);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("EJEMPLO CÉSAR_");
                Console.WriteLine("Cifrar = 1... Descifrar = 2");
                int option = int.Parse(Console.ReadLine());

                if (option == 1)
                {
                    Console.WriteLine("Ingrese el mensaje que desea cifrar:");
                    string aCifrar = Console.ReadLine();
                    Console.WriteLine("Ingrese la llave para cifrar:");
                    string key = Console.ReadLine();

                    Encrypted cesar = new Encrypted();
                    string result = cesar.Cesar(key, aCifrar, 1);

                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Su mensaje cifrado es:");
                    Console.WriteLine(result);

                    Console.WriteLine("---Presione cualquier tecla para continuar---");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (option == 2)
                {
                    Console.WriteLine("Ingrese el mensaje que desea descifrar:");
                    string aDescifrar = Console.ReadLine();
                    Console.WriteLine("Ingrese la llave para descifrar:");
                    string key = Console.ReadLine();
                    Encrypted cesar = new Encrypted();
                    string result = cesar.Cesar(key, aDescifrar, 2);

                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Su mensaje descifrado es:");
                    Console.WriteLine(result);

                    Console.WriteLine("---Presione cualquier tecla para continuar---");
                    Console.ReadKey();
                    Console.Clear();
                }

                Console.WriteLine("¿Desea continuar?");
                int continuar = int.Parse(Console.ReadLine());
                if (continuar == 0)
                {
                    exit = true;
                }
                Console.Clear();
            }
        }
    }
}