using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public float health = 100f;
    [SerializeField]
    private GameObject destroyEffect;


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
        GameManager.Instance.EnemyRegister(this.gameObject);
        startPosition = transform.position;
        newPosition = GetRoamingPosition();
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
        if(PlayerManager.Instance != null)
        {
            if (Vector2.Distance(PlayerManager.Instance.transform.position, transform.position) < findTargetRange)
            {
                state = State.ATTACKING;
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
        if(PlayerManager.Instance != null)
        {
            if (Vector2.Distance(PlayerManager.Instance.transform.position, transform.position) > findTargetRange)
            {
                state = State.ROAMING;
                Debug.Log(state);
            }

            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime);

            Vector3 targetPos = new Vector3(PlayerManager.Instance.transform.position.x - transform.position.x, PlayerManager.Instance.transform.position.y - transform.position.y, PlayerManager.Instance.transform.position.z);
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
        
    }

    public void Damage(float value)
    {
        health -= value;
        if (health <= 0)
        {
            GameManager.Instance.EnemyDefeated(this.gameObject);
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
