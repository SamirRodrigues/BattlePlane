using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public float speed;
    public float findTargetRange;

    public float shootRange;
    private Transform player;

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

        if(Vector3.Distance(transform.position, newPosition) < 1f)
        {
            newPosition = GetRoamingPosition();
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    //Utils Funcitons
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    void Move()
    {
        if(Vector2.Distance(player.position, transform.position) < findTargetRange && Vector2.Distance(player.position, transform.position) > shootRange )
        { 
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);        
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findTargetRange);
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
