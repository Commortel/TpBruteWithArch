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
        private const string PathFile = "Users.xml";

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
                    xml.WriteStartElement("BonusList");
                    foreach (Bonus bonus in brute.BonusList)
                    {
                        xml.WriteStartElement("Bonus");
                        xml.WriteElementString("Name", bonus.Name);
                        xml.WriteElementString("Life", Convert.ToString(bonus.Life));
                        xml.WriteElementString("Strength", Convert.ToString(bonus.Strength));
                        xml.WriteElementString("Agility", Convert.ToString(bonus.Agility));
                        xml.WriteElementString("Speed", Convert.ToString(bonus.Speed));
                        xml.WriteElementString("Image", Convert.ToString(bonus.Image));
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
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
            Console.WriteLine(File.Exists("Users.xml"));
            if(File.Exists("Users.xml"))
            {
                XmlTextReader xml = new XmlTextReader("Users.xml");
                while (xml.Read())
                {
                    if (xml.Name.Equals("Brute") && (xml.NodeType == XmlNodeType.Element))
                    {
                        xml.Read();
                        String name = xml.ReadElementString("Name");
                        short level = Convert.ToInt16(xml.ReadElementString("Level"));
                        short life = Convert.ToInt16(xml.ReadElementString("Life"));
                        short strength = Convert.ToInt16(xml.ReadElementString("Strength"));
                        short agility = Convert.ToInt16(xml.ReadElementString("Agility"));
                        short speed = Convert.ToInt16(xml.ReadElementString("Speed"));
                        int image = Convert.ToInt32(xml.ReadElementString("Image"));
                        Brute brute = new Brute(name, level, life, strength, agility, speed, image);
                        if(xml.Name.Equals("BonusList") && (xml.NodeType == XmlNodeType.Element))
                        {
                            XmlReader inner = xml.ReadSubtree();
                            while (inner.Read())
                            {
                                if (inner.Name.Equals("Bonus") && (xml.NodeType == XmlNodeType.Element))
                                {
                                    xml.Read();
                                    name = xml.ReadElementString("Name");
                                    life = Convert.ToInt16(xml.ReadElementString("Life"));
                                    strength = Convert.ToInt16(xml.ReadElementString("Strength"));
                                    agility = Convert.ToInt16(xml.ReadElementString("Agility"));
                                    speed = Convert.ToInt16(xml.ReadElementString("Speed"));
                                    image = Convert.ToInt32(xml.ReadElementString("Image"));
                                    brute.BonusList.Add(new Bonus(name, life, strength, agility, speed, image));
                                }
                            }
                            inner.Close();
                            Console.WriteLine(brute.ToString());
                        }
                        brutes.Add(brute.Name, brute);
                    }

                }
                xml.Close();
            }
            return brutes;
        }
    }
}
