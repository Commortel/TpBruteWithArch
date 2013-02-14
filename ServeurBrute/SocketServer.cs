using System;
using System.Collections;
using System.Linq;
using System.Text;
using Protocole;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ServeurBrute
{
    public class SocketServer : SocketImplementation
    {
        #region Fields

        public static Dictionary<String, Brute> listBrute = new Dictionary<String, Brute>();
        public static Dictionary<String, String> listUser = new Dictionary<String, String>();
        public static Dictionary<String, Bonus> listBonus = new Dictionary<string,Bonus>();

        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public SocketServer(Socket connection) : base (connection) {}

        #endregion Constructors

        #region Methods

        public void GetQuery()
        {
            this.Initialize();
            while (this.GetSocket().Connected)
            {
                Reader rd = this.GetReader();
                int d = rd.ReadDiscriminant();
                switch (d)
                {
                    case ProtocoleImplementation.QUERY_GET_BRUTE:
                        (new Query(this.GetWriter())).GetBrute(rd.ReadString());
                        break;
                    case ProtocoleImplementation.QUERY_DEL_BRUTE:
                        (new Query(this.GetWriter())).DelBrute(rd.ReadString());
                        break;
                    case ProtocoleImplementation.QUERY_UPDATE_BRUTE:
                        (new Query(this.GetWriter())).UpdateBrute(rd.ReadString(),rd.ReadBoolean());
                        break;
                    case ProtocoleImplementation.QUERY_NEW_BRUTE:
                        (new Query(this.GetWriter())).NewBrute(rd.ReadString());
                        break;
                    case ProtocoleImplementation.QUERY_DECONNEXION:
                        (new Query(this.GetWriter())).Deconnection();
                        this.GetSocket().Close();
                        Serveur.acceptList.Remove(this.GetSocket());
                        break;
                    case ProtocoleImplementation.QUERY_LOGIN:
                        (new Query(this.GetWriter())).Login(rd.ReadString(), rd.ReadString());
                        break;
                    case ProtocoleImplementation.QUERY_GET_LIST_OPPONENT:
                        (new Query(this.GetWriter())).ListOpponent();
                        break;
                    case ProtocoleImplementation.QUERY_GET_OPPONENT:
                        (new Query(this.GetWriter())).GetOpponent();
                        break;
                    case ProtocoleImplementation.QUERY_GET_LIST_BRUTE:
                        (new Query(this.GetWriter())).ListeBrute();
                        break;
                    case ProtocoleImplementation.QUERY_GETBONUS:
                        (new Query(this.GetWriter())).GetBonus(rd.ReadString());
                        break;
                    default:
                        break;
                }
            }
        }

        private void Initialize()
        {
            SocketServer.listBrute = DataManager.Read();
            SocketServer.listUser["Meyer"] = "Meyer";
            SocketServer.listBonus.Add("Sword",new Bonus("Sword",0,15,5,5,1));
            SocketServer.listBonus.Add("Trident",new Bonus("Trident",0,20,-5,5,2));
            SocketServer.listBonus.Add("Knife",new Bonus("Knife",0,2,15,15,1));
        }

        #endregion Methods
    }
}
