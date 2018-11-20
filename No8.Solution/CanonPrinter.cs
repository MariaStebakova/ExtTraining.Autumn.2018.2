using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    /// <summary>
    /// Class for Canon printers
    /// </summary>
    public class CanonPrinter: Printer
    {
        public CanonPrinter(string model) : base(model)
        {
        }

        public override string Name => "Canon";

        /// <summary>
        /// Printing file content logic on the Canon printer
        /// </summary>
        /// <param name="fs">File to print</param>
        protected override void PrintSimulation(FileStream fs)
        {
            for (int i = 0; i < fs.Length; i++)
            {
                // simulate printing
                Console.WriteLine(fs.ReadByte());
            }
        }
    }
}
