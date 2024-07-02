using FunkinSharp.Game.Funkin;
using SillyBilly.FunkinSharp.Game.Mod.Screens;

namespace SillyBilly.FunkinSharp.Game.Mod
{
    public static class ModInfo
    {
        public static string ResourcesPrefix => "Mod/";
        public static FunkinScreen StartingScreen => new IntroScreen();
    }
}
