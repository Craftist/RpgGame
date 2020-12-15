namespace RpgGameCore2.Data
{
    /// <summary>
    /// Denotes an item that can be worn on a player, e.g., helmet, necklace, sword.
    /// </summary>
    public abstract class Wearable : InventoryItem
    {
        /// <summary>
        /// Adds itself to the holder strength as a bonus.
        /// </summary>
        public int StrengthBonus;
        /// <summary>
        /// Adds itself to the holder defense as a bonus.
        /// </summary>
        public int DefenseBonus;
        /// <summary>
        /// Adds itself to the holder agility as a bonus.
        /// </summary>
        public int AgilityBonus;
        /// <summary>
        /// Adds itself to the holder luck as a bonus.
        /// </summary>
        public int LuckBonus;
        /// <summary>
        /// Adds itself to the holder stamina as a bonus.
        /// </summary>
        public int StaminaBonus;
    }
}
