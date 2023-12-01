using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Rename to unit interaction controller or somethin
public class MouseTooltip : MonoBehaviour
{
    [SerializeField]
    private Transform mouseAnchor;

    // Update is called once per frame
    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();

        Vector3 Offset = mouseAnchor.position - transform.position;

        transform.position -= Offset;
    }
}
