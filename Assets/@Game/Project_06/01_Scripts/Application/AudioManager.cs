using AudioConductor.Core;
using AudioConductor.Core.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tetris.Common
{
    public class AudioManager : MonoBehaviour
    {
        public class AudioPlayer
        {
            private Conductor _conductor;
            private CueSheetHandle _handle;
            private string _cueName;
            private PlaybackHandle _playback;

            public PlaybackHandle Playback => _playback;

            public AudioPlayer(Conductor conductor, CueSheetHandle handle, string cueName)
            {
                _conductor = conductor;
                _handle = handle;
                _cueName = cueName;
            }

            public void Play(string trackName, bool isLoop = false, float fadeTime = 0)
            {
                 _playback = _conductor.Play(_handle, _cueName, new PlayOptions
                {
                    IsLoop = isLoop,
                    FadeTime = fadeTime,
                    TrackName = trackName
                });
            }

            public void Play(int index, bool isLoop = false, float fadeTime = 0)
            {
                _playback = _conductor.Play(_handle, _cueName, new PlayOptions
                {
                    IsLoop = isLoop,
                    FadeTime = fadeTime,
                    TrackIndex = index,
                });
            }

            public void Stop()
            {
                _conductor.Stop(_playback);
            }

            public void Stop(float fadeTime)
            {
                _conductor.Stop(_playback, fadeTime);
            }

            public void Pause()
            {
                _conductor.Pause(_playback);
            }

            public void Resume()
            {
                _conductor?.Resume(_playback);
            }

            public void Dispose()
            {
                _conductor?.Dispose();
            }
        }

        [SerializeField] private AudioConductorSettings _setting;
        [SerializeField] private CueSheetAsset _sheetAsset;

        private Conductor _conductor;
        private Dictionary<string, AudioPlayer> _audioPlayers;

        public static AudioManager Instance;
        public AudioPlayer BGM { get; private set; }
        public AudioPlayer SE { get; private set; }

        private void Awake()
        {
            CheckInstance();
            Init();
        }

        private void CheckInstance()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            //V2===============================================================
            _conductor = new(_setting);
            CueSheetHandle handle = _conductor.RegisterCueSheet(_sheetAsset);

            BGM = new(_conductor, handle, "BGM");
            SE = new(_conductor, handle, "SE");

            //V1===============================================================
            /*
            AudioConductorInterface.Setup(_setting, OnCueSheetUnused);

            var cueList = _sheetAsset.cueSheet.cueList;

            _BGMController = AudioConductorInterface.CreateController(_sheetAsset, "BGM");
            _SEController = AudioConductorInterface.CreateController(_sheetAsset, "SE");

            BGM = new(_BGMController);
            SE = new(_SEController);*/
        }

        public void StopAll()
        {
            _conductor.StopAll();
        }

        public void StopAll(float fadeTime)
        {
            _conductor.StopAll(fadeTime);
        }

        private void OnDestroy()
        {
            BGM.Dispose();
            SE.Dispose();
        }

        private static void OnCueSheetUnused(CueSheetAsset sheetAsset)
        {
            Resources.UnloadAsset(sheetAsset);
        }
    }
}