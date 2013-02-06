using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocole;
using System.Net;
using System.Net.Sockets;

namespace ServeurBrute
{
    class Query
    {
        #region Fields

        private Writer writer;

        #endregion Fields

        #region Accessors

        public Writer GetWriter
        {
            get { return writer; }
            set { writer = value; }
        }

        #endregion Accessors

        #region Constructors

        public Query(Writer writer) { this.writer = writer; }

        #endregion Constructors

        #region Methods

         public void CreateBrute(String name) 
         {
            Console.WriteLine("NewBrute");
            short level = 1;
            short strength = 10;
            short agility = 10;
            short speed = 10;
            short life = 10;

            Brute brute = new Brute(name, level, life, strength, agility, speed/*, image*/);
            SocketServer.listBrute.Add(brute);
            this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
            this.GetWriter.Send();
        }

         public void ListBrute()
         {
             Console.WriteLine("ListBrute");
             this.GetWriter.CreateShortInt((short)SocketServer.listBrute.Count);
             if (SocketServer.listBrute.Count != 0)
             {
                 Console.WriteLine("ListBrute pleine" + (short)SocketServer.listBrute.Count);
                 foreach (Brute brute in SocketServer.listBrute)
                 {
                     this.GetWriter.CreateString(brute.getParam());
                 }
             }
             else
             {
                 this.GetWriter.CreateString("");
                 Console.WriteLine("ListBruteVide");
             }
             this.GetWriter.Send();
         }

         public void Opponent(String name)
         {
             throw new NotImplementedException();
         }

        #endregion Methods
    }
}
