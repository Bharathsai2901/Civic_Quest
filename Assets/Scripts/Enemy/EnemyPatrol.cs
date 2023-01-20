using UnityEngine;

public class EnemyPatrol: MonoBehaviour {
  [Header("Patrol Points")]
  [SerializeField] private Transform leftEdge; // left edge position of the patrol
  [SerializeField] private Transform rightEdge; // right edge position of the patrol
  [Header("Enemy")]
  [SerializeField] private Transform enemy; // reference to the enemy object

  [Header("Movement parameters")]
  [SerializeField] private float speed; // movement speed of the enemy
  private Vector3 initScale; // initial scale of the enemy
  private bool movingLeft; // flag to check if enemy is moving left

  [Header("Idle Behaviour")]
  [SerializeField] private float idleDuration; // duration of idle state
  private float idleTimer; // timer for idle state

  [Header("Enemy Animator")]
  [SerializeField] private Animator anim; // reference to the enemy animator

  private void Awake() {
    initScale = enemy.localScale; // store the initial scale of the enemy
  }
  private void OnDisable() {
    anim.SetBool("moving", false); // set the "moving" bool in the animator to false
  }

  private void Update() {
    if (movingLeft) // check if enemy is moving left
    {
      if (enemy.position.x >= leftEdge.position.x) // check if enemy has reached left edge
        MoveInDirection(-1); // move enemy to the left
      else
        DirectionChange(); // change direction
    } else {
      if (enemy.position.x <= rightEdge.position.x) // check if enemy has reached right edge
        MoveInDirection(1); // move enemy to the right
      else
        DirectionChange(); // change direction
    }
  }

  private void DirectionChange() {
    anim.SetBool("moving", false); // set the "moving" bool in the animator to false
    idleTimer += Time.deltaTime; // increment idle timer

    if (idleTimer > idleDuration) // check if idle duration has been reached
      movingLeft = !movingLeft; // change direction
  }

  private void MoveInDirection(int _direction) {
    idleTimer = 0; // reset idle timer
    anim.SetBool("moving", true); // set the "moving" bool in the animator to true

    //Make enemy face direction
    enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
      initScale.y, initScale.z);

    //Move in that direction
    enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
      enemy.position.y, enemy.position.z);
  }

}