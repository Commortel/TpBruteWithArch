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

         public void GetBrute(String name)
         {
             Console.WriteLine("GetBrute");
             if (SocketServer.listBrute.ContainsKey(name))
             {
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_DOWNLOAD_BRUTE);
                 this.GetWriter.CreateString(SocketServer.listBrute[name].getParam());
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_DOWNLOAD_BRUTE_IMG);
                 this.GetWriter.CreateImage("Perso-"+SocketServer.listBrute[name].Image+".jpg");
             }
             else
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             
             this.GetWriter.Send();
             Console.WriteLine("FinGetBrute");
         }

         public void DelBrute(String name)
         {
         }

         public void UpdateBrute(String name, bool result)
         {
         }

         public void NewBrute(String name)
         {
             Console.WriteLine("NewBrute");
             Brute brute = new Brute(name);
             brute.randomValue();
             SocketServer.listBrute.Add(name,brute);
             this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
             this.GetWriter.Send();
             Console.WriteLine("NewBrute" + SocketServer.listBrute.Count);
         }

         public void Deconnection(String name)
         {
         }

         public void Login(String login, String password)
         {

         }

         public void ListOpponent()
         {
         }

         public void GetOpponent()
         {
             Console.WriteLine("GetOpponent");
             Brute tmp = SocketServer.listBrute.ElementAt(new Random().Next(0,SocketServer.listBrute.Count)).Value;
             this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_DOWNLOAD_BRUTE);
             this.GetWriter.CreateString(tmp.getParam());
             this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_DOWNLOAD_BRUTE_IMG);
             this.GetWriter.CreateImage("Perso-" + tmp.Image + ".jpg");

             this.GetWriter.Send();
             Console.WriteLine("FinGetOpponent");
         }

         public void ListeBrute()
         {
             Console.WriteLine("ListBrute");
             this.GetWriter.CreateLongInt(SocketServer.listBrute.Count);
             if (SocketServer.listBrute.Count != 0)
             {
                 Console.WriteLine("ListBrute pleine" + SocketServer.listBrute.Count);
                 foreach (var brute in SocketServer.listBrute)
                 {
                     Console.WriteLine(brute.Value.getParam());
                     this.GetWriter.CreateString(brute.Value.getParam());
                 }
             }
             else
             {
                 this.GetWriter.CreateString("");
                 Console.WriteLine("ListBruteVide");
             }
             this.GetWriter.Send();
             Console.WriteLine("FinListBrute");
         }

        #endregion Methods
    }
}
