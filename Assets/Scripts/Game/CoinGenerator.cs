using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float chanceToSpawn;
    [SerializeField] private SpriteRenderer[] coinImages;
    private int amountOfCoins;

    private void Start()
    {
        foreach (var image in coinImages)
        {
            image.sprite = null;
        }

        amountOfCoins = Random.Range(minCoins, maxCoins);
        for (int i = 0; i < amountOfCoins; i++)
        {
            Vector3 offset = new Vector2(i - (amountOfCoins / 2), 0);
            Instantiate(coinPrefab, transform.position + offset, Quaternion.identity, transform);
        }
    }
}
