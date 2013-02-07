using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Protocole
{
    public class SocketImplementation
    {
        #region Fields

        private Socket connection;
        private NetworkStream Flux;
        private Reader reader;
        private Writer writer;

        #endregion Fields

        #region Accessors

        public Socket GetSocket()
        {
            return this.connection;
        }

        public Reader GetReader()
        {
            return this.reader;
        }

        public Writer GetWriter()
        {
            return this.writer;
        }

        #endregion Accessors

        #region Constructors

        public SocketImplementation(Socket connection)
        {
            this.connection = connection;
            this.Flux = new NetworkStream(connection);
            this.reader = new Reader(this.Flux);
            this.writer = new Writer(this.Flux);
        }

        #endregion Constructors

        #region Methods

        public void Close()
        {
            try 
            {
                this.connection.Close();
            }
            catch (SocketException e) 
            {
                Console.WriteLine("Close Socket fail : " + e.Message);
            }
        }

        #endregion Methods
    }
}
