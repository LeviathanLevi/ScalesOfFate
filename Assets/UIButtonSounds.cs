using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip buttonClicked;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        audioSource.PlayOneShot(buttonClicked);
    }
}
