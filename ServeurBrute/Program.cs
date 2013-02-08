using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocole;

namespace ServeurBrute
{
    class Program
    {
        static void Main(string[] args)
        {
            //Serveur s = new Serveur();
            //s.Start();
            Brute b = new Brute("Pouet");
            DataManager.InsertNewUser(b);
        }
    }
}
