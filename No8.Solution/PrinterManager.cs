using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using No8.Solution.Loggers;

namespace No8.Solution
{
    /// <summary>
    /// Class for managing printers
    /// </summary>
    public class PrinterManager
    {
        /// <summary>
        /// List of printers
        /// </summary>
        public List<Printer> Printers { get; }

        private readonly ILogger _logger;

        public PrinterManager(ILogger logger)
        {
            this._logger = logger;
            Printers = new List<Printer>();
        }


        /// <summary>
        /// Adds new printer to the list of printers
        /// </summary>
        /// <param name="newPrinter">Printer to add</param>
        public void Add(Printer newPrinter)
        {
            if (Printers.Contains(newPrinter))
            {
                _logger.Log($"Tried to add existing {newPrinter.Name} - {newPrinter.Model}");
                throw new ArgumentException($"Tried to add existing {newPrinter.Name} - {newPrinter.Model}");
            }

            Printers.Add(newPrinter);
            Register(newPrinter);

            _logger.Log($"Added {newPrinter.Name} - {newPrinter.Model}");
        }

        /// <summary>
        /// Prints the file of the input printer
        /// </summary>
        /// <param name="printer">Printer on which to print</param>
        /// <param name="filePath">Path of file to print</param>
        public void Print(Printer printer, string filePath)
        {
            if (printer == null)
            {
                _logger.Log("Tried to print on null printer");
                throw new ArgumentNullException($"{nameof(printer)} can't be null");
            }
            
            PrintCore(printer, filePath);
            
        }

        /// <summary>
        /// Subscribing on the event
        /// </summary>
        /// <param name="printer">The owner of the event</param>
        public void Register(Printer printer)
        {
            CheckInput(printer);
            printer.StartPrint += LogMessage;
            printer.EndPrint += LogMessage;
        }

        /// <summary>
        /// Unsubscribing on the event
        /// </summary>
        /// <param name="printer">The owner of the event</param>
        public void Unregister(Printer printer)
        {
            CheckInput(printer);
            printer.StartPrint -= LogMessage;
            printer.EndPrint -= LogMessage;
        }

        private void PrintCore(Printer printer, string filePath)
        {
            using (FileStream f = File.OpenRead(filePath))
                printer.Print(f);
        }


        private void LogMessage(object sender, TerminatorEventArgs eventArgs) => _logger.Log(eventArgs.Message);

        private void CheckInput(Printer printer)
        {
            if (printer == null)
                throw new ArgumentNullException($"{nameof(printer)} can't be null");
        }
    }
}
