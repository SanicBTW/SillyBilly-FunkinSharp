using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunkinSharp.Game;
using FunkinSharp.Game.Core;
using FunkinSharp.Game.Funkin;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Video;
using osu.Framework.Screens;

namespace SillyBilly.FunkinSharp.Game.Mod.Screens
{
    public partial class TestScreen : FunkinScreen
    {
        public TestScreen()
        {
            Add(new Box
            {
                Colour = Colour4.Violet,
                RelativeSizeAxes = Axes.Both,
            });
            Add(new SpriteText
            {
                Y = 20,
                Text = "Main Screen",
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = FontUsage.Default.With(size: 40)
            });

            OnActionPressed += introScreen_OnActionPressed;
        }

        private void introScreen_OnActionPressed(FunkinAction action)
        {
            if (action == FunkinAction.CONFIRM)
            {
                SwitchScreen(new TestScreen());
            }
        }
    }

    public partial class TestScreen2 : FunkinScreen
    {
        private Track videoAudio;
        private Video playingVideo;
        private bool manualPause = false;

        public TestScreen2()
        {
            videoAudio = Paths.GetTrack("Videos/SO_STAY_FINAL.mp4"); // THIS plays the VIDEO sound
            videoAudio.Looping = true;
            videoAudio.Start();

            Add(playingVideo = new Video(Paths.GetStream("Videos/SO_STAY_FINAL.mp4"))); // THIS plays the VIDEO WITHOUT sound
            OnActionPressed += introScreen_OnActionPressed;
        }

        private void introScreen_OnActionPressed(FunkinAction action)
        {
            if (action == FunkinAction.CONFIRM)
                manualPause = !manualPause;

            if (action == FunkinAction.UI_LEFT && videoAudio.CurrentTime >= 0)
                videoAudio.Seek(videoAudio.CurrentTime - 1000);

            if (action == FunkinAction.UI_RIGHT && videoAudio.CurrentTime <= videoAudio.Length)
                videoAudio.Seek(videoAudio.CurrentTime + 1000);
        }

        protected override void Update()
        {
            base.Update();

            if (playingVideo != null)
            {
                if (playingVideo.Buffering || manualPause)
                    videoAudio.Stop();

                if (!playingVideo.Buffering && !videoAudio.IsRunning && !manualPause)
                    videoAudio.Start();

                playingVideo.PlaybackPosition = videoAudio.CurrentTime;

                if (playingVideo.PlaybackPosition >= playingVideo.Duration)
                {
                    Content.Remove(playingVideo, true);
                    videoAudio.Stop();
                    playingVideo = null;
                    SwitchScreen(new TestScreen());
                }
            }
        }
    }
}
