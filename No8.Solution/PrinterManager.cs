using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace No8.Solution
{
    /// <summary>
    /// Class for managing printers
    /// </summary>
    public class PrinterManager
    {
        /// <summary>
        /// Event for notifying abot start and end of the printing
        /// </summary>
        public event EventHandler<TerminatorEventArgs> OutputTerminator = delegate { };

        /// <summary>
        /// List of printers
        /// </summary>
        public List<Printer> Printers { get; }

        public PrinterManager() => Printers = new List<Printer>();

        /// <summary>
        /// Adds new printer to the list of printers
        /// </summary>
        /// <param name="newPrinter">Printer to add</param>
        public void Add(Printer newPrinter)
        {
            foreach (var printer in Printers)
            {
                if (printer.Equals(newPrinter))
                {
                    Console.WriteLine($"{nameof(newPrinter)} already exists");
                    Log($"Tried to add existing {newPrinter.Name} - {newPrinter.Model}");
                    return;
                }
            }

            Printers.Add(newPrinter);
            
            Log($"Added {newPrinter.Name} - {newPrinter.Model}");
        }

        /// <summary>
        /// Prints the file of the input printer
        /// </summary>
        /// <param name="printer">Printer on which to print</param>
        public void Print(Printer printer)
        {
            if (printer == null)
            {
                Log("Tried to print on null printer");
                throw new ArgumentNullException($"{nameof(printer)} can't be null");
            }

            if (!Printers.Contains(printer))
            {
                Log("Tried to print on non-existing printer");
                throw new ArgumentException($"{nameof(printer)} must exist");
            }
                
            printer.Register(this);
            OnOutputTerminator(new TerminatorEventArgs("Output started at " + DateTime.Now));
            Log($"Printing started on {printer.Name}-{printer.Model} printer");

            PrintCore(printer);

            Log($"Printing finished on {printer.Name}-{printer.Model} printer");
            OnOutputTerminator(new TerminatorEventArgs("Output finished at " + DateTime.Now));
            printer.Unregister(this);
        }
        
        protected void OnOutputTerminator(TerminatorEventArgs eventArgs)
        {
            OutputTerminator?.Invoke(this, eventArgs);
        }

        private void PrintCore(Printer printer)
        {
            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.ShowDialog();
                using (FileStream f = File.OpenRead(o.FileName))
                    printer.Print(f);
            }
        }

        private void Log(string s)
        {
            using (FileStream fs = new FileStream("log.txt", FileMode.Append))
            using (var sr = new StreamWriter(fs))
                sr.WriteLine(s);  
        }
    }
}
