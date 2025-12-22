using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    public AudioSource playLoop;
    public AudioSource playOnce;

    public void AudioPlayLoop(AudioClip clip)
    {
        if (playLoop == null) return;
        if (playLoop.isPlaying)
        {
            playLoop.Stop();
        }
        playLoop.loop = true;
        playLoop.clip = clip;
        playLoop.Play();
    }

    public void AudioPlayOnce()
    {
        if (playOnce == null) return;
        playOnce.PlayOneShot(playOnce.clip);
    }

    private void Start()
    {
        if (playLoop != null) { 
            AudioPlayLoop(playLoop.clip); 
        }
    }
}