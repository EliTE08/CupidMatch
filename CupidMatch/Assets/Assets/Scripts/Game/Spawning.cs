using UnityEngine;
using System.Collections.Generic;

public class SpawnSquares : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array of customer sprites
    public int numberOfCustomers = 10;
    public float minX = 0.5f, maxX = 9.5f, minY = 0.5f, maxY = 9.5f;
    public float minDistance = 2.0f; // Minimum distance between customers
    public List<string> interests = new List<string>() { "Basketball", "Painting", "Science", "Skateboard", "Tennis" };

    void Start()
    {
        // Make sure numberOfCustomers is even, so each customer can have a match
        if (numberOfCustomers % 2 != 0)
        {
            Debug.LogError("Number of customers must be even for each customer to have a match!");
            return;
        }

        // Create a list of interests that will be assigned to customers
        List<string> interestsToAssign = new List<string>();
        foreach (string interest in interests)
        {
            // Add each interest twice, so each interest is assigned to two customers
            interestsToAssign.Add(interest);
            interestsToAssign.Add(interest);
        }

        for (int i = 0; i < numberOfCustomers; i++)
        {
            Vector2 spawnPosition = GenerateRandomPosition();
            GameObject selectedCustomerPrefab = GetRandomCustomerPrefab();
            GameObject newCustomer = Instantiate(selectedCustomerPrefab, spawnPosition, Quaternion.identity);

            // Choose a random interest from the list of interests to assign
            int randomIndex = Random.Range(0, interestsToAssign.Count);
            string randomInterest = interestsToAssign[randomIndex];

            // Remove the chosen interest from the list, so it won't be chosen again
            interestsToAssign.RemoveAt(randomIndex);

            newCustomer.GetComponent<Customer>().interest = randomInterest;
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
