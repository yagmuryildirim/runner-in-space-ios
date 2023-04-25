using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] platforms;
    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;
    [SerializeField] private Transform player;

    public static LevelGenerator instance;

    private Color platformColor = new Color(98f / 255f, 23f / 255f, 163f / 255f, 1f);


    public Color PlatformColor { get => platformColor; set => platformColor = value; }

    private void Awake()
    {
        platformColor = new Color(PlayerPrefs.GetFloat("pR", platformColor.r), PlayerPrefs.GetFloat("pG", platformColor.g), PlayerPrefs.GetFloat("pB", platformColor.b), 1f);
        instance = this;
    }

    private void Update()
    {
        DestroyPlatform();
        GeneratePlatform();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("pR", platformColor.r);
        PlayerPrefs.SetFloat("pG", platformColor.g);
        PlayerPrefs.SetFloat("pB", platformColor.b);
    }

    private void GeneratePlatform()
    {
        if (player == null) return;
        while (Vector2.Distance(respawnPosition, player.position) < distanceToSpawn)
        {
            Transform platform = platforms[Random.Range(0, platforms.Length)];

            foreach (PlatformController controller in platform.transform.GetComponentsInChildren<PlatformController>())
            {
                controller.GetComponent<SpriteRenderer>().color = PlatformColor;
            }

            Vector2 newPosition = new Vector2(respawnPosition.x - platform.Find("Start Point").position.x, 0);
            Transform newPlatform = Instantiate(platform, newPosition, Quaternion.identity, transform);

            respawnPosition = newPlatform.Find("End Point").position;
            distanceToSpawn = 100;
        }
    }

    private void DestroyPlatform()
    {
        if (player == null) return;
        if (transform.childCount > 0)
        {
            Transform toDelete = transform.GetChild(0);
            if (Vector2.Distance(player.position, toDelete.transform.position) > distanceToDelete)
            {
                Destroy(toDelete.gameObject);
            }
        }
    }
}
