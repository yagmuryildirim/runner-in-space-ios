using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trap : MonoBehaviour
{
    [SerializeField] protected float chanceToSpawn;

    protected virtual void Start()
    {
        bool canSpawn = chanceToSpawn >= Random.Range(0, 100);
        if (!canSpawn) Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.gameObject.SetActive(false);
            GameManager.instance.EndGame();
        }
    }
}
