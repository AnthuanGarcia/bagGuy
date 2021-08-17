using UnityEngine;
using Cinemachine;

public class BackScreenSize : MonoBehaviour
{
    public float followSpeed = 3f;
    CinemachineVirtualCamera vCam;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //float width = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        //transform.localScale = new Vector2(width + 1.5f, 4.5f);

        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(
            Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * followSpeed),
            Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime * followSpeed)
        );
    }

}
