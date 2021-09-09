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
}
