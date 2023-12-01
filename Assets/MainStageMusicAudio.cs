using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStageMusicAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip idleMusic;
    public AudioClip battleMusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameController.onRoundStart += switchToBattleMusic;
        GameController.onRoundEnd += switchToIdleMusic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void switchToBattleMusic()
    {
        audioSource.clip = battleMusic;
        audioSource.Play();
    }

    public void switchToIdleMusic()
    {
        audioSource.clip = idleMusic;
        audioSource.Play();
    }
}
