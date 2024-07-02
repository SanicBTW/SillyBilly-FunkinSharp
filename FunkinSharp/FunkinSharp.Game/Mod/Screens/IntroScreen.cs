using FunkinSharp.Game;
using FunkinSharp.Game.Core;
using FunkinSharp.Game.Funkin;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Video;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using FunkinSharp.Game.Core.Utils;
using osu.Framework.Screens;

namespace SillyBilly.FunkinSharp.Game.Mod.Screens
{
    public partial class IntroScreen : FunkinScreen
    {
        private Track videoAudio;
        private Video video;

        public IntroScreen() { }

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            // precache bs
            Image<Rgba32> image = new Image<Rgba32>(GameConstants.WIDTH, GameConstants.HEIGHT);

            Texture gradient = renderer.CreateTexture(GameConstants.WIDTH, GameConstants.HEIGHT, true);

            for (int i = 0; i < GameConstants.WIDTH; ++i)
            {
                for (int j = 0; j < GameConstants.HEIGHT; ++j)
                {
                    float brightness = (float)i / (GameConstants.WIDTH - 1);
                    float brightness2 = (float)j / (GameConstants.HEIGHT - 1);
                    image[i, j] = new Rgba32(
                        (byte)(128 + (1 + brightness - brightness2) / 2 * 127),
                        (byte)(128 + (1 + brightness2 - brightness) / 2 * 127),
                        (byte)(128 + (brightness + brightness2) / 2 * 127),
                        255);
                }
            }

            gradient.SetData(new TextureUpload(image));
            Paths.Cache("gradient", gradient);

            videoAudio = Paths.GetTrack("Videos/intro.mp4"); // THIS plays the VIDEO sound
            videoAudio.Start();

            Add(video = new Video(Paths.GetStream("Videos/intro.mp4"))); // THIS plays the VIDEO without sound
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            CursorVisible = false;
        }

        protected override void Update()
        {
            base.Update();

            if (video != null && video.PlaybackPosition >= video.Duration)
            {
                videoAudio.Dispose();
                Content.Remove(video, true);
                video = null;
                SwitchScreen(new TestScreen());
            }
        }
    }
}
