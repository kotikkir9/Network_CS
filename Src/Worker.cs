using System.Net.Mime;
using System.Net.Sockets;
using System.Text;

namespace Netværk
{
    public class Worker
    {
        private static int idCount = 0;

        private Server _server;
        private TcpClient _client;
        private int _workerId;

        private bool _receivedEven = false;
        private bool _receivedOdd = false;

        public Worker(Server server, TcpClient client)
        {
            _server = server;
            _client = client;
            _workerId = ++idCount;
        }

        public void Start() {
            System.Console.WriteLine($"[Worker {_workerId}] - New worker object created");

            using(NetworkStream stream = _client.GetStream()) 
            using(StreamReader reader = new StreamReader(stream))
            using(StreamWriter writer = new StreamWriter(stream))
            {
                // byte[] buffer = new byte[_client.ReceiveBufferSize];
                // byte[] buffer = new byte[8192];

                string dataReceived = "";

                while(!dataReceived.ToLower().Equals("<end>")) {
                    dataReceived = reader.ReadLine()?.Trim() ?? "";

                    // int bytesRead = stream.Read(buffer, 0, _client.ReceiveBufferSize);
                    // int bytesRead = stream.Read(buffer, 0, 8192);
                    // string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    
                    // var builder = new StringBuilder();

                    // foreach(char c in receivedMessage.TrimStart()) 
                    // {
                        
                    //     if(!c.Equals('\n'))
                    //     {
                    //         builder.Append(c);
                    //     } 
                    //     else 
                    //     {
                    //         break;
                    //     }
                    // }

                    // dataReceived = builder.ToString();

                    writer.WriteLine("ok");
                    writer.Flush();

                    if(dataReceived.ToLower().Equals("<end>"))
                    {
                        continue;
                    }

                    if(int.TryParse(dataReceived, out int number))
                    {
                        string result;
                        if(number % 2 == 0)
                        {
                            result = _receivedEven ? "even again" : "even";
                            _receivedEven = true;
                        }
                        else
                        {
                            result = _receivedOdd ? "odd again" : "odd";
                            _receivedOdd = true;                  
                        }

                        Console.WriteLine($"[Worker {_workerId}] - Received : {number,-3} {result}");
                    }
                    else
                    {
                        System.Console.WriteLine($"[Worker {_workerId}] - Received : {dataReceived} - Unable to parse the message");
                    }    
                }
            }
            
            System.Console.WriteLine($"[Worker {_workerId}] - Work done, bye!");   
            _server.RemoveWorker(this); 
            _client.Close();
        }
    }
}