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
                 this.GetWriter.CreateImage("../../res/perso/Perso-"+SocketServer.listBrute[name].Image+".jpg");
                 this.GetWriter.CreateLongInt(SocketServer.listBrute[name].BonusList.Count);
                 foreach (Bonus bonus in SocketServer.listBrute[name].BonusList) { this.GetBonus(bonus.Name); }
             }
             else
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             
             this.GetWriter.Send();
             Console.WriteLine("FinGetBrute");
         }

         public void DelBrute(String name)
         {
             Console.WriteLine("DelBrute");
             if(SocketServer.listBrute.Remove(name))
                this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
             else
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             this.GetWriter.Send();
             Console.WriteLine("DelBrute" + SocketServer.listBrute.Count);
         }

         public void UpdateBrute(String name, bool result)
         {
             Console.WriteLine("Début Update Brute");
             if(result)SocketServer.listBrute[name].Update();
             Console.WriteLine("Fin Update Brute");
         }

         public void NewBrute(String name)
         {
             Console.WriteLine("NewBrute");
             if(!SocketServer.listBrute.ContainsKey(name))
             {
                 Brute brute = new Brute(name);
                 brute.randomValue();
                 SocketServer.listBrute.Add(name,brute);
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
            }
             else
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             this.GetWriter.Send();
             Console.WriteLine("NewBrute");
         }

         public void Deconnection()
         {
             DataManager.Save(SocketServer.listBrute);
             Console.WriteLine("Deconnection");
             this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
             this.GetWriter.Send();
         }

         public void Login(String login, String password)
         {
             try
             {
                 if (SocketServer.listUser[login] == password)
                     this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
                 else
                     this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             }
             catch (KeyNotFoundException)
             {
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             }
             this.GetWriter.Send();
         }

         public void ListOpponent()
         {
         }

         public void GetOpponent(String name)
         {
             Console.WriteLine("GetOpponent");
             Brute tmp = new Brute(); bool OtherBrute = true;
             while (OtherBrute)
             {
                tmp = SocketServer.listBrute.ElementAt(new Random().Next(0,SocketServer.listBrute.Count)).Value;
                OtherBrute = tmp.Name.Equals(name);
             }
             this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_DOWNLOAD_BRUTE);
             this.GetWriter.CreateString(tmp.getParam());
             this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_DOWNLOAD_BRUTE_IMG);
             this.GetWriter.CreateImage("../../res/perso/Perso-" + tmp.Image + ".jpg");
             this.GetWriter.CreateLongInt(tmp.BonusList.Count);
             foreach (Bonus bonus in tmp.BonusList) { this.GetBonus(bonus.Name); }
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

         public void GetBonus(String name)
         {
             Console.WriteLine("DebutGetBonus");
            try
             {
                 if (SocketServer.listBonus.ContainsKey(name))
                 {
                     this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
                     this.GetWriter.CreateString(SocketServer.listBonus[name].getParam());
                     this.GetWriter.CreateImage("../../res/bonus/arme-" + SocketServer.listBonus[name].Image + ".png");
                 }
                 else
                     this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             }
             catch (KeyNotFoundException)
             {
                 this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_KO);
             }
            Console.WriteLine("FinGetBonus");
         }

        #endregion Methods

    }
}
