using UnityEngine;

public class DuplicateEffect : MonoBehaviour
{
    private Material material;

    void Awake()
    {
        material = new Material(Shader.Find("MyShaders/DuplicatedAbstract"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
