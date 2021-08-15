using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    public Text fpsDisplay;
    int framesPassed;
    float fpsTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsTotal += fps;
        framesPassed++;
        fpsDisplay.text = "FPS: " + (fpsTotal / framesPassed);
    }
}
