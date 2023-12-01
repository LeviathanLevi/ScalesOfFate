using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public UnitInformation unitInformation;

    public void setUnitInformation(string name, string health, string damage, string weight)
    {
        unitInformation.SetTitleAndDescription(name, health, damage, weight);
    }
}
