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
        public static ArrayList listUser = new ArrayList();

        #endregion Fields

        #region Accessors
        #endregion Accessors

        #region Constructors

        public SocketServer(Socket connection) : base (connection) {}

        #endregion Constructors

        #region Methods

        public void GetQuery()
        {
            while (true)
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
                        (new Query(this.GetWriter())).Deconnection(rd.ReadString());
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
                    default:
                        break;
                }
            }
        }

        #endregion Methods
    }
}
