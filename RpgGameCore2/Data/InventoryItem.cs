using RpgGameCore2.Util;

namespace RpgGameCore2.Data
{
    public abstract class InventoryItem
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract SGD Price { get; }
        public abstract Rarity Rarity { get; }

        public static readonly string[] RarityNames = {
            $"{Utils.ConsoleColorStr.Reset}Обычный",
            $"{Utils.ConsoleColorStr.Green}Редкий{Utils.ConsoleColorStr.Reset}",
            $"{Utils.ConsoleColorStr.Cyan}Эпический{Utils.ConsoleColorStr.Reset}",
            $"{Utils.ConsoleColorStr.Yellow}Легендарный{Utils.ConsoleColorStr.Reset}",
            $"{Utils.ConsoleColorStr.Red}БОЖЕСТВЕННЫЙ!!!{Utils.ConsoleColorStr.Reset}"
        };

        public override string ToString()
        {
            return $"{Description}\nСтоимость: {Price}\nРедкость: {RarityNames[(int) Rarity]}";
        }
    }

    public enum Rarity : sbyte
    {
        Common,
        Rare,
        Epic,
        Legendary,
        Godlike
    }
}
