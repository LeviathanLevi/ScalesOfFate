using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f; // duration of the fade-out effect
    [SerializeField] private Image fadeImage = null; // reference to the Image component

    private float currentAlpha = 0f; // current alpha value of the Image

    private void Start()
    {
        fadeImage.color = new Color(0f, 0f, 0f, currentAlpha); // set the initial alpha value
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        fadeImage.gameObject.SetActive(true);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            currentAlpha = Mathf.Lerp(0f, 1f, timer / fadeDuration); // calculate the new alpha value
            fadeImage.color = new Color(0f, 0f, 0f, currentAlpha); // set the new alpha value
            timer += Time.deltaTime;
            yield return null;
        }

        currentAlpha = 1f; // set the final alpha value
        fadeImage.color = new Color(0f, 0f, 0f, currentAlpha); // set the final alpha value
    }
}
