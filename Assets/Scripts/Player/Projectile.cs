using UnityEngine;

public class Projectile: MonoBehaviour {
  [SerializeField] private float speed; //Speed at which the projectile moves
  private float direction; //Direction the projectile is moving in
  private bool hit; //Boolean to check if the projectile has hit something
  private float lifetime; //How long the projectile has existed for
  //private float t = 0.035f;

  private Animator anim; //Reference to the animator component
  private BoxCollider2D boxCollider; //Reference to the box collider component

  private void Awake() {
    anim = GetComponent < Animator > (); //Get the animator component from the object
    boxCollider = GetComponent < BoxCollider2D > (); //Get the box collider component from the object
  }
  private void Update() {
    if (hit) return; //If the projectile has hit something, don't move it
    //float movementSpeed=0;
    float movementSpeed = speed * (Time.deltaTime) * direction; //Calculate the movement speed based on speed, deltaTime, and direction
    transform.Translate(movementSpeed, 0, 0); //Move the projectile in the x-axis

    lifetime += Time.deltaTime; //Increase the lifetime of the projectile
    if (lifetime > 5) gameObject.SetActive(false); //If the lifetime of the projectile is greater than 5 seconds, deactivate the object
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    hit = true; //The projectile has hit something
    boxCollider.enabled = false; //Disable the box collider so the projectile doesn't keep hitting things
    anim.SetTrigger("explode"); //Play the explode animation

    if (collision.tag == "Enemy")
      collision.GetComponent < Health > ()?.TakeDamage(1); //If the projectile hit an enemy, take 1 damage
  }
  public void SetDirection(float _direction) {
    lifetime = 0; //Reset the lifetime of the projectile
    direction = _direction; //Set the direction of the projectile
    gameObject.SetActive(true); //Activate the object
    hit = false; //The projectile has not hit anything yet
    boxCollider.enabled = true; //Enable the box collider

    float localScaleX = transform.localScale.x; //Get the current x scale of the object
    if (Mathf.Sign(localScaleX) != _direction) //If the sign of the x scale is not the same as the direction of the projectile
      localScaleX = -localScaleX; //Flip the x scale

    transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); //Set the local scale of the object
  }
  private void Deactivate() {
    gameObject.SetActive(false); //Deactivate the object
  }

}