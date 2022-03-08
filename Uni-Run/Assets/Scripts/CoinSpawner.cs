using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;//������ ���� ������

    public float timeBetSpawnMin = 1f; // ���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMax = 2f; // ���� ��ġ������ �ð� ���� �ִ�
    private float timeBetSpawn; // ���� ��ġ������ �ð� ����

    public float yMin = 2.5f; // ��ġ�� ��ġ�� �ּ� y��
    public float yMax = 3f; // ��ġ�� ��ġ�� �ִ� y��
    private float xPos = 20f; // ��ġ�� ��ġ�� x ��

    GameObject coins; // �̸� ������ ���ǵ�

    private Vector2 poolPosition = new Vector2(0, -20); // �ʹݿ� ������ ���ǵ��� ȭ�� �ۿ� ���ܵ� ��ġ
    private float lastSpawnTime; // ������ ��ġ ����

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
