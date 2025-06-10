using System.Xml.Serialization;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float speed = 5f;
    public AudioSource AudioSource;
    public AudioClip Beep;

    private void Start()
    {
        PlayClip(Beep);
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }
    }
}



