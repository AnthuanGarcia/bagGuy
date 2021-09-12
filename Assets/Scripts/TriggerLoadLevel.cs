using UnityEngine;

public class TriggerLoadLevel : MonoBehaviour
{
    public LevelLoader loader;
    public string nameSongToStop;
    public bool verifyTiles = false;

    void Start()
    {
        if(loader == null)
            loader = gameObject.AddComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(verifyTiles)
            {
                if(ComposeBakcground.tilesBackground == 10)
                {
                    loader.LoadNextLevel();
                    AudioManager.sharedInstance.Stop(nameSongToStop, 2f);
                    PlayerMovement.canMove = false;
                }
            }
            else
            {
                loader.LoadNextLevel();
                AudioManager.sharedInstance.Stop(nameSongToStop, 2f);
                PlayerMovement.canMove = false;
            }
        }
    }
}
