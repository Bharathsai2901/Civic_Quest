using UnityEngine;

public class RangedEnemy: MonoBehaviour {
  [Header("Attack Parameters")]
  [SerializeField] private float attackCooldown; // cooldown between ranged attacks
  [SerializeField] private float range; // range of the ranged attack
  [SerializeField] private int damage; // damage dealt by the ranged attack

  [Header("Ranged Attack")]
  [SerializeField] private Transform firepoint; // position from where the projectiles will be instantiated
  [SerializeField] private GameObject[] fireballs; // array of fireball prefabs

  [Header("Collider Parameters")]
  [SerializeField] private float colliderDistance; // distance of the collider from the enemy object
  [SerializeField] private BoxCollider2D boxCollider; // reference to the box collider component

  [Header("Player Layer")]
  [SerializeField] private LayerMask playerLayer; // layer of the player object
  private float cooldownTimer = Mathf.Infinity; // timer for attack cooldown

  [Header("Fireball Sound")]
  [SerializeField] private AudioClip fireballSound; // sound played when attacking

  //References
  private Animator anim; // reference to the animator component
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
        anim.SetTrigger("rangedAttack"); // trigger the ranged attack animation
      }
    }

    if (enemyPatrol != null)
      enemyPatrol.enabled = !PlayerInSight(); // disable enemy patrol if player is in sight
  }

  private void RangedAttack() {
    SoundManager.instance.PlaySound(fireballSound); // play attack sound
    cooldownTimer = 0; // reset cooldown timer
    fireballs[FindFireball()].transform.position = firepoint.position; // set the position of the fireball
    fireballs[FindFireball()].GetComponent < EnemyProjectile > ().ActivateProjectile(); // activate the projectile
  }
  private int FindFireball() {
    for (int i = 0; i < fireballs.Length; i++) {
      if (!fireballs[i].activeInHierarchy)
        return i;
    }
    return 0;
  }

  private bool PlayerInSight() {
    RaycastHit2D hit =
      Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

    return hit.collider != null;
  }
  private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
      new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
  }
}