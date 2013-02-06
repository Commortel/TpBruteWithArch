using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serveur
{
    class Bonus
    {
        #region Fields

        private String name;
        private int life, strength, agility, speed;

        #endregion Fields

        #region Accessors

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

        #endregion Accessors

        #region Constructors

        public Bonus()
        {
            this.name = null;
            this.life = 0;
            this.strength = 0;
            this.agility = 0;
            this.speed = 0;
        }

        public Bonus(String name, int life, int strength, int agility, int speed)
        {
            this.name = name;
            this.life = life;
            this.strength = strength;
            this.agility = agility;
            this.speed = speed;
        }

        #endregion Constructors
    }
}
