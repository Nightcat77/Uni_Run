using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;//코인의 원본 프리팹

    public float timeBetSpawnMin = 1f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = 2.5f; // 배치할 위치의 최소 y값
    public float yMax = 3f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    GameObject coins; // 미리 생성한 발판들

    private Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점

    private void Start()
    {
        coins = new GameObject();
        
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }
    private void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            float yPos = Random.Range(yMin, yMax);

            coins = Instantiate(coinPrefab, poolPosition, Quaternion.identity);
            coins.transform.position = new Vector2(xPos, yPos);
            

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(coins);
        Instantiate(coinPrefab, new Vector2(xPos,yMax), Quaternion.identity);
    }
}
