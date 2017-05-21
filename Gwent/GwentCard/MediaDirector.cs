using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Media;

namespace nGwentCard
{
    public class MediaDirector: IDisposable
    {
        private List<string> PlayList { get; set; }
        private MediaPlayer Player { get; set; }
        private int CurrMusic { get; set; }
        public MediaDirector()
        {
            PlayList = new List<string>();
            Player = new MediaPlayer();
            Player.Volume = 0.1;
            foreach (string BgMusicName in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + 
                "Music/" + "Background", "*.mp3"))
            {
                PlayList.Add(BgMusicName);
            }
            if (PlayList.Count > 0)
            {
                Random rnd = new Random();
                int FirstTrack = rnd.Next(PlayList.Count);
                Player.Open(new Uri(PlayList[FirstTrack]));
                Player.Play();
                CurrMusic = FirstTrack;
            }
            Player.MediaEnded += GetNextMusic;        
        }

        public void GetNextMusic(object sender, EventArgs args)
        {
            CurrMusic++;
            if (CurrMusic >= PlayList.Count) CurrMusic = 0;
            Player.Open(new Uri(PlayList[CurrMusic]));
            Player.Play();
        }

        public void Dispose()
        {
            Player.Close();
        }

    }
}
