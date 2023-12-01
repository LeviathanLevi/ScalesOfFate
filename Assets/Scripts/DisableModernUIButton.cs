using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableModernUIButton : MonoBehaviour
{
    [SerializeField] private ButtonManager myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<ButtonManager>();
        myButton.Interactable(false);
    }

}
