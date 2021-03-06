﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Protocole;

namespace Protocole
{
    public class Brute
    {
        #region Fields

        private String name;
        private short level, life, strength, agility, speed;
        private List<Bonus> bonusList;
        private int image;

        #endregion Fields

        #region Accessors

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public short Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public short Agility
        {
            get { return agility; }
            set { agility = value; }
        }

        public short Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public short Life
        {
            get { return life; }
            set { life = value; }
        }
        
        public short Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Image
        {
            get { return image; }
            set { image = value; }
        }

        public List<Bonus> BonusList
        {
            get { return bonusList; }
            set { bonusList = value; }
        }

        #endregion Accessors

        #region Constructors

        public Brute()
        {
            this.name = null;
            this.level = 0;
            this.life = 0;
            this.strength = 0;
            this.agility = 0;
            this.speed = 0;
            this.bonusList = new List<Bonus>();
            this.image = 0;
        }

        public Brute(String name)
        {
            this.name = name;
            this.level = 0;
            this.life = 0;
            this.strength = 0;
            this.agility = 0;
            this.speed = 0;
            this.bonusList = new List<Bonus>();
            this.image = 0;
        }

        public Brute(String name, short level, short life, short strength, short agility, short speed, int image)
        {
            this.name = name;
            this.level = level;
            this.life = life;
            this.strength = strength;
            this.agility = agility;
            this.speed = speed;
            this.bonusList = new List<Bonus>();
            this.image = image;
        }

        #endregion Constructor

        #region Methods

        public void randomValue()
        {
            Random n = new Random();
            this.level = 1;
            this.life = (short)n.Next(60,70);
            this.strength = (short)n.Next(2, 4);
            this.agility = (short)n.Next(2, 4);
            this.speed = (short)n.Next(2, 4);
            this.image = n.Next(1,13);
            this.BonusList.Add(new Bonus("Sword",0,15,5,5,1));
            this.BonusList.Add(new Bonus("Trident",0,20,-5,5,2));
            this.BonusList.Add(new Bonus("Knife",0,2,15,15,1));
        }

        public String getParam()
        {
            return this.name + ":" + this.level + ":" + this.life + ":" + this.strength + ":" + this.agility + ":" + this.speed + ":" + this.image;
        }

        public override String ToString()
        {
            return "Name : " + this.name + "\nLevel : " + this.level + "\nLife : " + this.life + "\nStrength : " + this.strength + "\nAgility : "
                + this.agility + "\n Speed : " + this.speed + "\n ImageNb : " + this.image + "\n Bonus : " + this.BonusList.Count;
        }

        public void Update()
        {
            this.level++;
            this.life +=5;
            this.strength++;
            this.agility++;
            this.speed++;
        }

        #endregion Methods

    }
}
