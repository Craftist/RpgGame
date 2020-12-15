using System;
using System.Collections.Generic;
using System.Reflection;
using RpgGameCore2.Util;

namespace RpgGameCore2.Screens
{
    public class GameOverScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = false;

        public override string LocationName { get; set; } = "";
        public override string Text { get; set; } = $@"{Utils.ConsoleColorStr.Red}  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
                                                     ░                   {Utils.ConsoleColorStr.Reset}";

        public override List<ScreenAction> Actions { get; }

        public GameOverScreen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => "Start a new game",
                    BeforeAction = delegate {
                        System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }
                },
                new ScreenAction {
                    Description = () => "Exit 🚪",
                    BeforeAction = () => Environment.Exit(0)
                }
            };
        }
    }
}
