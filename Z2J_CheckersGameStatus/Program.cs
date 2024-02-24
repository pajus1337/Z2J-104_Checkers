using System;
using System.IO;
using System.IO.Pipes;


namespace Z2J_CheckersGameStatus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Status Window";
            Console.WriteLine("Status Window : Online ,awaiting a content");

            while (true)
            {
                using (var server = new NamedPipeServerStream("GameStatusPipe"))
                {
                    server.WaitForConnection();

                    using (var reader = new StreamReader(server))
                    {
                        string message;
                        while ((message = reader.ReadLine()) != null)
                        {
                            Console.WriteLine($"{DateTime.Now.TimeOfDay:hh\\:mm\\:ss} : {message}");
                        }
                    }
                }
            }
        }
    }
}
