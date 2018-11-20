using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    public class TerminatorEventArgs : EventArgs
    {
        private string message;

        public string Message => message;

        public TerminatorEventArgs(string message)
        {
            this.message = message;
        }
}
}
