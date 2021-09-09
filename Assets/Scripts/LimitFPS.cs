using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    public static LimitFPS sharedInstance;
    public int targetFrame = 30;

    void Awake()
    {
        if(sharedInstance == null)
            sharedInstance = this;
        else
        {
            Destroy(sharedInstance);
            return;
        }

        DontDestroyOnLoad(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrame;
    }
}
