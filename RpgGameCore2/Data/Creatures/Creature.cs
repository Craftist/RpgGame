using System.Collections.Generic;

namespace RpgGameCore2.Data.Creatures
{
    /// <summary>
    /// Represents a living creature, such as a person, a dog or an ork.
    /// </summary>
    public class Creature : IAttackable
    {
        public virtual string Name { get; set; } = "Существо";

        /// <summary>
        /// Defines how much health the creature has now. If the creature has 20 health, they cannot attack enemies.
        /// </summary>
        public float CurrentHealth = 100;
        /// <summary>
        /// Defines how much health the creature can have maximum. Increases with stamina.
        /// </summary>
        public float MaxHealth = 100;
        /// <summary>
        /// Defines how much energy the creature has now. Energy defines how long can the creature travel.
        /// </summary>
        public float CurrentEnergy = 100;
        /// <summary>
        /// Defines how much energy the creature can have maximum. Increases with stamina.
        /// </summary>
        public float MaxEnergy = 100;
        
        /// <summary>
        /// The level approximately defines how strong the enemy is. It also defines what items can the creature possess.
        /// </summary>
        public int CurrentLevel = 1;
        /// <summary>
        /// Experience is what the creature gets from various tasks, e.g. completing missions (for a player) or killing other creatures. If enough experience is gotten, the level will be upped.
        /// </summary>
        public int CurrentExp = 0;

        /// <summary>
        /// Strength defines how much damage the creature inflicts on enemies.
        /// </summary>
        public int Strength = 5;
        /// <summary>
        /// Defense defines how much damage the creature can absorb from an enemy.
        /// </summary>
        public int Defense = 5;
        /// <summary>
        /// Agility defines how much critical damage the creature can inflict on an enemy. And also the change of this attack.
        /// </summary>
        public int Agility = 5;
        /// <summary>
        /// Luck defines the change of avoiding the attack of an enemy.
        /// </summary>
        public int Luck = 5;
        /// <summary>
        /// Stamina defines the rate of health replenishing and the maximum amount of health.
        /// </summary>
        public int Stamina = 5;
        
        public List<InventoryItem> Inventory = new List<InventoryItem>(128); // the rest of items is here

        public virtual int GetOverallStrength()
        {
            return Strength;
        }

        public virtual int GetOverallDefense()
        {
            return Defense;
        }

        public virtual int GetOverallAgility()
        {
            return Agility;
        }

        public virtual int GetOverallLuck()
        {
            return Luck;
        }

        public virtual int GetOverallStamina()
        {
            return Stamina;
        }

        public string GetNameStats()
        {
            return $"{Name} (STR {Strength}, DEF {Defense}, AGI {Agility}, LUC {Luck}, STA {Stamina})";
        }
    }
}
