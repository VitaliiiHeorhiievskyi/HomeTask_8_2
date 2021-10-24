using System;

namespace HomeTask8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //D:/Users/vital/source/repos/HomeTask2/input.txt
                var Input = new InputStorageFromConsole();

                Storage storage1 = new Storage();
                Input.StartStorage(storage1);

                Storage storage2 = new Storage();
                Input.StartStorage(storage2);

                Console.WriteLine("\n\nJoint Products\n\n");

                foreach (var item in storage1.GetJointProducts(storage2))
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\n\n Products Not Contains in Other storage\n\n");

                foreach (var item in storage1.GetProductsNotContainsOther(storage2))
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\n\nDifferent Products\n\n");

                foreach (var item in storage1.GetDifferentProducts(storage2))
                {
                    Console.WriteLine(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("\nEnd program\n");
            }

        }
    }
}
