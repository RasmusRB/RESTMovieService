using System;

namespace ConsumeREST
{
    class Program
    {
        static void Main(string[] args)
        {
            RESTConsumer worker = new RESTConsumer();
            worker.Start();

            Console.ReadLine();
        }
    }
}
