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

        public bool GetBrute(String name)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_KO)
                return false;
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
                this.GetReader().ReadImage("MyBruteImg.jpg");
                int len = this.GetReader().ReadLongInt();
                for (int i = 0; i < len; i++)
                    this.GetBonus(i, this.MyBrute);
                return true;
            }
        }

        public bool DelBrute(String name)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_DEL_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
                return true;
            else
                return false;
        }

        public bool UpdateBrute(String name, bool result)
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_UPDATE_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().CreateBoolean(result);
            this.GetWriter().Send();
            return true;
        }

        public bool CreateNewBrute(String name)
        {
            Console.WriteLine(name);
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_NEW_BRUTE);
            this.GetWriter().CreateString(name);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
                return true;
            else
                return false;
        }

        public bool Deconnection()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_DECONNEXION);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_OK)
            {
                this.GetSocket().Close();
                this.Close();
                return true;
            }
            else
                return false;
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

        public bool GetOpponent()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_OPPONENT);
            this.GetWriter().Send();
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_KO)
                return false;
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
                this.GetReader().ReadImage("OtherBruteImg.jpg");
                int len = this.GetReader().ReadLongInt();
                for (int i = 0; i < len; i++)
                    this.GetBonus(i, this.otherBrute);
                return true;
            }
        }

        public void ListeBrute()
        {
            this.GetWriter().CreateDiscriminant(ProtocoleImplementation.QUERY_GET_LIST_BRUTE);
            this.GetWriter().Send();
            int len = this.GetReader().ReadLongInt();
            Console.WriteLine(len);
            for (int i = 0; i < len; i++)
                Console.WriteLine(this.GetReader().ReadString());
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

        public bool GetBonus(int nbBonus, Brute brute)
        {
            if (this.GetReader().ReadDiscriminant() == ProtocoleImplementation.ANSWER_KO)
                return false;
            else
            {
                String[] tmp = this.GetReader().ReadStringParam();
                brute.BonusList.Add(new Bonus(tmp));
                if(MyBrute.Name.Equals(brute.Name))
                    this.GetReader().ReadImage("BruteBonus" + nbBonus +".png");
                else
                    this.GetReader().ReadImage("OtherBruteBonus" + nbBonus + ".png");
                return true;
            }
        }

        #endregion Methods
    }
}
