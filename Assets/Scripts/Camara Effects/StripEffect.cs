using UnityEngine;

public class StripEffect : MonoBehaviour
{
    private Material material;

    void Awake()
    {
        material = new Material(Shader.Find("MyShaders/StirpsEffect"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
