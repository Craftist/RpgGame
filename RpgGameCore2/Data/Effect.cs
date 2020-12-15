using System;
using System.Collections.Generic;
using System.Text;

namespace RpgGameCore2.Data
{
    public class Effect
    {
        /// <summary>
        /// The type of the effect.
        /// </summary>
        public EffectType Type;

        /// <summary>
        /// The stat target of the effect.
        /// </summary>
        public EffectTarget Target;

        /// <summary>
        /// The amount of the effect.
        /// </summary>
        public decimal Amount;

        /// <summary>
        /// The duration of the effect in seconds. After that much realtime seconds the effect wears out.
        /// </summary>
        public int Duration;

        /// <summary>
        /// If true, the effect is repeated (applied again) each Interval seconds. If false, the effect is contant.
        /// </summary>
        public bool IsRepeated;

        /// <summary>
        /// If IsRepeated == true, then Interval indicated how often the effect will be reapplied (the less - the more often).
        /// </summary>
        public int Interval;

        public static string[] TargetNames = {
            "к силе", "к защите", "к умению", "к удаче", "к максимальному здоровью",
            "серебра", "золота", "алмазов",
            "здоровье|здоровья|здоровья", "энергия|энергии|энергии", "уровень|уровня|уровней", "EXP"
        };

        public static int GetDuration(int seconds = 0, int minutes = 0, int hours = 0, int days = 0)
        {
            return seconds + minutes * 60 + hours * 3600 + days * 3600 * 24;
        }

        public static string Conjugate(string[] words, decimal amount)
        {
            int intAmount = (int) amount;
            if (Math.Abs(intAmount - amount) > 0.00001m || intAmount % 10 == 1 && intAmount != 11) {
                return words[0];
            }

            if (intAmount % 10 >= 2 && intAmount % 10 <= 4 && !(intAmount >= 12 && intAmount <= 14)) {
                return words[1];
            }

            return words[2];
        }

        public static string DeclineTime(int seconds, bool genitive = false)
        {
            var secondsStr = genitive ? new[] { "секунды", "секунд", "секунд" } : new[] { "секунду", "секунды", "секунд" };
            var minutesStr = genitive ? new[] { "минуты", "минут", "минут" } : new[] { "минуту", "минуты", "минут" };
            var hoursStr = genitive ? new[] { "часа", "часов", "часов" } : new[] { "час", "часа", "часов" };
            var daysStr = genitive ? new[] { "дня", "дней", "дней" } : new[] { "день", "дня", "дней" };

            string conjugateWithValue(string[] words, int amount)
            {
                if (amount == 0) return "";
                return $"{amount} {Conjugate(words, amount)} "; // a space at the end of the string is intentional
            }

            if (seconds < 60) return $"{conjugateWithValue(secondsStr, seconds)}".Trim();

            int minutes = seconds / 60;
            seconds %= 60;

            if (minutes < 60) return $"{conjugateWithValue(minutesStr, minutes)}{conjugateWithValue(secondsStr, seconds)}".Trim();

            int hours = minutes / 60;
            minutes %= 60;

            if (hours < 24) return $"{conjugateWithValue(hoursStr, hours)}{conjugateWithValue(minutesStr, minutes)}{conjugateWithValue(secondsStr, seconds)}".Trim();

            int days = hours / 24;
            hours %= 24;

            return $"{conjugateWithValue(daysStr, days)}{conjugateWithValue(hoursStr, hours)}{conjugateWithValue(minutesStr, minutes)}{conjugateWithValue(secondsStr, seconds)}".Trim();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Amount = Math.Floor(Amount * 10000) / 10000.0m;

            if (Type == EffectType.Add) {
                sb.Append(Amount >= 0 ? "+" : "").Append((int) Amount).Append(' ');
                string target = TargetNames[(int) Target];
                if (target.Contains("|")) {
                    string[] split = target.Split('|');
                    sb.Append(Conjugate(split, Amount));
                } else {
                    sb.Append(target);
                }
            } else {
                int percentAdd = (int) ((Math.Abs(Amount) - 1) * 100);
                sb.Append(Amount >= 0 ? "+" : "-").Append(percentAdd).Append("% ");
                string target = TargetNames[(int) Target];
                if (target.Contains("|")) {
                    string[] split = target.Split('|');
                    sb.Append(Conjugate(split, percentAdd));
                } else {
                    sb.Append(target);
                }
            }

            if (IsRepeated) {
                sb.Append($" раз в {DeclineTime(Interval)}");
            }

            sb.Append($" в течение {DeclineTime(Duration, true)}");

            return sb.ToString();
        }
    }

    public enum EffectType : sbyte
    {
        Add, Multiply
    }

    public enum EffectTarget : sbyte
    {
        Strength, Defense, Agility, Luck, Health,
        Silver, Gold, Diamonds,
        CurrentHealth, CurrentEnergy, Level, Exp
    }
}
