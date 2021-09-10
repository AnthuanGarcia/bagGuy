using UnityEngine;

public class PlayMusicTrigger : MonoBehaviour
{
    public string nameSong;
    public float fadeIn = 5.0f;
    [Range(0f, 1f)]public float volume = 1f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            AudioManager.sharedInstance.Play(nameSong, fadeIn, volume);
            GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
}
