using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Netværk
{
    public class Server
    {
        private LinkedList<Worker> _workers;
        private IPAddress _ipAdress;
        private bool _stopServer;
        
        private TcpListener _listener;

        public Server(int port, string ip = "127.0.0.1")
        {
            this._stopServer = false;
            this._workers = new LinkedList<Worker>();

            _ipAdress = IPAddress.Parse(ip);
            this._listener = new TcpListener(_ipAdress, port);
        }

        public void Start()
        {
            System.Console.WriteLine("[Server] - Online and listening...");
            _listener.Start();

            while(!_stopServer) 
            {
                try 
                {
                    TcpClient client = _listener.AcceptTcpClient();

                    var worker = new Worker(this, client);
                    _workers.AddLast(worker);
                    new Thread(worker.Start).Start();
                }
                catch
                {
                    
                }
            }

            System.Console.WriteLine("[Server] - Offline");
        }

        public bool ShutDown()
        {
            if(_workers.Count != 0) 
            {
                System.Console.WriteLine($"[Server] - Can't shut server down, {_workers.Count} process(es) still active!");
                return false;
            }
            else 
            {
                System.Console.WriteLine("[Server] - Shutting down...");
                _stopServer = true;
                _listener.Stop();
                return true;
            }
        }

        public void RemoveWorker(Worker worker)
        {
            _workers.Remove(worker);
        }
    }    
}