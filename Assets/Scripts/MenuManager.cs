using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private ScreenFader screenFader;
    [SerializeField]
    private SliderManager slider;

    // Start is called before the first frame update
    void Start()
    {
        screenFader = GetComponent<ScreenFader>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void playGame()
    {
        StartCoroutine(playGameCoroutine());
    }

    private IEnumerator playGameCoroutine()
    {
        screenFader.FadeOut();
        yield return new WaitForSeconds(1f);
        //Load scene
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        StartCoroutine(exitGameCoroutine());
    }

    private IEnumerator exitGameCoroutine()
    {
        screenFader.FadeOut();
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    public void UpdateVolume()
    {
        GameResources.gameVolume = slider.mainSlider.value;
    }
}
