using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    public Sprite mainSprite;
    public Sprite[] halfsSprites;

    public Color fruitColor;

    public GameObject halfPrefab;

    public ParticleSystem particles;

    private void Start()
    {
        GetComponent<Image>().sprite = mainSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blade"))
        {
            Cut();
        }
    }

    void Cut()
    {
        Debug.Log("Fruit Cut!");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 originalVelocity = rb.velocity;

        

        // D�truire le fruit original

        // Instancier les deux moiti�s
        GameObject leftHalf = Instantiate(halfPrefab, new Vector3(gameObject.transform.position.x - 20, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        GameObject rightHalf = Instantiate(halfPrefab, new Vector3(gameObject.transform.position.x + 20, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);

        ParticleSystem.MainModule settings = particles.main;
        settings.startColor = fruitColor;

        Instantiate(particles, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);

        Destroy(gameObject);

        leftHalf.GetComponent<Image>().sprite = halfsSprites[0];
        rightHalf.GetComponent<Image>().sprite = halfsSprites[1];

        // Ajouter des Rigidbody2D aux moiti�s si ce n'est pas d�j� fait
        Rigidbody2D leftRb = leftHalf.GetComponent<Rigidbody2D>();
        Rigidbody2D rightRb = rightHalf.GetComponent<Rigidbody2D>();

        // Appliquer des vitesses aux moiti�s
        if (leftRb != null && rightRb != null)
        {
            // R�duire la magnitude de la v�locit�
            Vector2 reducedVelocity = originalVelocity * 0.7f;

            // Ajouter une petite variation pour simuler l'effet de d�coupe
            Vector2 perpendicularDirection = Vector2.Perpendicular(originalVelocity.normalized) * 0.5f;

            leftRb.AddForce(new Vector2(-400000, 0));
            rightRb.AddForce(new Vector2(400000, 0));
        }
    }
}
