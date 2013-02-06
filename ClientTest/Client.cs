using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;
using Protocole;

namespace ClientTest
{
    class Client
    {
        #region Fields

        private IPAddress ip;
        private IPEndPoint ipEnd;
        private Socket ClientSocket;
        private SocketClient n;
        private Thread GetRead;
        
        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public Client()
        {
            this.ip = IPAddress.Parse("127.0.0.1");
            this.ipEnd = new IPEndPoint(ip, ProtocoleImplementation.PORT_ID);
            this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.GetRead = new Thread(new ThreadStart(SendQuery));
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {
            try
            {
                ClientSocket.Connect(ipEnd);
                if (ClientSocket.Connected)
                {
                    this.n = new SocketClient(ClientSocket);
                    GetRead.Start();
                }
            }
            catch (SocketException E)
            {
                Console.WriteLine("Connection" + E.Message);
            }
        }

        public void SendQuery()
        {
            String tmp = null;
            while (true)
            {
                Console.Write("Commande : "); 
                tmp = Console.ReadLine();
                switch (tmp)
                {
                    case ProtocoleImplementation.GET_BRUTE:
                        Console.WriteLine("QUERY_GET_BRUTE");
                        break;
                    case ProtocoleImplementation.DEL_BRUTE:
                        Console.WriteLine("QUERY_DEL_BRUTE");
                        break;
                    case ProtocoleImplementation.UPDATE_BRUTE:
                        Console.WriteLine("QUERY_GET_BRUTE");
                        break;
                    case ProtocoleImplementation.NEW_BRUTE:
                        this.n.CreateNewBrute(Console.ReadLine());
                        break;
                    case ProtocoleImplementation.DECONNEXION:
                        Console.WriteLine("QUERY_DECONNEXION");
                        break;
                    case ProtocoleImplementation.GET_LIST_OPPONENT:
                        Console.WriteLine("QUERY_GET_LIST_OPPONENT");
                        break;
                    case ProtocoleImplementation.GET_OPPONENT:
                        Console.WriteLine("QUERY_GET_OPPONENT");;
                        break;
                    case ProtocoleImplementation.GET_LIST_BRUTE:
                        this.n.ListeBrute();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Methods
    }
}
