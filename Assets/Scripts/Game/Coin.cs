using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlaySFX(3);
            GameManager.instance.IncrementCoins();
            Destroy(gameObject);
        }
    }
}
