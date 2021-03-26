using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider fillBar;
    [SerializeField]
    private Player player;

    private void Start()
    {
        if(!player)
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Update()
    {        
        fillBar.value = player.health / 100;
        if(player.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
