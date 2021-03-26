using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Transform player;

    public float speed;
    public float findTargetRange;
    public float acceleration_amount;
    public float shipRotationSpeed = 3f;


    private Vector3 startPosition;
    private Vector3 newPosition;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        newPosition = GetRoamingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

   

    void Move()
    {
        if(Vector2.Distance(player.position, transform.position) < findTargetRange )
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime);

            Vector3 targetPos = new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, player.position.z);
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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

            GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration_amount * Time.deltaTime);

            
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + GetRandomDir() * Random.Range(10f, 10f);
    }

    //Utils Funcitons
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, findTargetRange);
    }
}
