using UnityEngine;

public class SpawnSquares : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array of customer sprites
    public int numberOfCustomers = 10;
    public float minX = 0.5f, maxX = 9.5f, minY = 0.5f, maxY = 9.5f;
    public float minDistance = 2.0f; // Minimum distance between customers

    void Start()
    {
        for (int i = 0; i < numberOfCustomers; i++)
        {
            Vector2 spawnPosition = GenerateRandomPosition();
            GameObject selectedCustomerPrefab = GetRandomCustomerPrefab();
            Instantiate(selectedCustomerPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GenerateRandomPosition()
    {
        float randomX, randomY;
        Vector2 spawnPosition;

        do
        {
            randomX = Random.Range(minX, maxX);
            randomY = Random.Range(minY, maxY);
            spawnPosition = new Vector2(randomX, randomY);
        } while (IsTooCloseToOthers(spawnPosition));

        return spawnPosition;
    }

    GameObject GetRandomCustomerPrefab()
    {
        return customerPrefabs[Random.Range(0, customerPrefabs.Length)];
    }

    bool IsTooCloseToOthers(Vector2 position)
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        foreach (GameObject customer in customers)
        {
            float distance = Vector2.Distance(position, customer.transform.position);
            if (distance < minDistance)
            {
                return true; // Too close to another customer
            }
        }

        return false; // Safe to spawn
    }
}
