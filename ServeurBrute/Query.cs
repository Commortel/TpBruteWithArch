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

         public void readCreateBrute(String name) 
         {    
            short level = 1;
            short strength = 10;
            short agility = 10;
            short speed = 10;
            short life = 10;

            Brute brute = new Brute(name, level, life, strength, agility, speed/*, image*/);
            this.GetWriter.CreateDiscriminant(ProtocoleImplementation.ANSWER_OK);
            this.GetWriter.Send();
        }

        #endregion Methods
    }
}
