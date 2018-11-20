using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Console
{
    class Program
    {
        private static PrinterManager printerManager;
        private static bool isFinished = false;

        [STAThread]
        static void Main(string[] args)
        {
            printerManager = new PrinterManager();

            do
            {
                System.Console.WriteLine("Select your choice:");
                System.Console.WriteLine("1: Show printers");
                System.Console.WriteLine("2: Add new printer");
                System.Console.WriteLine("3: Print file");
                System.Console.WriteLine("4: Exit");

                var key = System.Console.ReadKey();

                System.Console.WriteLine();

                if (key.Key == ConsoleKey.D1)
                    ShowPrinters();
                if (key.Key == ConsoleKey.D2)
                    AddPrinter();
                if (key.Key == ConsoleKey.D3)
                    Print();
                if (key.Key == ConsoleKey.D4)
                    isFinished = true;

            } while (!isFinished);
            


        }

        private static void ShowPrinters()
        {
            System.Console.WriteLine();

            foreach (var printer in printerManager.Printers)
            {
                System.Console.WriteLine(printer);
            }

            System.Console.WriteLine();
        }

        private static void AddPrinter()
        {
            System.Console.WriteLine();

            System.Console.WriteLine("Select type of printer to add:");
            System.Console.WriteLine("1: Canon");
            System.Console.WriteLine("2: Epson");

            var key = System.Console.ReadKey();

            System.Console.WriteLine();
            if (key.Key == ConsoleKey.D1)
            {
                System.Console.WriteLine("Enter model of printer");
                string model = System.Console.ReadLine();
                printerManager.Add(new CanonPrinter(model));
            }
            if (key.Key == ConsoleKey.D2)
            {
                System.Console.WriteLine("Enter model of printer");
                string model = System.Console.ReadLine();
                printerManager.Add(new EpsonPrinter(model));
            }
            

            System.Console.WriteLine();
        }

        private static void Print()
        {
            System.Console.WriteLine();

            System.Console.WriteLine("Select type of printer on which to print");
            System.Console.WriteLine("1: Canon");
            System.Console.WriteLine("2: Epson");

            var key = System.Console.ReadKey();

            System.Console.WriteLine();
            if (key.Key == ConsoleKey.D1)
            {
                System.Console.WriteLine("Enter model of Canon printer");
                string model = System.Console.ReadLine();
                Printer printer = null;

                foreach (var p in printerManager.Printers)
                {
                    if (p.Model == model && p.Name == "Canon")
                        printer = p;
                }

                printerManager.Print(printer);
            }
            if (key.Key == ConsoleKey.D2)
            {
                System.Console.WriteLine("Enter model of Epson printer");
                string model = System.Console.ReadLine();
                Printer printer = null;

                foreach (var p in printerManager.Printers)
                {
                    if (p.Model == model && p.Name == "Epson")
                        printer = p;
                }

                printerManager.Print(printer);
            }
            

            System.Console.WriteLine();
        }
    }
}
