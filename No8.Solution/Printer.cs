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
    public abstract class Printer : IEquatable<Printer>
    {
        /// <summary>
        /// Model of a printer
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Name of a printer
        /// </summary>
        public abstract string Name { get; }

        public event EventHandler<TerminatorEventArgs> StartPrint = delegate { };
        public event EventHandler<TerminatorEventArgs> EndPrint = delegate { };

        private void OnStartPrint(object source, TerminatorEventArgs eventArgs) =>
            StartPrint?.Invoke(source, eventArgs);

        private void OnEndPrint(object source, TerminatorEventArgs eventArgs) =>
            EndPrint?.Invoke(source, eventArgs);

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

            OnStartPrint(this, new TerminatorEventArgs($"{Name}-{Model} started printing at {DateTime.Now}"));
            PrintSimulation(fs);
            OnEndPrint(this, new TerminatorEventArgs($"{Name}-{Model} finished printing at {DateTime.Now}"));
        }

        /// <summary>
        /// Abstract method for printing fole content logic
        /// </summary>
        /// <param name="fs"></param>
        protected abstract void PrintSimulation(FileStream fs);
        
        /// <summary>
        /// Checks printers on the equality
        /// </summary>
        /// <param name="other">Printer to compare</param>
        /// <returns>True if printers equal, otherwise false</returns>
        public bool Equals(Printer other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(Model, other.Model) && string.Equals(Name, other.Name);
        }

        /// <summary>
        /// Implemented method of <see cref="IEquatable{T}"/>
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if objects equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Printer)obj);
        }

        public override int GetHashCode()
        {
            return (Model != null ? Model.GetHashCode() : 0) * (Name != null ? Name.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"{Name} - {Model}";
        }
    }
}
