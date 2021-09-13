using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    public static LimitFPS sharedInstance;
    public int targetFrame = 30;
    public bool vSync = true;
    [Range(1, 4)] public int vSyncCounter = 1;

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

        if(vSync)
            QualitySettings.vSyncCount = vSyncCounter;
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrame;
        }
    }
}
