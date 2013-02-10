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
        private SocketClient client;
        private Thread GetRead;
        public static Brute myBrute = new Brute();
        public static Brute otherBrute = new Brute();
        
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
                    this.client = new SocketClient(ClientSocket);
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
            while (ClientSocket.Connected)
            {
                Console.Write("Commande : "); 
                tmp = Console.ReadLine();
                switch (tmp)
                {
                    case ProtocoleImplementation.GET_BRUTE:
                        this.client.GetBrute(Console.ReadLine());
                        break;
                    case ProtocoleImplementation.DEL_BRUTE:
                        this.client.DelBrute(Console.ReadLine());
                        break;
                    case ProtocoleImplementation.UPDATE_BRUTE:
                        this.client.UpdateBrute(Console.ReadLine(),Convert.ToBoolean(Console.ReadLine()));
                        break;
                    case ProtocoleImplementation.NEW_BRUTE:
                        this.client.CreateNewBrute(Console.ReadLine());
                        break;
                    case ProtocoleImplementation.DECONNEXION:
                        this.client.Deconnection();
                        break;
                    case ProtocoleImplementation.LOGIN:
                        this.client.Login(Console.ReadLine(), Console.ReadLine());
                        break;
                    case ProtocoleImplementation.GET_LIST_OPPONENT:
                        this.client.ListOpponent();
                        break;
                    case ProtocoleImplementation.GET_OPPONENT:
                        this.client.GetOpponent();
                        break;
                    case ProtocoleImplementation.GET_LIST_BRUTE:
                        this.client.ListeBrute();
                        break;
                    case ProtocoleImplementation.POPULATE:
                        this.client.Populate();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Methods
    }
}
