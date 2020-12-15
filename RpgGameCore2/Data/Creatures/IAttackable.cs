using System;

namespace RpgGameCore2.Data.Creatures
{
    public interface IAttackable
    {
        /// <summary>
        /// Attacks the attackable entity.
        /// </summary>
        /// <param name="attackSubject">Creature that attacks this creature.</param>
        /// <param name="bodyPart">Index of a body part to attack to (0 = head, 1 = body, 2 = legs). -1 means random.</param>
        /// <returns>Amount of dealt damage. If infinity, means the entity is dead.</returns>
        AttackResult Attack(Creature attackSubject, int bodyPart = -1)
        {
            // Default attack mechanics
            var result = new AttackResult {
                Subject = attackSubject,
                Object = this as Creature
            };
            var rng = new Random();

            if (this is Human humanObject) {
                int attackBodyPart = bodyPart == -1 ? rng.Next(0, 3) : bodyPart; // 0 = head, 1 = body, 2 = legs
                int subjectOverallStrength = attackSubject.Strength + humanObject.CalculateStrengthBonus();
                int objectOverallDefense; 
                
                if (attackBodyPart == 0) {
                    objectOverallDefense = humanObject.Defense + humanObject.Helmet?.DefenseBonus ?? 0;
                } else if (attackBodyPart == 1) {
                    objectOverallDefense = humanObject.Defense + humanObject.Chestplate?.DefenseBonus ?? 0;
                } else if (attackBodyPart == 2) {
                    objectOverallDefense = humanObject.Defense + humanObject.Legging?.DefenseBonus ?? 0;
                } else {
                    throw new Exception($"Unknown body part to attack: {attackBodyPart}. Make sure the value is 0, 1 or 2.");
                }

                result.BodyPart = attackBodyPart;
                objectOverallDefense += (humanObject.Sword?.DefenseBonus + humanObject.Necklace1?.DefenseBonus + humanObject.Necklace2?.DefenseBonus) ?? 0;

                // Chance to miss
                var subjAgil = attackSubject.GetOverallAgility();
                var objLuck = humanObject.GetOverallLuck();
                var chanceOfHit = Math.Pow(subjAgil, 2) / objLuck * objLuck / Math.Pow(subjAgil + objLuck, 2) * 2;
                if (objLuck > subjAgil) chanceOfHit *= 0.8;
                if (subjAgil > objLuck) chanceOfHit /= 0.8;
                if (chanceOfHit < 0) chanceOfHit = 0;
                if (chanceOfHit > 1) chanceOfHit = 1;
                var chanceOfMiss = 1 - chanceOfHit;
                var dice = rng.NextDouble();
                if (dice < chanceOfMiss) {
                    // Miss
                    result.IsMissed = true;
                    return result;
                }

                decimal baseDamage = subjectOverallStrength * 2 + rng.Next(-attackSubject.Strength, attackSubject.Strength) * 0.2m;
                decimal blockedDamage = objectOverallDefense * 1.8m + rng.Next(-attackSubject.Defense, attackSubject.Defense) * 0.18m;
                decimal inflictedDamage = baseDamage - blockedDamage;
                if (inflictedDamage < 0) inflictedDamage = 0;

                // Chance to critical attack
                var chanceOfCrit = 0.3 * subjAgil / (20 + subjAgil);
                dice = rng.NextDouble();
                if (dice < chanceOfCrit) {
                    // Crit
                    result.IsCritical = true;
                    decimal critMultiplier = 2.5m * subjAgil / (0.8m * subjAgil + 5);
                    if (critMultiplier > 2.5m) critMultiplier = 2.5m;
                    if (critMultiplier < 1.1m) critMultiplier = 1.1m;
                    result.CritMultiplier = critMultiplier;

                    inflictedDamage *= critMultiplier;
                }

                result.InflictedDamage = inflictedDamage;
                humanObject.CurrentHealth -= (float) inflictedDamage;
                humanObject.CurrentHealth = MathF.Floor(humanObject.CurrentHealth * 100) / 100f;
            } else {
                throw new Exception("Fighting with non-humans is not supported yet.");
            }

            return result;
        }
    }

    public class AttackResult
    {
        public Creature Subject, Object;
        public bool IsSuccessful, IsCritical, IsMissed;
        public decimal InflictedDamage, CritMultiplier;
        public int BodyPart;
    }

    public class FightResult
    {
        public Creature Subject, Object, Winner;
        public bool IsTie;
    }
}
