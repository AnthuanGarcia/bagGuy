using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    public Sound[] sounds;

    Dictionary<string, int> soundsOrd = new Dictionary<string, int>();

    void Awake()
    {
        if(sharedInstance == null)
            sharedInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        int idx = 0;

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.Clip;
            sound.source.loop = sound.loop;

            sound.source.volume = sound.Volume;
            sound.source.pitch = sound.Pitch;

            soundsOrd.Add(sound.name, idx++);
        }
    }

    public void Play(string name)
    {
        Sound s = sounds[soundsOrd[name]];
        s.source.Play();
    }

    public void Stop(string name, float fadeOut)
    {
        Sound s = sounds[soundsOrd[name]];

        if(s.source.isPlaying)
            StartCoroutine(AudioFade.FadeOut(s.source, fadeOut));
    }

}
