using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOvers : MonoBehaviour
{
    public AudioClip[] audioClips;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int index)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[index]);
    }
}
