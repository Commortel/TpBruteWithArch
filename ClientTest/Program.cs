using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;
using Protocole;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client();
            c.Start();
        }
    }
}
