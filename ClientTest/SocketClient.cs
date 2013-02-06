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
            Console.WriteLine(name);
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_NEW_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
        }

        public void ListeBrute()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_LIST_BRUTE);
            this.GetWriter().Send();
            int len = this.GetReader().ReadShortInt();
            Console.WriteLine(len);
            for(int i=0; i < len; i++)
             Console.WriteLine(this.GetReader().ReadString());
        }

        #endregion Methods
    }
}
