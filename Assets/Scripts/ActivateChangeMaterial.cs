using UnityEngine;

public class ActivateChangeMaterial : MonoBehaviour
{
    public float Interval = 5f;

    GameObject background;
    ChangeMaterialInterval changeMaterial;

    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background");
        changeMaterial = background.GetComponent<ChangeMaterialInterval>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            changeMaterial.enabled = true;
            changeMaterial.m_Interval = Interval;
        }
    }
}
