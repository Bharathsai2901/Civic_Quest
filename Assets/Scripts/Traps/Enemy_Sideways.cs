using UnityEngine;

public class Enemy_Sideways: MonoBehaviour {
  // The Enemy_Sideways class is a MonoBehaviour script that controls the behavior of a enemy that moves sideways in a game using UnityEngine.

  [SerializeField] private float movementDistance;
  // movementDistance is a float value that determines how far the enemy can move from its starting position.
  [SerializeField] private float speed;
  // speed is a float value that determines the speed of the enemy's movement.
  [SerializeField] private float damage;
  // damage is a float value that determines the amount of damage the enemy can deal to the player.
  private bool movingLeft;
  // movingLeft is a boolean value that determines if the enemy is currently moving to the left.
  private float leftEdge;
  // leftEdge is a float value that determines the leftmost point the enemy can move to.
  private float rightEdge;
  // rightEdge is a float value that determines the rightmost point the enemy can move to.

  private void Awake() {
    leftEdge = transform.position.x - movementDistance;
    rightEdge = transform.position.x + movementDistance;
  }
  // The Awake function is called when the script instance is being loaded. 
  //It sets the leftEdge and rightEdge values based on the enemy's starting position and the movementDistance value.

  private void Update() {
    if (movingLeft) {
      if (transform.position.x > leftEdge) {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
      } else
        movingLeft = false;
    } else {
      if (transform.position.x < rightEdge) {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
      } else
        movingLeft = true;
    }
  }
  // The Update function is called every frame. 
  //It checks if the enemy is currently movingLeft. 
  //If it is, it checks if the enemy's position is greater than the leftEdge.
  // If it is, it moves the enemy to the left based on the speed value and Time.deltaTime. 
  //If it isn't, it sets movingLeft to false. If the enemy is not movingLeft, it checks if the enemy's position is less than the rightEdge. 
  //If it is, it moves the enemy to the right based on the speed value and Time.deltaTime. If it isn't, it sets movingLeft to true.

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == "Player") {
      collision.GetComponent < Health > ().TakeDamage(damage);
    }
  }
  //The OnTriggerEnter2D function is called when the enemy collides with a trigger collider.
  // It checks if the collider's tag is "Player". 
  //If it is, it calls the TakeDamage function of the collider's Health component and deals damage to the player based on the damage value.
}