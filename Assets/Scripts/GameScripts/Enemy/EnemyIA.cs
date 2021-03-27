using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private GameManager gameManager;

    public float health = 100f;
    [SerializeField]
    private GameObject destroyEffect;


    private Transform player;

    public float speed = 10f;
    public float findTargetRange = 7f;
    public float acceleration_amount = 50f;
    public float shipRotationSpeed = 3f;
    public float moveRange = 10f;


    private Vector3 startPosition;
    private Vector3 newPosition;

    private enum State
    {
        ROAMING,
        ATTACKING,
    }

    private State state;

    private void Awake()
    {
        state = State.ROAMING;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        newPosition = GetRoamingPosition();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            default:
            case State.ROAMING:
                Move();
                break;
            case State.ATTACKING:
                AttackMode();
                break;

        }
        
    }

   

    void Move()
    {
        if(player != null)
        {
            if (Vector2.Distance(player.position, transform.position) < findTargetRange)
            {
                state = State.ATTACKING;
                Debug.Log(state);
            }
            else
            {
                Vector3 curretPosition = transform.position;
                Vector3 direction = new Vector3(newPosition.x - curretPosition.x, newPosition.y - curretPosition.y, newPosition.z - curretPosition.z);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f, shipRotationSpeed * Time.deltaTime)));

                if (Vector3.Distance(transform.position, newPosition) < 1f)
                {
                    newPosition = GetRoamingPosition();
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration_amount * Time.deltaTime);
                }
            }
        }
        
    }

    void AttackMode()
    {
        if(player != null)
        {
            if (Vector2.Distance(player.position, transform.position) > findTargetRange)
            {
                state = State.ROAMING;
                Debug.Log(state);
            }

            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime);

            Vector3 targetPos = new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, player.position.z);
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
        
    }

    public void Damage(float value)
    {
        health -= value;
        if (health <= 0)
        {
            gameManager.EnemyDefeated();
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    //Utils Funcitons
    private Vector3 GetRoamingPosition()
    {
        return startPosition + GetRandomDir() * Random.Range(moveRange, moveRange);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6) //Obstacle
        {
            newPosition = GetRoamingPosition();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, findTargetRange);
    }
}
