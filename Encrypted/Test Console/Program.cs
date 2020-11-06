using System;
using Encrypted_Structures;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Key key = new Key
            {
                Rows = 5,
                Columns = 4,
                Levels = 5
            };

            Encrypted ruta = new Encrypted();
            Console.WriteLine("Ejemplo Ruta (Vertical):");
            Console.WriteLine("--------------------------------------");
            string cifradoRuta = ruta.Route(key, "este es un mensaje");
            Console.WriteLine(cifradoRuta);
            Console.WriteLine("--------------------------------------");
            string descifradoRuta = ruta.DecryptedRoute(key, cifradoRuta, 18);
            Console.WriteLine(descifradoRuta);
            Console.ReadLine();
                
            Console.WriteLine("Ejemplo Zig_Zag:");
            Console.WriteLine("--------------------------------------");
            Encrypted zig_zag = new Encrypted();
            string cifradoZigZag = zig_zag.Zig_Zag(key, "Cómo estás amigo");
            Console.WriteLine("Mensaje original: Cómo estás amigo");
            Console.WriteLine("Mensaje cifrado: " + cifradoZigZag);
            Console.WriteLine("--------------------------------------");
            string descifradoZigZag = zig_zag.Decrypted_Zig_Zag(key, cifradoZigZag, 16);
            Console.WriteLine("El mensaje descifrado es: " + descifradoZigZag);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("EJEMPLO CÉSAR_");
                Console.WriteLine("Cifrar = 1... Descifrar = 2");
                int opción = int.Parse(Console.ReadLine());

                if (opción == 1)
                {
                    Console.WriteLine("Ingrese el mensaje que desea cifrar:");
                    string aCifrar = Console.ReadLine();
                    Console.WriteLine("Ingrese la llave para cifrar:");
                    string llave = Console.ReadLine();
                    key.Word = llave;

                    Encrypted cesar = new Encrypted();
                    string cifradoCesar = cesar.Cesar(key, aCifrar, 1);

                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Su mensaje cifrado es:");
                    Console.WriteLine(cifradoCesar);

                    Console.WriteLine("---Presione cualquier tecla para continuar---");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (opción == 2)
                {
                    Console.WriteLine("Ingrese el mensaje que desea descifrar:");
                    string aDescifrar = Console.ReadLine();
                    Console.WriteLine("Ingrese la llave para descifrar:");
                    string descifrar = Console.ReadLine();
                    key.Word = descifrar;
                    Encrypted cesar = new Encrypted();
                    string descifradoCesar = cesar.Cesar(key, aDescifrar, 2);

                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Su mensaje descifrado es:");
                    Console.WriteLine(descifradoCesar);

                    Console.WriteLine("---Presione cualquier tecla para continuar---");
                    Console.ReadKey();
                    Console.Clear();
                }

                Console.WriteLine("¿Desea continuar?");
                int continuar = int.Parse(Console.ReadLine());
                if (continuar == 0)
                {
                    salir = true;
                }
                Console.Clear();
            }
        }
    }
}