using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Protocole;
using System.Collections;
using System.IO;

namespace ServeurBrute
{
    public class DataManager
    {
        /// <summary> Expression XPath utilisée pour trouver le noeud XML renfermant les
        /// données relatives à l'utilisateur recherché</summary>
        private const string PathFile = "User.xml";

        public static void Save(Dictionary<String, Brute> brutes)
        {
            try
            {
                XmlTextWriter xml = new XmlTextWriter("Users.xml", System.Text.Encoding.UTF8);
                xml.WriteStartDocument();
                xml.WriteStartElement("Users");
                foreach (Brute brute in brutes.Values)
                {
                    xml.WriteStartElement("Brute");
                    xml.WriteElementString("Name", brute.Name);
                    xml.WriteElementString("Level", Convert.ToString(brute.Level));
                    xml.WriteElementString("Life", Convert.ToString(brute.Life));
                    xml.WriteElementString("Strength", Convert.ToString(brute.Strength));
                    xml.WriteElementString("Agility", Convert.ToString(brute.Agility));
                    xml.WriteElementString("Speed", Convert.ToString(brute.Speed));
                    xml.WriteElementString("Image", Convert.ToString(brute.Image));
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.Close(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static Dictionary<String,Brute> Read()
        {
            Dictionary<String, Brute> brutes = new Dictionary<String, Brute>();
            if(File.Exists("User.xml"))
            {
                XmlTextReader xml = new XmlTextReader("Users.xml");
                while (xml.Read())
                {
                    //  Here we check the type of the node, in this case we are looking for element
                    if (xml.NodeType == XmlNodeType.Element)
                    {
                        //  If the element is "profile"
                        if (xml.Name == "Brute")
                        {
                            xml.Read();
                            String name = xml.ReadElementString("Name");
                            short level = Convert.ToInt16(xml.ReadElementString("Level"));
                            short life = Convert.ToInt16(xml.ReadElementString("Life"));
                            short strength = Convert.ToInt16(xml.ReadElementString("Strength"));
                            short agility = Convert.ToInt16(xml.ReadElementString("Agility"));
                            short speed = Convert.ToInt16(xml.ReadElementString("Speed"));
                            int image = Convert.ToInt32(xml.ReadElementString("Image"));
                            brutes.Add(name,new Brute(name, level, life, strength, agility, speed, image));
                        }
                    }

                }
                xml.Close();
            }
            return brutes;
        }
    }
}
