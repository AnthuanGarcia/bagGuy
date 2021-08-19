using UnityEngine;
using Cinemachine;

public class BackScreenSize : MonoBehaviour
{
    public float followSpeed = 3f;
    CinemachineVirtualCamera vCam;

    // Start is called before the first frame update
    void Start()
    {
        float width = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        transform.localScale = new Vector2(width - 1.5f, 5f);

        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        /*transform.position = Vector2.MoveTowards(
            transform.position,
            vCam.transform.position,
            Time.deltaTime * followSpeed
        );*/

        transform.position = new Vector2(
            vCam.transform.position.x,
            vCam.transform.position.y
        );
    }

    /*void FixedUpdate()
    {
        transform.position = new Vector2(
            Mathf.Lerp(transform.position.x, Camera.main.transform.position.x, Time.deltaTime * followSpeed),
            Mathf.Lerp(transform.position.y, Camera.main.transform.position.y, Time.deltaTime * followSpeed)
        );
    }*/

}
