using UnityEngine;

public class EnemyProjectile: EnemyDamage {
  // The EnemyProjectile class is a MonoBehaviour script that controls the behavior of a enemy projectile in a game using UnityEngine. It inherits from the EnemyDamage class, which controls the damage dealt by an enemy.

  [SerializeField] private float speed;
  // speed is a float value that determines the speed of the projectile's movement.
  [SerializeField] private float resetTime;
  // resetTime is a float value that determines how long the projectile is active before deactivating.
  private float lifetime;
  // lifetime is a float value that keeps track of how long the projectile has been active.
  private Animator anim;
  // anim is an Animator component that is used to play an animation when the projectile hits an object.
  private BoxCollider2D coll;
  // coll is a BoxCollider2D component that is used to detect when the projectile hits an object.

  private bool hit;
  // hit is a boolean value that keeps track of whether the projectile has hit an object.

  private void Awake() {
    anim = GetComponent < Animator > ();
    coll = GetComponent < BoxCollider2D > ();
  }
  // The Awake function is called when the script instance is being loaded. It gets the Animator and BoxCollider2D components of the projectile.

  public void ActivateProjectile() {
    hit = false;
    lifetime = 0;
    gameObject.SetActive(true);
    coll.enabled = true;
  }
  // The ActivateProjectile function is called to activate the projectile. It sets the hit variable to false, the lifetime variable to 0, sets the gameObject to active, and enables the collider.

  private void Update() {
    if (hit) return;
    float movementSpeed = speed * Time.deltaTime;
    transform.Translate(movementSpeed, 0, 0);

    lifetime += Time.deltaTime;
    if (lifetime > resetTime)
      gameObject.SetActive(false);
  }
  // The Update function is called every frame. 
  //It checks if the projectile has hit an object. 
  //If it has, the function exits. If not, it moves the projectile based on the speed and Time.deltaTime values, and it increases the lifetime variable by Time.deltaTime. 
  //If the lifetime is greater than the resetTime, the function deactivates the gameObject.

  private void OnTriggerEnter2D(Collider2D collision) {
    hit = true;
    base.OnTriggerEnter2D(collision); //Execute logic from parent script first
    coll.enabled = false;

    if (anim != null)
      anim.SetTrigger("explode"); //When the object is a fireball explode it
    else
      gameObject.SetActive(false); //When this hits any object deactivate arrow
  }
  //The OnTriggerEnter2D function is called when the projectile collides with a trigger collider. 
  //It sets the hit variable to true, disables the collider, calls the OnTriggerEnter2D function of the parent script (EnemyDamage) first to deal damage to the player, and if the animator is not null it triggers the explode animation, otherwise it deactivates the game object.

  private void Deactivate() {
    gameObject.SetActive(false);
  }
  // The Deactivate function is called to deactivate the projectile.
  // It sets the gameObject to inactive. It's not clear from the code where this function is called from.
}