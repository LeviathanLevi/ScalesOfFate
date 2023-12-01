using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputAction moveHorizontal;
    [SerializeField]
    InputAction moveVertical;
    [SerializeField]
    InputAction zoom;

    [SerializeField]
    float moveSpeed = 35f;
    [SerializeField]
    float scrollSpeed = 5f;
    
    [SerializeField]
    float minX = -25f;
    [SerializeField]
    float maxX = 25f;
    [SerializeField]
    float minY = -15f;
    [SerializeField]
    float maxY = 15f;
    [SerializeField]
    float minZ = -10f;
    [SerializeField]
    float maxZ = 25f;

    Vector3 targetPos;

    //Hover for info:
    private Camera mainCamera;
    private Vector2 mousePosition;

    [SerializeField]
    InputAction delete;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    private UnitInformation unitInformation; // Variable

    private GameObject currentUnit;

    public bool allowUnitHoveringAndDeletion = true;

    //For end game animation:
    private Vector3 startingPos;
    public bool movementActive = true;


    private void Start()
    {
        startingPos = transform.position;
        moveHorizontal.Enable();
        moveVertical.Enable();
        zoom.Enable();
        targetPos = transform.position;

        mainCamera = Camera.main;
        delete.Enable();
    }

    public void ResetCameraPosition()
    {
        targetPos = startingPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementActive)
        {
            //Read inputs and update over time
            Vector3 movement = new(moveHorizontal.ReadValue<float>() * moveSpeed, zoom.ReadValue<float>() * scrollSpeed, moveVertical.ReadValue<float>() * moveSpeed);
            movement = movement * Time.deltaTime;
            movement = targetPos + movement;
            //Clamp
            movement.x = Mathf.Clamp(movement.x, minX, maxX);
            movement.y = Mathf.Clamp(movement.y, minY, maxY);
            movement.z = Mathf.Clamp(movement.z, minZ, maxZ);

            //Set new vector to the target
            targetPos = movement;
        }

        // Use Lerp to smoothly transition to the target position
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);

        transform.rotation = Quaternion.Euler(transform.rotation.x + (transform.position.y + 30), transform.rotation.y, transform.rotation.z);

        //Hover for information:
        mousePosition = Mouse.current.position.ReadValue();

        if (allowUnitHoveringAndDeletion)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (unitInformation.gameObject.activeSelf == false || currentUnit != hit.transform.gameObject)
                {
                    currentUnit = hit.transform.gameObject;

                    unitInformation.gameObject.SetActive(true);

                    UnitController hitUnitController = hit.transform.GetComponent<UnitController>();

                    if (hitUnitController.isHeavenly == true)
                    {
                        unitInformation.SetRedX(true);
                    }
                    else
                    {
                        unitInformation.SetRedX(false);
                    }

                    unitInformation.SetTitleAndDescription(hitUnitController.unitName, hitUnitController.health.ToString(), hitUnitController.damage.ToString(), hitUnitController.weight.ToString());
                }

                if (delete.WasPressedThisFrame() && hit.transform.gameObject.GetComponent<UnitController>().isHeavenly == true)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                unitInformation.gameObject.SetActive(false);
            }
        }
    }
}
