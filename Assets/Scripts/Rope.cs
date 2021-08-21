using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hookInit;
    public Rigidbody2D hookEnd;
    public GameObject[] prefabSegments;
    public int numLinks = 5;

    // Start is called before the first frame update
    void Start()
    {
        GeneralRope();
    }

    void GeneralRope()
    {
        GameObject newSeg = null;
        Rigidbody2D prevBod = hookInit;

        for(int i = 0; i < numLinks; i++)
        {
            newSeg = Instantiate(prefabSegments[0]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }

        newSeg.GetComponent<HingeJoint2D>().connectedBody = hookEnd;
    }
}
