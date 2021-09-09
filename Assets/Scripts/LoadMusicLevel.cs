using UnityEngine;

public class LoadMusicLevel : MonoBehaviour
{
    public string nameSong;

    void Start()
    {
        AudioManager.sharedInstance.Play(nameSong);
    }
}
