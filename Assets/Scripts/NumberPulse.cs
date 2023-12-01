using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberPulse : MonoBehaviour
{
    public void Start()
    {
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }
}
