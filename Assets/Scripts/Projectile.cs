using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject heavenlyExplosionPrefab;
    public GameObject hellishExplosionPrefab;

    public Transform target;
    public float speed = 10f;
    public float lifespan = 5000f;

    [SerializeField]
    private float lifeTimer = 0f;

    public int damage;
    public bool isHeavenly;


    public bool isActive = false;
    private void Start()
    {
        // Set the initial direction towards the target
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPosition);
    }

    private void Update()
    {
        if (isActive)
        {
            // Move towards the target
            float step = speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            // Update the lifespan timer
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifespan)
            {
                // If the projectile has exceeded its lifespan, destroy it
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHeavenly)
        {
            if (other.gameObject.layer == 8)
            {
                other.gameObject.GetComponent<UnitControllerReference>().unitController.MinionHit(damage);
                Instantiate(heavenlyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<UnitControllerReference>().unitController.MinionHit(damage);
                Instantiate(hellishExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
