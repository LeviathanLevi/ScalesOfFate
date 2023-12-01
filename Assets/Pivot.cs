using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    private Vector3 targetRot;

    public GameObject hellBuffUp;
    public GameObject hellBuffDown;

    public GameObject heavenBuffUp;
    public GameObject heavenBuffDown;

    private float rotationSpeed = 6f; // degrees per second
    private float currentRotation = 0f; // track rotation angle

    private bool rotateRight = false;
    private bool rotateLeft = false;

    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetRot = transform.rotation.eulerAngles;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateRight)
        {
            // calculate the amount to rotate this frame
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            // if rotation would exceed 25 degrees, restrict it 
            if (currentRotation + rotationThisFrame > 25f)
            {
                rotationThisFrame = 25f - currentRotation;
            }

            // rotate and update current rotation
            transform.Rotate(Vector3.up, rotationThisFrame);
            currentRotation += rotationThisFrame;

            // Stop rotating after reaching 25 degrees
            if (currentRotation >= 25f)
            {
                rotationSpeed = 0;
            }
        }
        else if (rotateLeft)
        {
            // calculate the amount to rotate this frame
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            // if rotation would exceed 25 degrees, restrict it 
            if (currentRotation + rotationThisFrame > 25f)
            {
                rotationThisFrame = 25f - currentRotation;
            }

            // rotate and update current rotation
            transform.Rotate(Vector3.up, -rotationThisFrame);
            currentRotation += rotationThisFrame;

            // Stop rotating after reaching 25 degrees
            if (currentRotation >= 25f)
            {
                rotationSpeed = 0;
            }
        }
    }

    public void HellLostRotatePivot()
    {
        hellBuffDown.SetActive(true);
        heavenBuffUp.SetActive(true);
        rotationSpeed = 6f;
        rotateRight = false;
        rotateLeft = true;
    }

    public void HeavenLostRotatePivot()
    {
        heavenBuffDown.SetActive(true);
        hellBuffUp.SetActive(true);
        rotationSpeed = 6f;
        rotateLeft = false;
        rotateRight = true;
    }

    public void ResetPivot()
    {
        hellBuffUp.SetActive(false);
        hellBuffDown.SetActive(false);

        heavenBuffUp.SetActive(false);
        heavenBuffDown.SetActive(false);

        rotateRight = false;
        rotateLeft = false;

        currentRotation = 0;
        transform.rotation = originalRotation;
    }
}
