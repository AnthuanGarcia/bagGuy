using UnityEngine;

public class LoadMusicLevel : MonoBehaviour
{
    public string nameSong;
    public float fadeInTime = 0.0f;
    [Range(0f, 1f)]public float volume = 1f;

    void Start()
    {
        if(Mathf.Approximately(fadeInTime, 0))
            AudioManager.sharedInstance.Play(nameSong);
        else
            AudioManager.sharedInstance.Play(nameSong, fadeInTime, volume);
    }
}
