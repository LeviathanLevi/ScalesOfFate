using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlinking : MonoBehaviour
{
    public Image imageToFade; // Assign this in the inspector
    public float pulseFrequency = 1f; // Control the frequency of the pulse

    private float initialAlpha;
    private float targetAlpha;

    void Start()
    {
        imageToFade = GetComponent<Image>();
        initialAlpha = 0f; // Minimum opacity
        targetAlpha = 1f; // Maximum opacity
    }

    void Update()
    {
        float currentAlpha = Mathf.Lerp(initialAlpha, targetAlpha, (Mathf.Sin(pulseFrequency * Time.time) + 1f) / 2f);
        imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, currentAlpha);
    }
}
