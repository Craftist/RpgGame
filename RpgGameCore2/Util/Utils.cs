using System;
using System.Text;
using RpgGameCore2.Data.Creatures;

namespace RpgGameCore2.Util
{
    public static class Utils
    {
        public static class ConsoleColorStr
        {
            public static string Black = "\u001b[30m";
            public static string Blue = "\u001b[34;1m";
            public static string Cyan = "\u001b[36;1m";
            public static string DarkBlue = "\u001b[34m";
            public static string DarkCyan = "\u001b[36m";
            public static string DarkGray = "\u001b[30;1m";
            public static string DarkGreen = "\u001b[32m";
            public static string DarkMagenta = "\u001b[35m";
            public static string DarkRed = "\u001b[31m";
            public static string DarkYellow = "\u001b[33m";
            public static string Gray = "\u001b[37m";
            public static string Green = "\u001b[32;1m";
            public static string Magenta = "\u001b[35;1m";
            public static string Red = "\u001b[31;1m";
            public static string White = "\u001b[37;1m";
            public static string Yellow = "\u001b[33;1m";
            public static string Reset = "\u001b[0m";

            public static string FromRgbFore(int r, int g, int b)
            {
                return FromRgbFore(r << 16 | g << 8 | b);
            }

            public static string FromRgbFore(int color)
            {
                return $"\u001b[38;5;{WorldMap.ApproximateColor(color)}m";
            }

            public static string FromRgbBack(int r, int g, int b)
            {
                return FromRgbBack(r << 16 | g << 8 | b);
            }

            public static string FromRgbBack(int color)
            {
                return $"\u001b[48;5;{WorldMap.ApproximateColor(color)}m";
            }
        }

        public static void PrintStats(Creature creature)
        {
            Game.Write($"♥ {(int) creature.CurrentHealth}/{creature.MaxHealth} ", ConsoleColor.Red);
            Game.Write($"⚡ {(int) creature.CurrentEnergy}/{creature.MaxEnergy} ", ConsoleColor.Yellow);

            if (creature is Human human) {
                Game.Write($"● {human.Silver} ", System.ConsoleColor.DarkGray);
                Game.Write($"● {human.Gold} ", System.ConsoleColor.Yellow);
                Game.Write($"💎 {human.Diamonds}", System.ConsoleColor.Cyan);
            }
        }

        public static string FormatStats(Creature creature)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append($"\u001b[31;1m♥ {(int) creature.CurrentHealth}/{creature.MaxHealth} ");
            sb.Append($"\u001b[33;1m⚡ {(int) creature.CurrentEnergy}/{creature.MaxEnergy} ");

            if (creature is Human human) {
                sb.Append($"\u001b[30;1m● {human.Silver} ");
                sb.Append($"\u001b[33;1m● {human.Gold} ");
                sb.Append($"\u001b[36;1m💎 {human.Diamonds}");
            }

            sb.Append(ConsoleColorStr.Reset);
            
            return sb.ToString();
        }
    }
}
