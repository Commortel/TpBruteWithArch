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

        private int len = 0;

        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public SocketClient(Socket connection) : base(connection) { }

        #endregion Constructors

        #region Methods

        public void GetBrute()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_BRUTE);
            this.GetWriter().Send();
        }

        public void DelBrute()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_DEL_BRUTE);
            this.GetWriter().Send();
        }

        public void UpdateBrute()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_UPDATE_BRUTE);
            this.GetWriter().Send();
        }

        public void CreateNewBrute(String name)
        {
            Console.WriteLine(name);
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_NEW_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
                Console.WriteLine("Succes");
            else
                Console.WriteLine("Fail");
        }

        public void Deconnection()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_DECONNEXION);
            this.GetWriter().Send();
        }

        public void Login()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_LOGIN);
            this.GetWriter().Send();
        }

        public void ListOpponent()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_LIST_OPPONENT);
            this.GetWriter().Send();
        }

        public void GetOpponent()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_OPPONENT);
            this.GetWriter().Send();
        }

        public void ListeBrute()
        {
            Console.WriteLine("Début Liste Brute");
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_LIST_BRUTE);
            this.GetWriter().Send();
            this.len = this.GetReader().ReadLongInt();
            Console.WriteLine(len);
            for (int i = 0; i < len; i++)
                Console.WriteLine(this.GetReader().ReadString());
            Console.WriteLine("Fin Liste Brute");
            this.len = 0;
        }

        public void Populate()
        {
            this.CreateNewBrute("Meyer");
            this.CreateNewBrute("Thibaut");
            this.CreateNewBrute("Chevalier");
            this.CreateNewBrute("Simon");
            this.CreateNewBrute("Lacroix");
            this.CreateNewBrute("Florent");
            this.CreateNewBrute("Daver");
            this.CreateNewBrute("Léonard");
        }

        #endregion Methods
    }
}
