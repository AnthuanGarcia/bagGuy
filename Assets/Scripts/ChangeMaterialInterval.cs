using UnityEngine;

public class ChangeMaterialInterval : MonoBehaviour
{
    public float m_Interval = 0.5f;
    public SpriteRenderer spriteRenderer;
    public Material[] m_Materials;

    int idx = 0;
    float time = 0;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if(time >= m_Interval)
            {
                time = 0;
                spriteRenderer.material = m_Materials[idx];
                idx++;

                if(idx >= m_Materials.Length)
                    idx = 0;
            }

            time += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
            start = true;
    }
}
