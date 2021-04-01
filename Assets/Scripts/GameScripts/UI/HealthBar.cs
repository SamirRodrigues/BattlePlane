using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider fillBar;

    public void Update()
    {        
        fillBar.value = PlayerManager.Instance.health / 100;
        if(PlayerManager.Instance.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
