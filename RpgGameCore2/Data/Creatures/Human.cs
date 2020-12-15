using RpgGameCore2.Data.Chestplates;
using RpgGameCore2.Data.Helmets;
using RpgGameCore2.Data.Leggings;
using RpgGameCore2.Data.Necklaces;
using RpgGameCore2.Data.Swords;

namespace RpgGameCore2.Data.Creatures
{
    public class Human : Creature
    {
        public override string Name { get; set; } = "Незнакомец";

        /// <summary>
        /// Gold is the second valuable currency, after gold. Can be earned in rewards to missions or found on enemies or loot crates.
        /// </summary>
        public int Gold = 0;
        /// <summary>
        /// Silver is the most common currency. Can be earned in rewards to missions, found on enemies or loot crates or if the player sells their inventory items.
        /// </summary>
        public int Silver = 100;
        /// <summary>
        /// Diamonds are the most valuable currency. Can be exchanged to gold (1D:15G) and silver (1D:100S).
        /// </summary>
        public int Diamonds = 0;
        
        // Inventory, if applicable
        public Helmet Helmet;
        public Chestplate Chestplate;
        public Legging Legging;
        public Sword Sword;
        public Necklace Necklace1;
        public Necklace Necklace2;

        public override int GetOverallStrength()
        {
            return base.GetOverallStrength() + CalculateStrengthBonus();
        }

        public override int GetOverallDefense()
        {
            return base.GetOverallDefense() + CalculateDefenseBonus();
        }

        public override int GetOverallAgility()
        {
            return base.GetOverallAgility() + CalculateAgilityBonus();
        }

        public override int GetOverallLuck()
        {
            return base.GetOverallLuck() + CalculateLuckBonus();
        }

        public override int GetOverallStamina()
        {
            return base.GetOverallStamina() + CalculateStaminaBonus();
        }

        public int CalculateStrengthBonus()
        {
            return (Helmet?.StrengthBonus + Chestplate?.StrengthBonus + Legging?.StrengthBonus + Sword?.StrengthBonus + Necklace1?.StrengthBonus + Necklace2?.StrengthBonus) ?? 0;
        }

        public int CalculateDefenseBonus()
        {
            return (Helmet?.DefenseBonus + Chestplate?.DefenseBonus + Legging?.DefenseBonus + Sword?.DefenseBonus + Necklace1?.DefenseBonus + Necklace2?.DefenseBonus) ?? 0;
        }

        public int CalculateAgilityBonus()
        {
            return (Helmet?.AgilityBonus + Chestplate?.AgilityBonus + Legging?.AgilityBonus + Sword?.AgilityBonus + Necklace1?.AgilityBonus + Necklace2?.AgilityBonus) ?? 0;
        }

        public int CalculateLuckBonus()
        {
            return (Helmet?.LuckBonus + Chestplate?.LuckBonus + Legging?.LuckBonus + Sword?.LuckBonus + Necklace1?.LuckBonus + Necklace2?.LuckBonus) ?? 0;
        }

        public int CalculateStaminaBonus()
        {
            return (Helmet?.StaminaBonus + Chestplate?.StaminaBonus + Legging?.StaminaBonus + Sword?.StaminaBonus + Necklace1?.StaminaBonus + Necklace2?.StaminaBonus) ?? 0;
        }
    }
}
