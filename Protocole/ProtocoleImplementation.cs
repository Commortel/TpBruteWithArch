using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocole
{
    public class ProtocoleImplementation
    {
        static public int PORT_ID = 6969;

        public static Object EXIT_TEXT = "exit";

        //Type
        public const int BYTE = 1;
        public const int BOOL = 1;
        public const int CHAR = 1;
        public const int SHORT_INT = 2;
        public const int LONG_INT = 4;
        public const int FLOAT = 8;

        // Query
        public const byte QUERY_GET_BRUTE = (byte)0x0001;
        public const byte QUERY_DEL_BRUTE = (byte)0x0002;
        public const byte QUERY_UPDATE_BRUTE = (byte)0x0003;
        public const byte QUERY_NEW_BRUTE = (byte)0x0004;
        public const byte QUERY_DECONNEXION = (byte)0x0005;
        public const byte QUERY_LOGIN = (byte)0x0006;
        public const byte QUERY_GET_LIST_OPPONENT = (byte)0x0007;
        public const byte QUERY_GET_OPPONENT = (byte)0x0008;
        public const byte QUERY_GET_LIST_BRUTE = (byte)0x0009;
        public const byte QUERY_FIGHT = (byte)0x0010;
        public const byte QUERY_POPULATE = (byte)0x0011;
        public const byte QUERY_GETBONUS = (byte)0x0012;

        // Answer
        public const byte ANSWER_OK = (byte)0x0030;
        public const byte ANSWER_KO = (byte)0x0031;
        public const byte ANSWER_TEXT = (byte)0x0032;
        public const byte ANSWER_DOWNLOAD_BRUTE = (byte)0x0033;
        public const byte ANSWER_DOWNLOAD_BRUTE_IMG = (byte)0x0034;

        // CommandeClient
        public const String GET_BRUTE = "GetBrute";
        public const String DEL_BRUTE = "DelBrute";
        public const String UPDATE_BRUTE = "Update";
        public const String NEW_BRUTE = "NewBrute";
        public const String DECONNEXION = "Exit";
        public const String LOGIN = "Login";
        public const String GET_LIST_OPPONENT = "ListOpponent";
        public const String GET_OPPONENT = "GetOpponent";
        public const String GET_LIST_BRUTE = "ListBrute";
        public const String POPULATE = "Populate";
        
    }
}
