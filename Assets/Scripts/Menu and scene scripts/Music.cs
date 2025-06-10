using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    public AudioClip musicClip;         
    public AudioSource audioSource;

    void Start()
    {
        
        
        audioSource.clip = musicClip;
        audioSource.loop = true;        
        audioSource.playOnAwake = false;

        PlayMusic();
    }

    void PlayMusic()
    {
        if (musicClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not assigned!");
        }
    }
}

