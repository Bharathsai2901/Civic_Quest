using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
[SerializeField] private float healthValue; // A float value for the amount of health this collectible will add to the player's health
[SerializeField] private AudioClip pickupSound; // An audio clip that will play when the player collects this object

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.tag == "Player")
    {
        SoundManager.instance.PlaySound(pickupSound); // Play the pickup sound
        collision.GetComponent<Health>().AddHealth(healthValue); // Add the healthValue to the player's health
        gameObject.SetActive(false); // Deactivate the collectible
    }
}

}