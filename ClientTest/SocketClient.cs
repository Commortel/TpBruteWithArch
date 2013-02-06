using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocole;
using System.Net;
using System.Net.Sockets;

namespace ClientTest
{
    class SocketClient : SocketImplementation
    {
        #region Fields
        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public SocketClient(Socket connection) : base(connection) { }

        #endregion Constructors

        #region Methods

        public void CreateNewBrute(String name)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_NEW_BRUTE);
            this.GetWriter().Send();
        }

        public void ListeBrute(String name)
        {

        }

        #endregion Methods
    }
}
