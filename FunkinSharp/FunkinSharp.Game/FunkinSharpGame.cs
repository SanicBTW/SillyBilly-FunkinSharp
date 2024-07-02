using System;
using FunkinSharp.Game.Core;
using FunkinSharp.Game.Core.Cursor;
using FunkinSharp.Game.Funkin;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Framework.Screens;
using SillyBilly.FunkinSharp.Game.Mod;

namespace FunkinSharp.Game
{
    public partial class FunkinSharpGame : FunkinSharpGameBase
    {
        private ScreenStack screenStack = new() { RelativeSizeAxes = Axes.Both };
        public override ScreenStack ScreenStack => screenStack;

        public BasicCursorContainer Cursor { get; protected set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            // This makes the OS Cursor hide and leaves the Game Cursor be the pointer in game
            Window.CursorState |= CursorState.Hidden;
            Children = [screenStack, Cursor = new()];
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(ModInfo.StartingScreen);
        }

        // took me almost a whole day, but im proud of the code so yeah
        // also to know where the cached gradient texture comes from, check IntroScreen
        public void SwitchScreen(FunkinScreen current, FunkinScreen next)
        {
            bool transIn = (next == null);

            Paths.Cache("gradient", out Texture cachedGradient);

            Sprite gradient = new Sprite()
            {
                Texture = cachedGradient,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = (transIn) ? ColourInfo.GradientVertical(Colour4.Black, new Colour4(0, 0, 0, 0)) : ColourInfo.GradientVertical(new Colour4(0, 0, 0, 0), Colour4.Black),
                Rotation = 180
            };
            current.Add(gradient);

            Sprite transBlack = new Sprite()
            {
                Texture = cachedGradient,
                Size = new osuTK.Vector2(GameConstants.WIDTH, GameConstants.HEIGHT + ((transIn) ? 0 : 400)),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Colour4.Black,
            };
            current.Add(transBlack);

            Action<Drawable> update = (Drawable obj) =>
            {
                if (transIn)
                    transBlack.Y = gradient.Y + gradient.Height;
                else
                {
                    gradient.Y += ((transBlack.Height - GameConstants.HEIGHT) / 2) + 50;
                    transBlack.Y = gradient.Y - transBlack.Height;
                }
            };

            if (transIn)
                gradient.Y = transBlack.Y - transBlack.Height;
            else
            {
                gradient.Y = -gradient.Height;
                transBlack.Y = gradient.Y - transBlack.Height + 50;
            }

            gradient.Delay((!transIn) ? 0 : 200).MoveToY(gradient.Height + 50, 600D, Easing.None).OnComplete((_) =>
            {
                current.Remove(gradient, false);
                current.Remove(transBlack, false);
                current.OnUpdate -= update;

                if (next != null)
                {
                    current.Alpha = 0;
                    screenStack.Push(next);
                }
            });

            current.OnUpdate += update;
        }
    }
}
