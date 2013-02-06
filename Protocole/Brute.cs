using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Serveur
{
    public class Brute
    {
        #region Fields

        private String name;
        private int level, life, strength, agility, speed;
        private List<Bonus> bonusList;
        private Bitmap image;

        #endregion Fields

        #region Accessors

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Agility
        {
            get { return agility; }
            set { agility = value; }
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public int Life
        {
            get { return life; }
            set { life = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        internal List<Bonus> BonusList
        {
            get { return bonusList; }
        }

        public Bitmap Image
        {
            get { return image; }
            set { image = value; }
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
        }

        #endregion Constructor

        #region Methods

        public void randomValue()
        {
            this.level = 1;
            this.life = 10;
            this.strength = 10;
            this.agility = 10;
            this.speed = 10;
        }

        public String getParam()
        {
            return ":" + this.name + ":" + this.level + ":" + this.life + ":" + this.strength + ":" + this.agility + ":" + this.speed;
        }

        public override String ToString()
        {
            return "Name : " + this.name + "\nLevel : " + this.level + "\nLife : " + this.life + "\nStrength : " + this.strength + "\nAgility : " + this.agility + "\n Speed : " + this.speed;
        }

        #endregion Methods
    }
}
