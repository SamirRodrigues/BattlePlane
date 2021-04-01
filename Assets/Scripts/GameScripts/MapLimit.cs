using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.ChangeScene("GameOver");
        }
    }
}
