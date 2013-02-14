using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocole;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class SocketClient : SocketImplementation
    {
        #region Fields

        private Brute myBrute = new Brute();
        private Brute otherBrute = new Brute();

        #endregion Fields

        #region Accessors

        public Brute MyBrute
        {
            get { return myBrute; }
            set { myBrute = value; }
        }

        public Brute OtherBrute
        {
            get { return otherBrute; }
            set { otherBrute = value; }
        }

        #endregion Accessors

        #region Constructors

        public SocketClient(Socket connection) : base(connection) { }

        #endregion Constructors

        #region Methods

        public void GetBrute(String name)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_KO)
                Console.WriteLine("Error Download Brute");
            else
            {
                String[] tmp = this.GetReader().ReadStringParam();
                this.myBrute.Name = tmp[0];
                this.myBrute.Level = Convert.ToInt16(tmp[1]);
                this.myBrute.Life = Convert.ToInt16(tmp[2]);
                this.myBrute.Strength = Convert.ToInt16(tmp[3]);
                this.myBrute.Agility = Convert.ToInt16(tmp[4]);
                this.myBrute.Speed = Convert.ToInt16(tmp[5]);
                this.myBrute.Image = Convert.ToInt32(tmp[6]);
                this.GetReader().ReadDiscriminant();
                Console.WriteLine(this.GetReader().ReadImage("MyBruteImg.jpg"));
                Console.WriteLine(this.myBrute.ToString());
            }
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

        public bool Login(String login, String password)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_LOGIN);
            this.GetWriter().CreateString(login);
            this.GetWriter().CreateString(password);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
                return true;
            else
                return false;
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
                this.otherBrute.Name = tmp[0];
                this.otherBrute.Level = Convert.ToInt16(tmp[1]);
                this.otherBrute.Life = Convert.ToInt16(tmp[2]);
                this.otherBrute.Strength = Convert.ToInt16(tmp[3]);
                this.otherBrute.Agility = Convert.ToInt16(tmp[4]);
                this.otherBrute.Speed = Convert.ToInt16(tmp[5]);
                this.otherBrute.Image = Convert.ToInt32(tmp[6]);
                this.GetReader().ReadDiscriminant();
                Console.WriteLine(this.GetReader().ReadImage("OtherBruteImg.jpg"));
                Console.WriteLine(this.otherBrute.ToString());
            }

            Console.WriteLine("FinGetBrute");
        }

        public void ListeBrute()
        {
            Console.WriteLine("Début Liste Brute");
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_LIST_BRUTE);
            this.GetWriter().Send();
            int len = this.GetReader().ReadLongInt();
            Console.WriteLine(len);
            for (int i = 0; i < len; i++)
                Console.WriteLine(this.GetReader().ReadString());
            Console.WriteLine("Fin Liste Brute");
        }

        public void Populate()
        {
            this.CreateNewBrute("Meyer");
            Thread.Sleep(10);
            this.CreateNewBrute("Thibaut");
            Thread.Sleep(10);
            this.CreateNewBrute("Chevalier");
            Thread.Sleep(10);
            this.CreateNewBrute("Simon");
            Thread.Sleep(10);
            this.CreateNewBrute("Lacroix");
            Thread.Sleep(10);
            this.CreateNewBrute("Florent");
            Thread.Sleep(10);
            this.CreateNewBrute("Daver");
            Thread.Sleep(10);
            this.CreateNewBrute("Léonard");
            Thread.Sleep(10);
        }

        public void GetBonus(String name)
        {
            //this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GETBONUS);
            //this.GetWriter().CreateString(name);
        }

        #endregion Methods
    }
}
