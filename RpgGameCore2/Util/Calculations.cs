using System;
using System.Collections.Generic;

namespace RpgGameCore2.Util
{
    public class SGD
    {
        public int Silver, Gold, Diamonds;

        public SGD(int silver = 0, int gold = 0, int diamonds = 0)
        {
            Silver = silver;
            Gold = gold;
            Diamonds = diamonds;
        }

        public override string ToString()
        {
            var l = new List<string>();
            
            if (Silver != 0) l.Add($"{Silver}S");
            if (Gold != 0) l.Add($"{Gold}G");
            if (Diamonds != 0) l.Add($"{Diamonds}D");

            return string.Join(", ", l);
        }
    }
    public static class Calculations
    {
        /// <summary>
        /// Calculates the price of upgrading the strength.
        /// </summary>
        /// <param name="oldStat">Old stat value, e.g. to calculate the price of increasing the strength from 6 to 7, oldStat == 6.</param>
        /// <returns>Tuple consisting of price. Values can be zero.</returns>
        public static SGD StrengthUpgradePrice(int oldStat)
        {
            SGD ret = new SGD(0, 0, 0);

            ret.Silver = (int)(30 + Math.Pow(oldStat - 5, 2.35));
            ret.Gold = oldStat < 20 ? 0 : (int)(5 + Math.Pow(oldStat, 1.75));
            ret.Diamonds = oldStat < 50 ? 0 : (int)(5 + Math.Pow(oldStat - 40, 1.2));
            
            return ret;
        }
        
        /// <summary>
        /// Calculates the price of upgrading the defense.
        /// </summary>
        /// <param name="oldStat">Old stat value, e.g. to calculate the price of increasing the defense from 6 to 7, oldStat == 6.</param>
        /// <returns>Tuple consisting of price. Values can be zero.</returns>
        public static SGD DefenseUpgradePrice(int oldStat)
        {
           SGD ret = new SGD(0, 0, 0);

            ret.Silver = (int)(20 + Math.Pow(oldStat - 5, 2.3));
            ret.Gold = oldStat < 25 ? 0 : (int)(5 + Math.Pow(oldStat, 1.7));
            ret.Diamonds = oldStat < 60 ? 0 : (int)(5 + Math.Pow(oldStat - 50, 1.17));
            
            return ret;
        }
        
        /// <summary>
        /// Calculates the price of upgrading the agility.
        /// </summary>
        /// <param name="oldStat">Old stat value, e.g. to calculate the price of increasing the agility from 6 to 7, oldStat == 6.</param>
        /// <returns>Tuple consisting of price. Values can be zero.</returns>
        public static SGD AgilityUpgradePrice(int oldStat)
        {
            SGD ret = new SGD(0, 0, 0);

            ret.Silver = (int)(15 + Math.Pow(oldStat - 5, 2.25));
            ret.Gold = oldStat < 30 ? 0 : (int)(5 + Math.Pow(oldStat, 1.65));
            ret.Diamonds = oldStat < 75 ? 0 : (int)(5 + Math.Pow(oldStat - 50, 1.15));
            
            return ret;
        }
        
        /// <summary>
        /// Calculates the price of upgrading the luck.
        /// </summary>
        /// <param name="oldStat">Old stat value, e.g. to calculate the price of increasing the luck from 6 to 7, oldStat == 6.</param>
        /// <returns>Tuple consisting of price. Values can be zero.</returns>
        public static SGD LuckUpgradePrice(int oldStat)
        {
            SGD ret = new SGD(0, 0, 0);

            ret.Silver = (int)(10 + Math.Pow(oldStat - 5, 2.2));
            ret.Gold = oldStat < 30 ? 0 : (int)(5 + Math.Pow(oldStat, 1.6));
            ret.Diamonds = oldStat < 75 ? 0 : (int)(5 + Math.Pow(oldStat - 50, 1.11));
            
            return ret;
        }
    }
}
