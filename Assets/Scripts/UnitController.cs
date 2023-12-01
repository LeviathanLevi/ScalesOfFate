using JetBrains.Annotations;
using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class UnitController : MonoBehaviour
{
    //Stats:
    public bool isHeavenly = true;
    public bool isStationary = false;
    public string unitName = "Angel";
    public int health = 50;
    public int damage = 15;
    public int weight = 75;
    public float speed = 3.25f;

    public GameObject projectilePrefab;

    private Transform heavenlyUnits;
    private Transform hellishUnits;

    private Transform target;

    private State state;

    private bool isMoving = false;

    private bool pause = false;
    private float pauseTime = 1f;

    //control
    public bool active = false;

    Vector3 unitTargetPosition;

    private int originalHealth;

    private GameObject projectiles;

    private bool move = false;

    //Name and healthbar:
    public ProgressBar healthBar;
    public TextMeshProUGUI unitPersonalName;

    //Death effect:
    public GameObject heavenlyUnitDeathEffectPrefab;
    public GameObject hellishUnitDeathEffectPrefab;

    //Audio
    public AudioClip unitPlaced;
    public AudioClip unitHit;
    public AudioClip unitDied;
    public AudioClip unitAttack;

    AudioSource unitAudioSource;

    //Animation
    Animator animator;

    //HitBox:
    CapsuleCollider capsuleCollider;
    public GameObject towerColliders;


    string[] names = new string[]
    {
        "ANGELS", "SERAPHIM", "CHERUBIM", "ARCHANGELS", "SAINTS",
        "GOD", "PARADISE", "EDEN", "ZION", "NIRVANA",
        "VALHALLA", "ELYSIUM", "JANNAH", "SHANGRI-LA", "HEAVEN",
        "DEMONS", "DEVIL", "LUCIFER", "SATAN", "HELL",
        "HADIS", "TARTARUS", "ABADDON", "GEHENNA", "NIFLHEIM",
        "PANDAEMONIUM", "PERDITION", "APOCALYPSE", "UNDERWORLD", "INFERNO"
    };

    enum State
    {
        STATE_STANDING,
        STATE_MOVING,
        STATE_ATTACKING
    };


    // Start is called before the first frame update
    void Start()
    {
        if (!isStationary)
        {
            capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        }

        projectiles = GameObject.Find("Projectiles");

        Assert.IsNotNull(projectiles);

        originalHealth = health;
        state = State.STATE_STANDING;

        heavenlyUnits = GameObject.Find("Heavenly Units").transform;
        hellishUnits = GameObject.Find("Hellish Units").transform;

        //Choose a random personal name:
        int choice = Random.Range(0, names.Count());
        unitPersonalName.text = names[choice];
        healthBar.ChangeValue(((float)health / originalHealth) * 100);

        animator = GetComponentInChildren<Animator>();

        unitAudioSource = GetComponentInChildren<AudioSource>();

        unitAudioSource.PlayOneShot(unitPlaced);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GetTarget();
        }

        if (active)
        {
            switch (state)
            {
                case State.STATE_STANDING:
                    if (pause == true)
                    {
                        pauseTime -= Time.deltaTime;
                        if (pauseTime < 0)
                        {
                            pause = false;
                            pauseTime = 1f;
                        }
                        else
                            return;
                    }

                    if (isStationary == false)
                    {
                        if (move == true)
                        {
                            //moving
                            animator.SetTrigger("Walk");
                            unitTargetPosition = GetRandomPointInBounds();
                            transform.LookAt(unitTargetPosition);
                            state = State.STATE_MOVING;
                            isMoving = true;
                            move = false;
                        }
                        else if (move == false)
                        {
                            move = true;
                            state = State.STATE_ATTACKING;
                        }
                    }
                    else
                    {
                        state = State.STATE_ATTACKING;
                    }

                    pause = true;
                    break;

                case State.STATE_MOVING:
                    if (isMoving)
                    {

                        // Calculate the distance from the current position to the target position
                        float distance = Vector3.Distance(transform.position, unitTargetPosition);

                        // If the distance is greater than a small value (to prevent never reaching the target due to floating point precision)
                        if (distance > 0.01f)
                        {

                            // Calculate distance to move during this frame
                            float step = speed * Time.deltaTime; // calculate distance to move

                            // Move our position a step closer to the target.
                            transform.position = Vector3.MoveTowards(transform.position, unitTargetPosition, step);
                            //transform.position = Vector3.Lerp(transform.position, unitTargetPosition, speed * Time.deltaTime);
                            return;
                        }

                        isMoving = false;
                        state = State.STATE_STANDING;
                        animator.SetTrigger("Idle");
                        pause = true;
                    } 

                    break;

                case State.STATE_ATTACKING:

                    if (pause == true)
                    {
                        pauseTime -= Time.deltaTime;
                        if (pauseTime < 0)
                        {
                            pause = false;
                            pauseTime = 1f;
                        }
                        else
                            return;
                    }

                    //attacking
                    transform.LookAt(target);
                    animator.SetTrigger("Attack");

                    GameObject instance = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                    instance.transform.SetParent(projectiles.transform, true);
                    Projectile projectile = instance.GetComponent<Projectile>();
                    projectile.isHeavenly = isHeavenly;
                    projectile.target = target;
                    projectile.damage = damage;
                    projectile.isActive = true;
                    unitAudioSource.PlayOneShot(unitAttack);

                    target = null;

                    state = State.STATE_STANDING;
                    pause = true;
                    break;
            }
        }
    }

    public Vector3 GetRandomPointInBounds()
    {
        if (isHeavenly == true)
        {
            float x = Random.Range(-20f, -8.6f);
            float z = Random.Range(13f, 27f);

            return new Vector3(x, gameObject.transform.position.y, z);
        }
        else
        {
            float x = Random.Range(8.6f, 20f);
            float z = Random.Range(13f, 27f);

            return new Vector3(x, gameObject.transform.position.y, z);
        }
    }

    public void GetTarget()
    {
        if (isHeavenly == true)
        {
            if (hellishUnits.childCount > 0)
            {
                // Generate a random index
                int index = Random.Range(0, hellishUnits.childCount);
                // Get the child transform at the random index
                Transform randomChild = hellishUnits.GetChild(index);
                if (randomChild.gameObject.GetComponent<UnitController>().active == true)
                {
                    target = randomChild;
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                active = false;
            }
        }
        else
        {
            if (heavenlyUnits.childCount > 0)
            {
                // Generate a random index
                int index = Random.Range(0, heavenlyUnits.childCount);
                // Get the child transform at the random index
                Transform randomChild = heavenlyUnits.GetChild(index);
                if (randomChild.gameObject.GetComponent<UnitController>().active == true)
                {
                    target = randomChild;
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                active = false;
            }
        }
    }

    public void MinionHit(int damage)
    {
        health -= damage;

        healthBar.ChangeValue(((float)health / originalHealth) * 100);

        unitAudioSource.PlayOneShot(unitHit);

        if (health <= 0)
        {
            if (isHeavenly) 
            {
                Instantiate(heavenlyUnitDeathEffectPrefab, transform.position, heavenlyUnitDeathEffectPrefab.transform.rotation);
            }
            else
            {
                Instantiate(hellishUnitDeathEffectPrefab, transform.position, hellishUnitDeathEffectPrefab.transform.rotation);
            }
            //Dead
            active = false;

            if (!isStationary)
            {
                capsuleCollider.enabled = false;
            }
            else
            {
                towerColliders.SetActive(false);
            }

            StartCoroutine(Death());


        }
    }

    public void ResetUnit()
    {
        animator.SetTrigger("Idle");
        health = originalHealth;
        healthBar.ChangeValue(((float)health / originalHealth) * 100);
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");

        unitAudioSource.PlayOneShot(unitDied);

        yield return new WaitForSeconds(3.633f);

        Destroy(gameObject);

        yield return null;
    }
}
