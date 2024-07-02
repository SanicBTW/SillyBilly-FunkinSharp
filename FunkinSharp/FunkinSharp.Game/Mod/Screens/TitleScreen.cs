using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunkinSharp.Game.Core;
using FunkinSharp.Game.Core.Containers;
using FunkinSharp.Game.Funkin;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace SillyBilly.FunkinSharp.Game.Mod.Screens
{
    public partial class TitleScreen : FunkinScreen
    {
        public TitleScreen() { }

        [BackgroundDependencyLoader]
        private void load()
        {
            /*
            Camera camera = new(false);
            camera.AddRange(new Drawable[]
            {
                new Sprite
                {
                    Texture = Paths.GetTexture("Textures/Opening/MENUBGMAIN.png"),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new Sprite
                {
                    Texture = Paths.GetTexture("Textures/Opening/TITLEFACEBACK.png"),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new Sprite
                {
                    Texture = Paths.GetTexture("Textures/Opening/face.png"),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new Sprite
                {
                    Texture = Paths.GetTexture("Textures/Opening/title.png"),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            });
            Add(camera);*/

            Add(new Sprite
            {
                Texture = Paths.GetTexture("Textures/Opening/MENUBGMAIN.png", false),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });

            Add(new Sprite
            {
                Texture = Paths.GetTexture("Textures/Opening/TITLEFACEBACK.png", false),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });

            Add(new Sprite
            {
                Texture = Paths.GetTexture("Textures/Opening/face.png", false, osu.Framework.Graphics.Textures.WrapMode.ClampToEdge, osu.Framework.Graphics.Textures.WrapMode.ClampToEdge, osu.Framework.Graphics.Textures.TextureFilteringMode.Nearest),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Alpha = 0.7f
            });

            Add(new Sprite
            {
                Texture = Paths.GetTexture("Textures/Opening/title.png", false),
                Scale = new osuTK.Vector2(0.8f),
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
            });
        }
    }
}
