using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    public AudioSource playLoop;

    public void AudioPlayLoop(AudioClip clip)
    {
        playLoop.loop = true;
        playLoop.clip = clip;
        playLoop.Play();
    }

    private void Start()
    {
        AudioPlayLoop(playLoop.clip);
    }
}