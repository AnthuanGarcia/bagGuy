using UnityEngine;
using UnityEngine.Audio;

public class SpeedUpSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float pitch = 1.0f;
    AudioMixerGroup pitchBend;

    void Awake()
    {
        if(Mathf.Approximately(pitch, 1f))
            pitchBend = null;
        else
            pitchBend = Resources.Load<AudioMixerGroup>("Mixer Speed Up");
            
        audioSource.outputAudioMixerGroup = pitchBend;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            audioSource.pitch = pitch;
            pitchBend.audioMixer.SetFloat("pitchBend", 1f / pitch);
        }
    }
}
