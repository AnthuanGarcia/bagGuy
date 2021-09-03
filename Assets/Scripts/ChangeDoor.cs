using UnityEngine;

public class ChangeDoor : MonoBehaviour
{
    public Material targetMaterial;
    GameObject door;
    bool once = true;
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("BigDoor");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && ComposeBakcground.tilesBackground == 10 && once)
        {
            SpriteRenderer spriteDoor = door.GetComponent<SpriteRenderer>();
            spriteDoor.material = targetMaterial;
            door.AddComponent<MooverDoor>();
            once = false;
        }
    }

}
