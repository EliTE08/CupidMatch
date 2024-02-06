using UnityEngine;

public class SpawnSquares : MonoBehaviour
{
    public GameObject squarePrefab;
    public int numberOfSquares = 10;
    public float minX = 0.5f, maxX = 9.5f, minY = 0.5f, maxY = 9.5f;

    void Start()
    {
        for (int i = 0; i < numberOfSquares; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 spawnPosition = new Vector2(randomX, randomY);
            Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
