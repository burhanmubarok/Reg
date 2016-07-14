using System;
using StructureMap;
using StructureMap.Graph;

namespace Reg
{
    class Program 
    {
        static void Main(string[] args)
        {
            var container = Container.For<ConsoleRegistry>();
            var app = container.GetInstance<Application>();
            app.Run();
            Console.ReadKey();
        }
    }

    internal class Application
    {
        private readonly IWriter _writer;
        private readonly ILog _logger;

        public Application(IWriter writer, ILog logger)
        {
            _writer = writer;
            _logger = logger;
        }

        public void Run()
        {
            _logger.Info(nameof(Application) + " started.");
            _writer.WriteLine("Hello Code!.");
            _logger.Info(nameof(Application) + " finished.");
        }
    }

    internal class ConsoleRegistry : Registry
    {
        public ConsoleRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
            For<ILog>().Use<ConsoleLogger>();
        }
    }

    internal interface ILog
    {
        void Info(string message);
    }

    public class ConsoleLogger : ILog
    {
        public void Info(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }

    public interface IWriter
    {
        void WriteLine(string output);
    }

    public class Writer : IWriter
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
