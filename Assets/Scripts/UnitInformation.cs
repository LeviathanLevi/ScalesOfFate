using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformation : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    TextMeshProUGUI description;

    [SerializeField]
    Image redX;

    public void SetTitleAndDescription(string name, string health, string damage, string weight)
    {
        title.text = name;
        description.text = "Health: " + health + "\nAttack: " + damage + "\nWeight: " + weight;
    }

    public void SetRedX(bool on)
    {
        if (on)
        {
            redX.gameObject.SetActive(true);
        }
        else
        {
            redX.gameObject.SetActive(false);
        }
    }
}
