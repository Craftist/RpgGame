using System;
using System.Collections.Generic;
using System.Linq;
using RpgGameCore2.Data.Creatures;
using RpgGameCore2.Util;

namespace RpgGameCore2.Screens
{
    public class FightScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "Драка";
        public override string Text { get; set; } = "";

        public List<string> History;

        public override List<ScreenAction> Actions { get; }
        public Action<FightResult> ActionAfterFight;

        public Creature Enemy;

        public override void OnShow()
        {
            Text = $"Драка с {Enemy.Name}: {Utils.FormatStats(Enemy)}\n{(History.Count > 5 ? "...\n" : "")}{string.Join("\n", History.TakeLast(5))}";
        }

        public static string FormatReport(AttackResult result)
        {
            var report = $"{result.Subject.Name} ударил {result.Object.Name} по {new[] { "голове", "туловищу", "ногам" }[result.BodyPart]}, ";
            if (result.IsMissed) {
                report += "но промахнулся.";
            } else {
                report += $"нанеся {(result.IsCritical ? "критический " : "")}удар в {result.InflictedDamage} урона.";
            }

            return report;
        }

        public FightScreen(Creature enemy)
        {
            Enemy = enemy;
            History = new List<string>();
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => "Ударить по голове",
                    BeforeAction = delegate { DefaultAttack(enemy, 0); }
                },
                new ScreenAction {
                    Description = () => "Ударить по туловищу",
                    BeforeAction = delegate { DefaultAttack(enemy, 1); }
                },
                new ScreenAction {
                    Description = () => "Ударить по ногам",
                    BeforeAction = delegate { DefaultAttack(enemy, 2); }
                }
            };
        }

        private void DefaultAttack(Creature enemy, int bodyPart = -1)
        {
            var myResult = (enemy as IAttackable).Attack(Game.Player, bodyPart);
            var myReport = FormatReport(myResult);
            var enemyResult = (Game.Player as IAttackable).Attack(enemy);
            var enemyReport = FormatReport(enemyResult);
            History.Add($"{myReport}\n{enemyReport}");

            if (Game.Player.CurrentHealth < 1 && enemy.CurrentHealth < 1) {
                var fightResult = new FightResult { Subject = Game.Player, Object = enemy };
                fightResult.IsTie = true;
                ActionAfterFight?.Invoke(fightResult);
            } else if (Game.Player.CurrentHealth < 1) {
                var fightResult = new FightResult { Subject = Game.Player, Object = enemy };
                fightResult.Winner = fightResult.Object;
                ActionAfterFight?.Invoke(fightResult);
                Game.GoTo(Game.Screens.GameOver);
            } else if (enemy.CurrentHealth < 1) {
                var fightResult = new FightResult { Subject = Game.Player, Object = enemy };
                fightResult.Winner = fightResult.Subject;
                ActionAfterFight?.Invoke(fightResult);
            }
        }
    }
}
