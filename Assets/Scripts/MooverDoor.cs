using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooverDoor : MonoBehaviour
{
    public float floatStrength = 1f;
    public float heightWave = 4.5f;
    public bool side;
    float initialPos;

    void Start()
    {
        initialPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time);
        sin *= side ? 1 : -1;

        transform.position = new Vector2(
            transform.position.x,
            initialPos + sin/heightWave * floatStrength
        );
    }
}
