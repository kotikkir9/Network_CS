using System;
using System.Net.Sockets;
using System.Text;

namespace Netværk
{
    public class LiveClient
    {
        private int _port;
        private string _ipAdress;
        private TcpClient? _client;

        private string DISCONNECT = "disconnect";

        public LiveClient(int port, string ipAdress = "127.0.0.1")
        {
            _port = port;
            _ipAdress = ipAdress;
            Start();
        }

        public void Start()
        {
            System.Console.WriteLine("<--- Connecting to the server... --->");

            try
            {
                using(_client = new TcpClient("127.0.0.1", _port))
                using(NetworkStream stream = _client.GetStream()) 
                using(StreamWriter writer = new StreamWriter(stream))
                using(StreamReader reader = new StreamReader(stream))
                {
                    System.Console.WriteLine("<--- Online --->");

                    string message = "";
                    while(message.ToLower() != DISCONNECT)
                    {
                        System.Console.Write("> ");
                        message = System.Console.ReadLine() ?? "";

                        if(message.ToLower() == DISCONNECT)
                        {
                            continue;
                        }
                        // byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message + "\n");
                        // stream.Write(bytesToSend, 0, bytesToSend.Length);

                        writer.WriteLine(message);
                        writer.Flush();

                        string response = reader.ReadLine() ?? "";
                        if(response.Equals("ok"))
                        {
                            System.Console.WriteLine("<--- Server received the message --->");
                        }
                    }
                    
                    writer.WriteLine("<end>");
                    writer.Flush();

                    // byte[] bytesTerminate = ASCIIEncoding.ASCII.GetBytes("<end>\n");
                    // stream.Write(bytesTerminate, 0, bytesTerminate.Length);

                    System.Console.WriteLine("<--- Disconnected --->");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR: " + e.Message);
            }
           
        }
    }
}