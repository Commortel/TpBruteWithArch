using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Protocole
{
    public class Writer
    {
        #region Fields

        private NetworkStream ns;

        #endregion Fields

        #region Constructors

        public Writer(NetworkStream Flux)
        {
            this.ns = Flux;
        }

        #endregion Constructors

        #region Methods

        public void CreateDiscriminant(byte disc)
        {
            this.CreateByte(disc);
        }

        public void CreateByte(byte value)
        {
            this.ns.WriteByte(value);
        }

        /*public void CreateText(String txt)
        {
            try
            {
                byte StrByte = Convert.ToByte(txt);

                this.ns.Write(
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CreateInt(int i)
        {
            try
            {
                ns.WriteLine(i);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }*/

        public void Send()
        {
            try
            {
                ns.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Close()
        {
            try
            {
                ns.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion Methods
    }
}
