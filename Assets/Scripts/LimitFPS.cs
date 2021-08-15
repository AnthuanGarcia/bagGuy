using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    public int targetFrame = 30;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrame;
    }
}
