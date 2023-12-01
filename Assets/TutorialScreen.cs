using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    public PlayerController playerController;

    public VoiceOvers voiceOvers;

    int screen = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController.movementActive = false;

        StartCoroutine(playIntro());
    }

    public void NextScreen()
    {
        if (screen == 0)
        {
            image1.SetActive(false);
            image2.SetActive(true);
            voiceOvers.PlayClip(1);
        }
        else if (screen == 1)
        {
            playerController.movementActive = true;
            gameObject.SetActive(false);
        }
        screen++;
    }

    IEnumerator playIntro()
    {
        yield return new WaitForSeconds(1);
        voiceOvers.PlayClip(0);
    }
}
