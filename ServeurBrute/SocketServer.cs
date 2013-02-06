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
                    case ProtocoleImplementation.QUERY_NEW_BRUTE:
                        Console.WriteLine("Query_NewBrute");
                        break;
                    case ProtocoleImplementation.QUERY_GET_OPPONENT:
                        break;
                }
            }
        }

        #endregion Methods
    }
}
