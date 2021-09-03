using UnityEngine;
using Cinemachine;

public class BackScreenSize : MonoBehaviour
{
    public Transform follow;
    public float followSpeed = 3f;
    public float resizeSpeed = 2f;
    public bool resize = true;
    public bool initOnZero = false;

    Vector2 originalSize;
    bool reduce = false;
    float widthRed = 6f;
    float heightRed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        if(resize)
        {
            float width = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
            originalSize = new Vector2(width - 1.5f, 5f);
            transform.localScale = originalSize;
        }
        else
            originalSize = transform.localScale;

        if(initOnZero)
            transform.localScale = Vector2.zero;

        //vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
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
                Mathf.Lerp(transform.localScale.x, widthRed, Time.fixedDeltaTime * resizeSpeed),
                Mathf.Lerp(transform.localScale.y, heightRed, Time.fixedDeltaTime * resizeSpeed)
            );

            transform.position = new Vector2(
                Mathf.Lerp(transform.position.x, follow.transform.position.x, Time.fixedDeltaTime * followSpeed),
                Mathf.Lerp(transform.position.y, follow.transform.position.y, Time.fixedDeltaTime * followSpeed)
            );
        }
        else
        {
            if(!initOnZero)
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
                follow.transform.position.x,
                follow.transform.position.y
            );
        }
    }

    public void ReduceSize(float wid, float hei, bool redu)
    {
        widthRed = wid;
        heightRed = hei;
        reduce = redu;
        initOnZero = false;
    }

}
