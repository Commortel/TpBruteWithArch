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
                //Démarrage du thread avant la première connexion client
                Thread pingPongThread = new Thread(new ThreadStart(CheckIfStillConnected));
                pingPongThread.Start();

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

        private void CheckIfStillConnected()
        {
            //Etant donné que la propriété .Connected d'une socket n'est pas mise à jour lors de la déconnexion d'un client sans que l'on ait prélablement essayé de lire ou d'écrire sur cette socket, cette méthode
            // parvient à déterminer si une socket cliente s'est déconnectée grâce à la méthode poll. On effectue un poll en lecture sur la socket, si le poll retourne vrai et que le nombre de bytes disponible est 0
            // il s'agit d'une connexion terminée
            while (true)
            {
                for (int i = 0; i < acceptList.Count; i++)
                {
                    if (((Socket)acceptList[i]).Poll(10, SelectMode.SelectRead) && ((Socket)acceptList[i]).Available == 0)
                    {
                        Console.WriteLine("Client " + ((Socket)acceptList[i]).GetHashCode() + " déconnecté");
                        ((Socket)acceptList[i]).Close();
                        acceptList.Remove(((Socket)acceptList[i]));
                        i--;
                    }
                }
                Thread.Sleep(5);
            }
        }

        #endregion Methods
    }
}
