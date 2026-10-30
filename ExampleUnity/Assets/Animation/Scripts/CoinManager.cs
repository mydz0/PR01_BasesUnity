using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform coinParent;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float minX = 10f;
    [SerializeField] float maxX = 30f;
    [SerializeField] float spawnY = 25f;

    List<GameObject> spawnedCoins = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        if (coinParent == null)
        {
            coinParent = GameObject.Find("Landscape").transform;
        }
        StartCoroutine(CreateCoins());
    }

    IEnumerator CreateCoins()
    {
        while (true)
        {
            float posX = Random.Range(minX, maxX);
            Vector2 coinPosition = new Vector2(posX, spawnY);
            GameObject newCoin = Instantiate(coinPrefab, coinPosition, Quaternion.identity, coinParent);
            spawnedCoins.Add(newCoin);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public int DestroyCoinsInRange(Vector2 center, float radius)
    {
        int destroyed = 0;
        for (int i = spawnedCoins.Count - 1; i >= 0; i--)
        {
            if (spawnedCoins[i] == null)
            {
                spawnedCoins.RemoveAt(i);
                continue;
            }
            if (Vector2.Distance(spawnedCoins[i].transform.position, center) <= radius)
            {
                Destroy(spawnedCoins[i]);
                spawnedCoins.RemoveAt(i);
                destroyed++;
            }
        }
        return destroyed;
    }
}
