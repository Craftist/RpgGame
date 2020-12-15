using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RpgGameCore2.Data.Consumables.Drinks;
using RpgGameCore2.Serialization;

namespace RpgGameCore2.Screens
{
    public class MainSquareScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "Главная площадь";
        public override string Text { get; set; } = "";

        public override List<ScreenAction> Actions { get; }

        public MainSquareScreen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => "Таверна",
                    NextScreen = () => Game.Screens.Tavern
                },
                new ScreenAction {
                    Description = () => "Лекарня"
                },
                new ScreenAction {
                    Description = () => "Мастер Шу"
                },
                new ScreenAction {
                    Description = () => "Рынок"
                },
                new ScreenAction {
                    Description = () => "Библиотека"
                },
                new ScreenAction {
                    Description = () => "Лес"
                },
                new ScreenAction {
                    Description = () => "Инвентарь",
                    NextScreen = () => Game.Screens.Inventory
                },
                new CheatScreenAction {
                    Description = () => "Получить хабар",
                    BeforeAction = delegate {
                        Game.AddItems(new Ale(), new Whiskey(), new Samogon());
                    }
                }
            };
        }
    }
}

