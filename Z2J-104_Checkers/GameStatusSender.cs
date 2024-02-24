using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class GameStatusSender : IGameStatusSender
    {
        public void SendStatus(string message)
        {
            using (var client = new NamedPipeClientStream("GameStatusPipe"))
            {
                var timeOut = 5000;
                try
                {
                    client.Connect(timeOut);
                    Console.WriteLine("Connected with status window");

                    using (var steamWriter = new StreamWriter(client) { AutoFlush = true })
                    {
                        steamWriter.WriteLine(message);
                        Console.WriteLine($"Test Message send: {message}");
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine($"Failed to connect status window after : {timeOut}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"We have some Exception : {ex.Message}");
                }
            }
        }
    }
}
