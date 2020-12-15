using System.Linq;

namespace RpgGameCore2.Data
{
    /// <summary>
    /// Denotes an item that can be consumed by a player, e.g. apple, alcohol, potions. Can have positive or negative effects upon consumation.
    /// </summary>
    public abstract class Consumable : InventoryItem
    {
        public abstract Effect[] Effects { get; }
        
        public override string ToString()
        {
            return base.ToString() + $"\nЭффекты:\n{string.Join("\n", Effects.Select(x => $"• {x}"))}";
        }
    }
}
