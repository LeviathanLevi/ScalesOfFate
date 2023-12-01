using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragAndDropMonster : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] 
    GameObject objectPrefabToSpawn;
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    GameObject spawnLocationParticleEffectPrefab;
    GameObject spawnLocationParticleEffect;

    [SerializeField]
    GameObject unitSpawnParticleEffectPrefab;

    private Mouse mouse;

    [SerializeField]
    Transform heavenlyUnits;


    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        spawnLocationParticleEffect = Instantiate(spawnLocationParticleEffectPrefab, new Vector3(-300,-300,-300), objectPrefabToSpawn.transform.rotation);

        // Your code here, e.g. change the appearance of the button to indicate dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        //this.transform.position = eventData.position; // Follows the cursor position
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            spawnLocationParticleEffect.transform.position = hit.point;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (spawnLocationParticleEffect != null)
        {
            Destroy(spawnLocationParticleEffect);
        }
        // cast from cursor to world
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // the object identified by hit.transform was clicked
            // "hit.point" was the position on the clicked object
            //Debug.Log("Object hit: " + hit.transform.name);
            //Debug.Log("Hit point: " + hit.point);
            Destroy(spawnLocationParticleEffect);
            Instantiate(unitSpawnParticleEffectPrefab, hit.point, objectPrefabToSpawn.transform.rotation);

            // Instantiate an object at the current position
            Instantiate(objectPrefabToSpawn, hit.point, objectPrefabToSpawn.transform.rotation, heavenlyUnits);
        }
    }
}