using UnityEngine;

public class WaveExplosionPost : MonoBehaviour
{
    public Material mat;
	protected float _radius;

	void Awake()
	{
		mat = new Material(Shader.Find("MyShaders/LiquidWarpEffect"));
	}

	public float radius {
		get => _radius;
		set { 
			_radius=value;
			mat.SetFloat("_Radius",_radius);
		}
	}
	public void StartIt(Vector2 center) {
        //float width = Screen.width;
        //float height = Screen.height;

		mat.SetFloat("_CenterX",Mathf.Round(center.x - 25f));
		mat.SetFloat("_CenterY",Mathf.Round(center.y - 25f));
		radius=0f;

		GoTweenConfig config=new GoTweenConfig().floatProp("radius",1.6f);
		//config.easeType=GoEaseType.ExpoOut;
		//config.onComplete(HandleComplete);
		Go.to(this,1f,config);
	}
	protected void HandleComplete(AbstractGoTween tween) {
		Destroy(this);
	}
	static WaveExplosionPost _postProcessing;
	static public WaveExplosionPost Get() {
		WaveExplosionPost postProcessing=Camera.main.gameObject.AddComponent<WaveExplosionPost>(); 
		return postProcessing;
	}
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, mat);
	}
}
