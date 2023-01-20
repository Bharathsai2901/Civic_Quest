using UnityEngine;

public class Spikehead: EnemyDamage {
  // The Spikehead class is a subclass of the EnemyDamage class and is used to define the behavior of a specific enemy type in a game.

  [Header("SpikeHead Attributes")]
  [SerializeField] private float speed; // The speed at which the spikehead moves towards its destination.
  [SerializeField] private float range; // The maximum distance at which the spikehead can detect the player.
  [SerializeField] private float checkDelay; // The amount of time between each check for the player's presence.
  [SerializeField] private LayerMask playerLayer; // The layer mask used to detect the player when casting rays.
  private Vector3[] directions = new Vector3[4]; // An array of four directions (right, left, up, and down) that the spikehead can detect the player in.
  private Vector3 destination; // The position the spikehead is moving towards.
  private float checkTimer; // A timer used to track the time since the last check for the player's presence.
  private bool attacking; // A flag that is true when the spikehead is moving towards the player and false when it is not.

  [Header("SFX")]
  [SerializeField] private AudioClip impactSound; // The sound effect that is played when the spikehead collides with something.

  private void OnEnable() {
    Stop(); // When the object is enabled, stop the spikehead from moving.
  }

  private void Update() {
    //Move spikehead to destination only if attacking
    if (attacking)
      transform.Translate(destination * Time.deltaTime * speed); // Move the spikehead towards its destination at the specified speed.
    else {
      checkTimer += Time.deltaTime;
      if (checkTimer > checkDelay)
        CheckForPlayer(); // Check for the player's presence if the check delay has been reached.
    }
  }

  private void CheckForPlayer() {
    CalculateDirections(); // Calculate the directions in which the spikehead can detect the player.

    //Check if spikehead sees player in all 4 directions
    for (int i = 0; i < directions.Length; i++) {
      Debug.DrawRay(transform.position, directions[i], Color.red); // Draw a debug ray in the current direction to visualize the detection range.
      RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer); // Cast a ray in the current direction and check for a hit on the player layer.

      if (hit.collider != null && !attacking) {
        attacking = true;
        destination = directions[i]; // Set the destination to the direction in which the player was detected.
        checkTimer = 0;
      }
    }
  }

  private void CalculateDirections() {
    directions[0] = transform.right * range; //Right direction
    directions[1] = -transform.right * range; //Left direction
    directions[2] = transform.up * range; //Up direction
    directions[3] = -transform.up * range; //Down direction
  }

  private void Stop() {
    destination = transform.position; //Set destination as current position so it doesn't move
    attacking = false; // Set the attacking flag to false to stop the spikehead from moving.
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    SoundManager.instance.PlaySound(impactSound);
    base.OnTriggerEnter2D(collision);
    Stop(); //Stop spikehead once he hits something
  }
}