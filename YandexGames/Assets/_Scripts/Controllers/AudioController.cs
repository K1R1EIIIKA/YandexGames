using System.Collections;
using _Scripts.EventsLogic;
using _Scripts.EventsLogic.Events;
using _Scripts.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Controllers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private Sound.Sound[] _sounds;

        private readonly SoundName[] _mainThemes = { SoundName.MainTheme1, SoundName.MainTheme2, SoundName.MainTheme3 };
        private Coroutine _musicRoutine;

        public static AudioController Instance { get; private set; }

        private void Awake()
        {
            foreach (var sound in _sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
                sound.Source.volume = sound.Volume;
                sound.Source.loop = sound.IsLoop;
                sound.Source.pitch = sound.Pitch;
            }

            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);

            PlayMusicLoop();
        }

        private void OnEnable()
        {
            EventBus<OnMusicSettingsChanged>.OnEvent += OnMusicSettingsChanged;
            EventBus<OnSoundSettingsChanged>.OnEvent += OnSoundSettingsChanged;
        }

        private void OnDisable()
        {
            EventBus<OnMusicSettingsChanged>.OnEvent -= OnMusicSettingsChanged;
            EventBus<OnSoundSettingsChanged>.OnEvent -= OnSoundSettingsChanged;
        }

        private void OnMusicSettingsChanged(OnMusicSettingsChanged @event)
        {
            if (@event.IsMusicEnabled)
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Type == SoundType.Music)
                    {
                        sound.Source.volume = sound.Volume;
                        // sound.Source.Play();
                    }
                }
            }
            else
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Type == SoundType.Music)
                    {
                        sound.Source.volume = 0f;
                        // sound.Source.Stop();
                    }
                }
            }
        }

        private void OnSoundSettingsChanged(OnSoundSettingsChanged @event)
        {
            if (@event.IsSoundEnabled)
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Type == SoundType.SFX)
                        sound.Source.volume = sound.Volume;
                }
            }
            else
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Type == SoundType.SFX)
                        sound.Source.volume = 0f;
                }
            }
        }

        private Sound.Sound FindSound(SoundName soundName)
        {
            foreach (var sound in _sounds)
            {
                if (sound.Name == soundName)
                {
                    return sound;
                }
            }

            Debug.LogWarning($"Sound {soundName} not found!");
            return null;
        }

        public void PlaySound(SoundName soundName)
        {
            var sound = FindSound(soundName);
            if (sound != null)
            {
                sound.Source.Play();
            }
        }

        public void StopSound(SoundName soundName)
        {
            var sound = FindSound(soundName);
            if (sound != null)
            {
                sound.Source.Stop();
            }
        }

        public void PlayMusicLoop()
        {
            if (_musicRoutine != null)
                StopCoroutine(_musicRoutine);

            _musicRoutine = StartCoroutine(MusicLoopRoutine());
        }

        private IEnumerator MusicLoopRoutine()
        {
            PlaySound(SoundName.MainTheme1);
            var mainTheme1 = FindSound(SoundName.MainTheme1);
            yield return new WaitForSeconds(mainTheme1?.Clip.length ?? 0);

            while (true)
            {
                var nextTheme = _mainThemes[Random.Range(0, _mainThemes.Length)];
                PlaySound(nextTheme);
                var sound = FindSound(nextTheme);
                yield return new WaitForSeconds(sound?.Clip.length ?? 0);
            }
        }
    }
}