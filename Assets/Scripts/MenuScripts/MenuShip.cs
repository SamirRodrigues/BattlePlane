using UnityEngine;

public class MenuShip : MonoBehaviour
{
    public float speed = 10f;
    public float acceleration_amount = 50f;
    public float shipRotationSpeed = 3f;
    public float moveRange = 10f;


    private Vector3 startPosition;
    private Vector3 newPosition;


    void Start()
    {
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
        if (collision.gameObject.layer == 6) //Obstacle
        {
            newPosition = GetRoamingPosition();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
