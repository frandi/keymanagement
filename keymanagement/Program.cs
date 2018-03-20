using System;
using System.Linq;

namespace keymanagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var counter = 0;

            int menu;
            do
            {
                menu = GetSelectedMenu(counter);
                switch (menu)
                {
                    case 1:
                        AddKey();
                        break;
                    case 2:
                        GetKey();
                        break;
                    case 3:
                        DeleteKey();
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }

                counter++;
            } while (menu > 0);
        }

        private static int GetSelectedMenu(int counter)
        {
            int[] validMenu = {0, 1, 2, 3};

            if (counter > 0)
                Console.WriteLine();

            Console.WriteLine("Available action:");
            Console.WriteLine("1. Add key");
            Console.WriteLine("2. Get key");
            Console.WriteLine("3. Delete key");
            Console.WriteLine("0. Exit");

            int selected;
            do
            {
                Console.Write("What do you want to do? ");    
                var s = Console.ReadLine();
                int.TryParse(s, out selected);
            } while (!validMenu.Contains(selected));

            return selected;
        }

        private static void AddKey()
        {
            Console.WriteLine();
            Console.WriteLine("-- Add new key to vault --");
            Console.Write("Key Name: ");
            var keyName = Console.ReadLine();
            Console.Write("Key Value: ");
            var keyValue = Console.ReadLine();

            try
            {
                var id = KeyManager.Add(keyName, keyValue);
                Console.WriteLine($"Key Id: {id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Oh snap, {ex.GetInnerExceptionMessage()}");
            }
        }

        private static void GetKey()
        {
            Console.WriteLine();
            Console.WriteLine("-- Get a key value from vault --");
            Console.Write("Key Name: ");
            var keyName = Console.ReadLine();

            try
            {
                var keyValue = KeyManager.Get(keyName);
                Console.WriteLine($"Key Value: {keyValue}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Oh snap, {ex.GetInnerExceptionMessage()}");
            }
        }

        private static void DeleteKey()
        {
            Console.WriteLine();
            Console.WriteLine("-- Delete a key value from vault --");
            Console.Write("Key Name: ");
            var keyName = Console.ReadLine();

            try
            {
                var recoveryId = KeyManager.Delete(keyName);
                Console.WriteLine($"Recovery Id: {recoveryId}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Oh snap, {ex.GetInnerExceptionMessage()}");
            }
        }
    }
}
