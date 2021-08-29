using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooverDoor : MonoBehaviour
{
    public float floatStrength = 1f;
    public float heightWave = 4.5f;
    public bool side;
    float initialPos;
    float sign;

    void Start()
    {
        initialPos = transform.position.y;
        sign = side ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time) * sign;

        transform.position = new Vector2(
            transform.position.x,
            initialPos + sin/heightWave * floatStrength
        );
    }
}
