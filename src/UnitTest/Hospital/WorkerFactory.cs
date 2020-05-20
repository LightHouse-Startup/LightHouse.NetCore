using System;

namespace Hospital
{
    public class WorkerFactory
    {
        public Guid Id { get; } = Guid.NewGuid();

        public Worker Create(string name, bool isProgrammer = false)
        {     
            Console.WriteLine("creating...");

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (isProgrammer)
            {
                return new Programmer { Name = name };
            }
            return new Plumber { Name = name };
        }
    }
}
