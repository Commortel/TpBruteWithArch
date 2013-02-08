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

        public void GetBrute(String name)
        {
            Console.WriteLine("GetBrute");
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_KO)
                Console.WriteLine("Error Download Brute");
            else
            {
                String[] tmp = this.GetReader().ReadStringParam();
                Client.myBrute.Name = tmp[0];
                Client.myBrute.Level = Convert.ToInt16(tmp[1]);
                Client.myBrute.Life = Convert.ToInt16(tmp[2]);
                Client.myBrute.Strength = Convert.ToInt16(tmp[3]);
                Client.myBrute.Agility = Convert.ToInt16(tmp[4]);
                Client.myBrute.Speed = Convert.ToInt16(tmp[5]);
                Client.myBrute.Image = Convert.ToInt32(tmp[6]);
                this.GetReader().ReadDiscriminant();
                Console.WriteLine(this.GetReader().ReadImage("MyBruteImg.jpg"));
                Console.WriteLine(Client.myBrute.ToString());
            }
            Console.WriteLine("FinGetBrute");
        }

        public void DelBrute(String name)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_DEL_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
                Console.WriteLine("DelBrute done");
            else
                Console.WriteLine("Fail DelBrute");
        }

        public void UpdateBrute(String name, bool result)
        {
            Console.WriteLine("Début UpdateBrute");
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_UPDATE_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().CreateBoolean(result);
            this.GetWriter().Send();
            Console.WriteLine("Fin UpdateBrute");
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
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
            {
                Console.WriteLine("Deconnection");
                this.GetSocket().Close();
                this.Close();
            }
            else
                Console.WriteLine("Fail");
        }

        public void Login(String login, String password)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_LOGIN);
            this.GetWriter().CreateString(login);
            this.GetWriter().CreateString(password);
            this.GetWriter().Send();
            if(this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
                Console.WriteLine("Connecté");
            else
                Console.WriteLine("Fail Connection");
        }

        public void ListOpponent()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_LIST_OPPONENT);
            this.GetWriter().Send();
        }

        public void GetOpponent()
        {
            Console.WriteLine("GetBrute");
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_OPPONENT);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_KO)
                Console.WriteLine("Error Download Brute");
            else
            {
                String[] tmp = this.GetReader().ReadStringParam();
                Client.otherBrute.Name = tmp[0];
                Client.otherBrute.Level = Convert.ToInt16(tmp[1]);
                Client.otherBrute.Life = Convert.ToInt16(tmp[2]);
                Client.otherBrute.Strength = Convert.ToInt16(tmp[3]);
                Client.otherBrute.Agility = Convert.ToInt16(tmp[4]);
                Client.otherBrute.Speed = Convert.ToInt16(tmp[5]);
                Client.otherBrute.Image = Convert.ToInt32(tmp[6]);
                this.GetReader().ReadDiscriminant();
                Console.WriteLine(this.GetReader().ReadImage("OtherBruteImg.jpg"));
                Console.WriteLine(Client.otherBrute.ToString());
            }

            Console.WriteLine("FinGetBrute");
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
