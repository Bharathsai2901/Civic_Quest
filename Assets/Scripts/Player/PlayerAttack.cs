using UnityEngine;

public class PlayerAttack: MonoBehaviour {
  // The amount of time the player must wait before they can attack again
  [SerializeField] private float attackCooldown;
  // The location where the fireball will spawn
  [SerializeField] private Transform firePoint;
  // An array of fireball prefabs
  [SerializeField] private GameObject[] fireballs;
  // The audio clip that plays when the player attacks
  [SerializeField] private AudioClip fireballSound;

  // The Animator component of the player
  private Animator anim;
  // The PlayerMovement component of the player
  private PlayerMovement playerMovement;
  // The timer for the attack cooldown
  private float cooldownTimer = Mathf.Infinity;

  private void Awake() {
    // Get the Animator and PlayerMovement components of the player
    anim = GetComponent < Animator > ();
    playerMovement = GetComponent < PlayerMovement > ();
  }

  private void Update() {
    // Check if the player is pressing the left mouse button and the cooldown timer has expired, 
    // the player can attack, and the game is not paused
    if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack() &&
      Time.timeScale > 0)
      Attack();

    // Increment the cooldown timer
    cooldownTimer += Time.deltaTime;
  }

  private void Attack() {
    // Play the fireball sound
    SoundManager.instance.PlaySound(fireballSound);
    // Set the attack trigger on the Animator component
    anim.SetTrigger("attack");
    // Reset the cooldown timer
    cooldownTimer = 0;

    // Set the position of the next available fireball to the fire point
    fireballs[FindFireball()].transform.position = firePoint.position;
    // Set the direction of the fireball based on the player's scale
    fireballs[FindFireball()].GetComponent < Projectile > ().SetDirection(Mathf.Sign(transform.localScale.x));
  }
  // Returns the index of the next available fireball in the fireballs array
  private int FindFireball() {
    for (int i = 0; i < fireballs.Length; i++) {
      if (!fireballs[i].activeInHierarchy)
        return i;
    }
    return 0;
  }

}