using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_DD
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
        Action OnFinishedPlaying { get; set; }
    }
}
