using UnityEngine;

public class EnemyDamage: MonoBehaviour {
  // The EnemyDamage class is a MonoBehaviour script that controls the damage dealt by an enemy in a game using UnityEngine.

  [SerializeField] protected float damage;
  // damage is a float value that determines the amount of damage the enemy can deal to the player.

  protected void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == "Player")
      collision.GetComponent < Health > ()?.TakeDamage(damage);
  }
  // The OnTriggerEnter2D function is called when the enemy collides with a trigger collider. 
  //It checks if the collider's tag is "Player". 
  //If it is, it uses the null-conditional operator (?.) to call the TakeDamage function of the collider's Health component and deals damage to the player based on the damage value.
  // In case the Health component is not present in the game object, the function will do nothing instead of throwing an exception.
}