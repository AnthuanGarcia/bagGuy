using System.Collections;
using UnityEngine;

public static class AudioFade
{
    public static IEnumerator FadeOut(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;

        while(source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource source, float fadeTime, float volume)
    {
        float startVolume = 0.2f;

        source.volume = 0;
        source.Play();

        while(source.volume < volume)
        {
            source.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        source.volume = volume;
    }
}
