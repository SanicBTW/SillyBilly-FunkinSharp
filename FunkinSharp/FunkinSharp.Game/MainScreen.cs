using FunkinSharp.Game.Funkin;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

namespace FunkinSharp.Game
{
    public partial class MainScreen : FunkinScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Violet,
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText
                {
                    Y = 20,
                    Text = "Main Screen",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                }
            };
        }
    }
}
