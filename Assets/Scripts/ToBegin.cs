using UnityEngine;

public class ToBegin : MonoBehaviour
{
    public float time = 3f;
    float transcurred = 0f;

    // Update is called once per frame
    void Update()
    {
        if(transcurred > time)
            Application.Quit();

        transcurred += Time.deltaTime;
    }
}
