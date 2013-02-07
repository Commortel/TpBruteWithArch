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
        private char[] delimeterChars = { ':' };

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
            try
            {
                byte[] b = new byte[1];
                this.ns.Read(b, 0, b.Length);
                return b[0];
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return (byte)0;
        }

        public byte ReadDiscriminant()
        {
            return this.ReadByte();
        }

        public short ReadShortInt()
        {
            try
            {
                byte[] b = new byte[ProtocoleImplementation.SHORT_INT];
                int numberOfBytesRead = 0;
                while (numberOfBytesRead < b.Length)
                {
                    numberOfBytesRead += this.ns.Read(b, 0, b.Length);
                }
                return BitConverter.ToInt16(b, 0);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return -1;
        }

        public int ReadLongInt()
        {
            try
            {
                byte[] b = new byte[ProtocoleImplementation.LONG_INT];
                int numberOfBytesRead = 0;
                while (numberOfBytesRead < b.Length)
                {
                    numberOfBytesRead += this.ns.Read(b, 0, b.Length);
                }
                return BitConverter.ToInt32(b,0);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return -1;
        }

        public String ReadString()
        {
            try
            {
                byte[] b = new byte[this.ReadShortInt()];
                int numberOfBytesRead = 0;
                while (numberOfBytesRead < b.Length)
                {
                    numberOfBytesRead += this.ns.Read(b, 0, b.Length);
                }
                return System.Text.Encoding.UTF8.GetString(b);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public String[] ReadStringParam()
        {
            try
            {
                byte[] b = new byte[this.ReadShortInt()];
                int numberOfBytesRead = 0;
                while (numberOfBytesRead < b.Length)
                {
                    numberOfBytesRead += this.ns.Read(b, 0, b.Length);
                }
                return System.Text.Encoding.UTF8.GetString(b).Split(delimeterChars);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public bool ReadBoolean()
        {
            try
            {
                byte b = this.ReadByte();
                return b != 0x00;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        public String ReadImage(String uri)
        {
            int imgSize = this.ReadLongInt();
            byte[] b = new byte[imgSize];
            int read = 0;
            while(read < imgSize){
                read += this.ns.Read(b, read, imgSize - read);
            }

            using (FileStream fs = new FileStream(@uri, FileMode.Create))
            {
                fs.Write(b, 0, b.Length);
            }
            return uri;
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
