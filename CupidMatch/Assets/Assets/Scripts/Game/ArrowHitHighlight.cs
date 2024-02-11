using UnityEngine;

public class ArrowHitHighlight : MonoBehaviour
{
    public Color highlightColor = new Color(1f, 0f, 0f, 1f); // Opaque red color for highlighting

    private Color originalColor; // Store the original color of the object
    private SpriteRenderer objectRenderer; // Reference to the object's SpriteRenderer
    private bool isHighlighted = false; // Flag to track if the object is currently highlighted
    private string interest; // Store the interest of the customer
    private static int hitCounter = 0; // Counter to track the order of hits

    private void Start()
    {
        // Ensure the object has a SpriteRenderer component
        objectRenderer = GetComponent<SpriteRenderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("ArrowHitHighlight script requires a SpriteRenderer component on the object.");
            enabled = false;
            return;
        }

        // Store the original color
        originalColor = objectRenderer.color;

        // Retrieve the interest from the Customer component
        Customer customerComponent = GetComponent<Customer>();
        if (customerComponent != null)
        {
            interest = customerComponent.interest;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collider belongs to a projectile (you may need to adjust the tag or other criteria)
        if (collider.gameObject.CompareTag("Projectile"))
        {
            // Increment the hit counter
            hitCounter++;

            // Highlight the object when hit
            Destroy(collider.gameObject, 0f);
            HighlightObject();

            // Check for linked interests after the second hit
            if (hitCounter == 2)
            {
                CheckForLink();
            }
        }
    }

    private void CheckForLink()
    {
        // Reset the hit counter for the next round
        hitCounter = 0;

        // Check if the interests of the first and second hits match
        if (GameManager.GetFirstPlayerInterest() == GameManager.GetSecondPlayerInterest())
        {
            DeselectCustomers();
            Debug.Log("Linked");
        }
        else
        {
            // If interests don't match, deselect (unhighlight) both customers
            DeselectCustomers();
        }
    }

    private void DeselectCustomers()
    {
        // Reset the highlight state and color for both customers
        isHighlighted = false;
        objectRenderer.color = originalColor;

        // Find the other customer object and reset its state and color
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in customers)
        {
            if (customer != gameObject)
            {
                ArrowHitHighlight otherCustomerHighlight = customer.GetComponent<ArrowHitHighlight>();
                if (otherCustomerHighlight != null)
                {
                    otherCustomerHighlight.isHighlighted = false;
                    otherCustomerHighlight.objectRenderer.color = otherCustomerHighlight.originalColor;
                }
            }
        }
    }

    private void HighlightObject()
    {
        // Toggle the highlight state
        isHighlighted = !isHighlighted;

        // Apply the appropriate color based on the highlight state
        if (isHighlighted)
        {
            objectRenderer.color = highlightColor;
            // Assign interest based on the order of hits
            if (hitCounter == 1)
            {
                GameManager.SetFirstPlayerInterest(interest);
            }
            else if (hitCounter == 2)
            {
                GameManager.SetSecondPlayerInterest(interest);
            }
        }
        else
        {
            objectRenderer.color = originalColor;
        }
    }
}
