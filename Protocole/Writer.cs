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

        public void CreateByte(byte val)
        {
            try { this.ns.WriteByte(val); }
            catch (IOException E) { throw new IOException(E.Message); }
        }

        public void CreateShortInt(short val)
        {
            try 
            { 
                byte[] Tbyte = BitConverter.GetBytes(val);
                this.ns.Write(Tbyte, 0,ProtocoleImplementation.SHORT_INT);
            }
            catch (IOException E) { throw new IOException(E.Message); }
        }

        public void CreateLongInt(int val)
        {
            try
            {
                byte[] Tbyte = BitConverter.GetBytes(val);
                this.ns.Write(Tbyte, 0, ProtocoleImplementation.LONG_INT);
            }
            catch (IOException E) { throw new IOException(E.Message); }
        }

        public void CreateString(String txt)
        {
            try
            {
                this.CreateShortInt((short)txt.Length);
                byte[] Tbyte = System.Text.Encoding.UTF8.GetBytes(txt);
                this.ns.Write(Tbyte, 0, ProtocoleImplementation.CHAR*txt.Length);
            }
            catch (IOException E) { throw new IOException(E.Message); }
        }

        public void CreateBoolean(bool val)
        {
            try 
            {
                byte b = (byte)(val?0xFF:0x00);
                this.CreateByte(b);
            }
            catch (IOException E) { throw new IOException(E.Message); }
        }

        public void CreateImage(String uri)
        {
            FileStream fileStream = new FileStream(@uri, FileMode.Open, FileAccess.Read);
            try 
            {
                int length = (int)fileStream.Length;
                this.CreateLongInt(length);
                byte[] buffer = new byte[length];
                int sum = 0, count = 0;

                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
                this.ns.Write(buffer, 0, buffer.Length);
            } 
            catch (IOException e) { Console.WriteLine(e.ToString()); }
        }

        public void Send()
        {
            try { ns.Flush(); }
            catch (IOException e) { Console.WriteLine(e.ToString()); }
        }

        public void Close()
        {
            try { ns.Close(); }
            catch (IOException e) { Console.WriteLine(e.ToString()); }
        }

        #endregion Methods
    }
}
