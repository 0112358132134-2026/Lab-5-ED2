using System;
using System.Collections.Generic;
using Encrypted_Structures;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {            
            bool exit = false;
            while (!exit)
            {
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
                    Console.WriteLine("Su mensaje cifrado es:");
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
