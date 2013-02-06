using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocole;
using System.Net;
using System.Net.Sockets;

namespace ServeurBrute
{
    class SocketServer : SocketImplementation
    {
        #region Fields
        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public SocketServer(Socket connection) : base (connection) {}

        #endregion Constructors

        #region Methods

        public void GetQuery()
        {
            while (this.GetSocket().Connected)
            {
                Reader rd = this.GetReader();
                int d = rd.ReadDiscriminant();
                switch (d)
                {
                    case ProtocoleImplementation.QUERY_GET_BRUTE:
                        Console.WriteLine("QUERY_GET_BRUTE");
                        break;
                    case ProtocoleImplementation.QUERY_DEL_BRUTE:
                        Console.WriteLine("QUERY_DEL_BRUTE");
                        break;
                    case ProtocoleImplementation.QUERY_UPDATE_BRUTE:
                        Console.WriteLine("QUERY_GET_BRUTE");
                        break;
                    case ProtocoleImplementation.QUERY_NEW_BRUTE:
                        (new Query(this.GetWriter())).readCreateBrute(rd.ReadDiscriminant);
                        break;
                    case ProtocoleImplementation.QUERY_DECONNEXION:
                        Console.WriteLine("QUERY_DECONNEXION");
                        break;
                    case ProtocoleImplementation.QUERY_GET_LIST_OPPONENT:
                        Console.WriteLine("QUERY_GET_LIST_OPPONENT");
                        break;
                    case ProtocoleImplementation.QUERY_GET_OPPONENT:
                        Console.WriteLine("QUERY_GET_OPPONENT");
                        break;
                    case ProtocoleImplementation.QUERY_GET_LIST_BRUTE:
                        Console.WriteLine("QUERY_GET_LIST_BRUTE");
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Methods
    }
}
