using UnityEngine;
using Cinemachine;

public class BackScreenSize : MonoBehaviour
{
    public float followSpeed = 3f;
    public float resizeSpeed = 2f;

    CinemachineVirtualCamera vCam;

    Vector2 originalSize;
    bool reduce = false;

    // Start is called before the first frame update
    void Start()
    {
        float width = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        originalSize = new Vector2(width - 1.5f, 5f);
        transform.localScale = originalSize;

        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    /*void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            vCam.transform.position,
            Time.deltaTime * followSpeed
        );

        transform.position = new Vector2(
            vCam.transform.position.x,
            vCam.transform.position.y
        );
    }*/

    void FixedUpdate()
    {
        if(reduce)
        {
            transform.localScale = new Vector2(
                Mathf.Lerp(transform.localScale.x, 6f, Time.fixedDeltaTime * resizeSpeed),
                Mathf.Lerp(transform.localScale.y, 4f, Time.fixedDeltaTime * resizeSpeed)
            );

            transform.position = new Vector2(
                Mathf.Lerp(transform.position.x, vCam.transform.position.x, Time.fixedDeltaTime * followSpeed),
                Mathf.Lerp(transform.position.y, vCam.transform.position.y, Time.fixedDeltaTime * followSpeed)
            );
        }
        else
        {
            transform.localScale = new Vector2(
                Mathf.Lerp(transform.localScale.x, originalSize.x, Time.fixedDeltaTime * resizeSpeed),
                Mathf.Lerp(transform.localScale.y, originalSize.y, Time.fixedDeltaTime * resizeSpeed)
            );

            /*transform.position = Vector2.Lerp(
                transform.position,
                vCam.transform.position,
                Time.fixedDeltaTime * followSpeed
            );*/

            transform.position = new Vector2(
                vCam.transform.position.x,
                vCam.transform.position.y
            );
        }
    }

    public void ReduceSize()
    {
        reduce = !reduce;
    }

}
