using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;
using Protocole;

namespace ServeurBrute
{
    class Serveur
    {
        #region Fields

        public static ArrayList acceptList = new ArrayList();

        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public Serveur(){}

        #endregion Constructors

        #region Methods

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Console.WriteLine("IP=" + ipAddress.ToString());
            Socket CurrentClient = null;
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //On lie la socket au point de communication
                ServerSocket.Bind(new IPEndPoint(ipAddress, ProtocoleImplementation.PORT_ID));
                //On la positionne en mode "écoute"
                ServerSocket.Listen(10);

                while (true)
                {
                    Console.WriteLine("Attente d'une nouvelle connexion...");
                    //L'exécution du thread courant est bloquée jusqu'à ce qu'un nouveau client se connecte
                    CurrentClient = ServerSocket.Accept();
                    Console.WriteLine("Nouveau client:" + CurrentClient.GetHashCode());
                    acceptList.Add(CurrentClient);
                    SocketServer n = new SocketServer(CurrentClient);
                    Thread getReadClients = new Thread(new ThreadStart(n.GetQuery));
                    getReadClients.Start();
                }
            }
            catch (SocketException E)
            {
                Console.WriteLine(E.Message);
            }
        }

        #endregion Methods
    }
}
