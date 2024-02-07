using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Customer firstCustomerHit;
    private int score;

    public void HitByArrow(Customer customerHit)
    {
        // If this is the first customer hit by an arrow
        if (firstCustomerHit == null)
        {
            firstCustomerHit = customerHit;
            // Highlight the customer with a white border
            HighlightCustomer(customerHit, Color.white);
        }
        else // This is the second customer hit
        {
            // If the two customers have the same interest
            if (firstCustomerHit.interest == customerHit.interest)
            {
                // Increase the score
                score += 10;
                // Make the customers disappear
                Destroy(firstCustomerHit.gameObject);
                Destroy(customerHit.gameObject);
            }
            else // The customers have different interests
            {
                // Highlight both customers with a red border
                HighlightCustomer(firstCustomerHit, Color.red);
                HighlightCustomer(customerHit, Color.red);
            }

            // Reset the first customer hit
            firstCustomerHit = null;
        }
    }

    private void HighlightCustomer(Customer customer, Color color)
    {
        // Assuming the customer has a SpriteRenderer component
        SpriteRenderer spriteRenderer = customer.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.material.color = color;
        }
    }
}
