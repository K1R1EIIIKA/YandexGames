using System;
using _Scripts.Controllers;
using UnityEngine;

namespace _Scripts.Sound
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private SoundName _name;
        [SerializeField] private SoundType _type;

        public SoundName Name => _name;
        public SoundType Type => _type;
        [Range(0.1f, 3f)] public float Pitch = 1f;

        [NonSerialized]
        public AudioSource Source;
        public AudioClip Clip;
        [Range(0f, 1f)] public float Volume = 1f;
        public bool IsLoop;
    }
}