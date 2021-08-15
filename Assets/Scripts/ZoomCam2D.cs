using UnityEngine;
using Cinemachine;

public class ZoomCam2D : MonoBehaviour
{
    [SerializeField] public float zoomFactor = 1.0f;
    [SerializeField] public float zoomSpeed = 5.0f;
    CinemachineVirtualCamera vcam;
    private float originalSize = 0f;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        originalSize = vcam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float targetSize = originalSize * zoomFactor;

        if(targetSize != vcam.m_Lens.OrthographicSize)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(
                vcam.m_Lens.OrthographicSize,
                targetSize,
                Time.deltaTime * zoomSpeed
            );
        }
    }

    void SetZoom(float zoomFactor)
    {
        this.zoomFactor = zoomFactor;
    }
}
