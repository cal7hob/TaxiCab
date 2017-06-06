using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TinHead_Developer
{
    [System.Serializable]
    class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1.0f)]
        public float Volume = 1.0f;

        [Range(0f, 1.0f)]
        public float Pitch;

        [Range(0.0f, 1.0f)]
        public float RandomVolume = 0.0f;

        [Range(0.0f, 1.0f)]
        public float RandomPitch = 0.0f;

        public bool PlayOnAwake = false;
        public bool Loop=false;



        private AudioSource source;

        public void SetSource(AudioSource _Source)
        {
            source = _Source;
            source.volume = Volume * (1 + Random.Range(-RandomVolume / 2.0f, RandomVolume / 2.0f));
            source.pitch = Pitch * (1 + Random.Range(-RandomPitch / 2.0f, RandomPitch / 2.0f));
            source.loop = Loop;
            source.playOnAwake = PlayOnAwake;
            source.clip = clip;

        }

        public void Play()
        {
            source.Play();
        }

    }

    public class SoundManager : SingletonPattern<SoundManager>
    {
        [SerializeField]
        Sound[] SoundList;

        public void Start()
        {
            for (int i = 0; i < SoundList.Length; i++)
            {
                GameObject _go = new GameObject("Sound_FX_"+SoundList[i].name);
                _go.transform.SetParent(this.transform);
                SoundList[i].SetSource(_go.AddComponent<AudioSource>());
            }
        }
        public void PlaySound(string name)
        {
            for (int i = 0; i < SoundList.Length; i++)
            {
                if (SoundList[i].name == name)
                {
                    SoundList[i].Play();
                    return;
                }
            }
           
            Debug.LogWarning("SoundManager: Sound not found in the desired list");
        }
    }
}