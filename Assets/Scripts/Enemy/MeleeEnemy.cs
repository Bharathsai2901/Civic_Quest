using UnityEngine;

public class MeleeEnemy: MonoBehaviour {
  [Header("Attack Parameters")]
  [SerializeField] private float attackCooldown; // cooldown between attacks
  [SerializeField] private float range; // range of attack
  [SerializeField] private int damage; // damage dealt on attack

  [Header("Collider Parameters")]
  [SerializeField] private float colliderDistance; // distance of the collider from the enemy object
  [SerializeField] private BoxCollider2D boxCollider; // reference to the box collider component

  [Header("Player Layer")]
  [SerializeField] private LayerMask playerLayer; // layer of the player object
  private float cooldownTimer = Mathf.Infinity; // timer for attack cooldown
  [SerializeField] private AudioClip fireballSound; // sound played when attacking

  //References
  private Animator anim; // reference to the animator component
  private Health playerHealth; // reference to the player's health script
  private EnemyPatrol enemyPatrol; // reference to the parent's enemy patrol script

  private void Awake() {
    anim = GetComponent < Animator > (); // get reference to the animator component
    enemyPatrol = GetComponentInParent < EnemyPatrol > (); // get reference to the parent's enemy patrol script
  }

  private void Update() {
    cooldownTimer += Time.deltaTime; // increment cooldown timer

    //Attack only when player in sight?
    if (PlayerInSight()) {
      if (cooldownTimer >= attackCooldown) // check if cooldown has finished
      {
        cooldownTimer = 0; // reset cooldown timer
        anim.SetTrigger("meleeAttack"); // trigger the melee attack animation
        SoundManager.instance.PlaySound(fireballSound); // play the attack sound
      }
    }

    if (enemyPatrol != null)
      enemyPatrol.enabled = !PlayerInSight(); // disable enemy patrol if player is in sight
  }

  private bool PlayerInSight() {
    RaycastHit2D hit =
      Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer); //Projecting a Ray to detect player using box

    if (hit.collider != null)
      playerHealth = hit.transform.GetComponent < Health > ();

    return hit.collider != null; // return true if the player is in sight, false otherwise
  }
  private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
      new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
  }

  private void DamagePlayer() {
    if (PlayerInSight()) // check if player is in sight
      playerHealth.TakeDamage(damage); // deal

  }
}