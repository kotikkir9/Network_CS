using System.Net.Sockets;
using System.Text;

namespace Netværk
{
    public class Client
    {
        private string _clientName;
        private int _port;
        private string _ipAdress;
        private string[] _messageArray;   
        private TcpClient? _client;

        public Client(string name, int port, string messagesToSend, string ipAdress = "127.0.0.1")
        {
            _clientName = name;
            _port = port;
            _ipAdress = ipAdress;
            _messageArray = messagesToSend.Split(" ");
        }

        public void Start()
        {
            System.Console.WriteLine($"[{_clientName}] - Online");

            using(_client = new TcpClient("127.0.0.1", _port))
            using(NetworkStream stream = _client.GetStream()) 
            using(StreamWriter writer = new StreamWriter(stream))
            {
                foreach(var message in _messageArray)
                {
                    // byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
                    // stream.Write(bytesToSend, 0, bytesToSend.Length);

                    writer.WriteLine(message);
                    writer.Flush();

                    Thread.Sleep(2000);
                }

                // byte[] bytesTerminate = ASCIIEncoding.ASCII.GetBytes("<end>");
                // stream.Write(bytesTerminate, 0, bytesTerminate.Length);

                writer.WriteLine("<end>");
                writer.Flush();

                System.Console.WriteLine($"[{_clientName}] - Offline");
            }
        }
    }
}
