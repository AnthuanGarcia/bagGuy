using UnityEngine;
using SpriteGlow;

public class GlowEffect : MonoBehaviour
{
    SpriteGlowEffect glowEffect;
    int pointsTouched = 0;
    const int maxPoints = 40;

    void Start()
    {
        glowEffect = GetComponent<SpriteGlowEffect>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (pointsTouched == maxPoints) collision.gameObject.SetActive(false);

        if(collision.gameObject.CompareTag("GlowPoint"))
        {
            float glow = glowEffect.GlowBrightness;
            glow += 0.05f;
            glowEffect.GlowBrightness = glow;

            Vector2 axis = collision.transform.position;
            collision.transform.position = new Vector2(axis.x + 1.0f, axis.y);
            pointsTouched++;
        }

        if(collision.gameObject.CompareTag("DoorGlow"))
        {
            SpriteGlowEffect door = collision.gameObject.GetComponent<SpriteGlowEffect>();
            door.GlowBrightness = 2.5f;
        }
    }
}
