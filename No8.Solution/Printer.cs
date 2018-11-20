using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    /// <summary>
    /// Abstract class for printers
    /// </summary>
    public abstract class Printer
    {
        /// <summary>
        /// Model of a printer
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Name of a printer
        /// </summary>
        public abstract string Name { get;  }
        
        protected Printer(string model)
        {
            if (String.IsNullOrEmpty(model))
                throw new ArgumentException($"{nameof(model)} can't be null or empty");

            Model = model;
        }

        /// <summary>
        /// Prints the content of the file somehow
        /// </summary>
        /// <param name="fs">File to print</param>
        public virtual void Print(FileStream fs)
        {
            if (fs == null)
                throw new ArgumentNullException($"{nameof(fs)} can't be null");

            PrintSimulation(fs);
        }

        /// <summary>
        /// Abstract method for printing fole content logic
        /// </summary>
        /// <param name="fs"></param>
        protected abstract void PrintSimulation(FileStream fs);

        /// <summary>
        /// Subscribing on the event
        /// </summary>
        /// <param name="printerManager">The owner of the event</param>
        public void Register(PrinterManager printerManager)
        {
            CheckInput(printerManager);
            printerManager.OutputTerminator += ShowMessage;
        }

        /// <summary>
        /// Unsubscribing on the event
        /// </summary>
        /// <param name="printerManager">The owner of the event</param>
        public void Unregister(PrinterManager printerManager)
        {
            CheckInput(printerManager);
            printerManager.OutputTerminator -= ShowMessage;
        }

        /// <summary>
        /// Checks printers on the equality
        /// </summary>
        /// <param name="other">Printer to compare</param>
        /// <returns>True if printers equal, otherwise false</returns>
        public bool Equals(Printer other)
        {
            return (other.Model == Model) && (other.Name == Name);
        }
        
        public override string ToString()
        {
            return $"{Name} - {Model}";
        }

        private void ShowMessage(object sender, TerminatorEventArgs eventArgs) => Console.WriteLine(eventArgs.Message);

        private void CheckInput(PrinterManager printerManager)
        {
            if (printerManager == null)
                throw new ArgumentNullException($"{nameof(printerManager)} can't be null");
        }
    }
}
