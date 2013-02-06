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

        // Query
        static public int QUERY_GET_BRUTE = 0x0001;
        static public int QUERY_DEL_BRUTE = 0x0002;
        static public int QUERY_UPDATE_BRUTE = 0x0003;
        public const byte QUERY_NEW_BRUTE = (byte)0x0004;
        static public int QUERY_DECONNEXION = 0x0005;
        static public int QUERY_GET_LIST_OPPONENT = 0x0006;
        public const int QUERY_GET_OPPONENT = 0x0007;
        static public int QUERY_GET_LIST_BRUTE = 0x0008;

        // Answer
        static public int ANSWER_OK = 0x0010;
        static public int ANSWER_KO = 0x0011;
        static public int ANSWER_TEXT = 0x0012;
        static public int ANSWER_DOWNLOAD_BRUTE = 0x0013;
        static public int ANSWER_DOWNLOAD_BRUTE_IMG = 0x0014;
    }
}
