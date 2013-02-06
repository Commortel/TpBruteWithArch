using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Protocole
{
    public class Reader
    {
        #region Fields

        private NetworkStream ns;

        #endregion Fields

        #region Constructors

        public Reader(NetworkStream Flux)
        {
            this.ns = Flux;
        }

        #endregion Constructors

        #region Methods

        public byte ReadByte()
        {
            byte[] b = new byte[1];
            this.ns.Read(b, 0, 1);
            return b[0];
        }

        public byte ReadDiscriminant()
        {
            return this.ReadByte();
        }

        /*public String ReadText()
        {
            try
            {
                String tmp = sr.ReadLine();
                //int length = Convert.ToInt32(tmp.Substring(0, 2));
                return tmp;//.Substring(2, length);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public int ReadInt()
        {
            try
            {
                return Convert.ToInt32(sr.ReadLine());
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }

        public String[] ReadTabString()
        {
            try
            {
                String tmp = sr.ReadLine();
                return tmp.Split(delimeterChars);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }*/

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
