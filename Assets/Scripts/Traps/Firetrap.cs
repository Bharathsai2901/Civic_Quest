using UnityEngine;
using System.Collections;

public class Firetrap: MonoBehaviour {
  //This script is for a Firetrap object in a game using UnityEngine

    //The amount of damage the firetrap will deal to the player
    [SerializeField] private float damage;

    //Timers for the firetrap's activation and how long it stays active
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    //Reference to the animator component and the sprite renderer component
    private Animator anim;
    private SpriteRenderer spriteRend;

    //Audio clip for the firetrap sound effect
    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    //Boolean variables to check if the trap has been triggered and if it is currently active
    private bool triggered;
    private bool active;

    //Reference to the player's health script
    private Health playerHealth;

    //Retrieves the animator and sprite renderer components on awake
    private void Awake() {
      anim = GetComponent < Animator > ();
      spriteRend = GetComponent < SpriteRenderer > ();
    }

    //If the player is within the firetrap's trigger and it is active, damage the player's health
    private void Update() {
      if (playerHealth != null && active)
        playerHealth.TakeDamage(damage);
    }

    //When the player enters the firetrap's trigger, set the player's health reference and start the firetrap's activation sequence
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.tag == "Player") {
        playerHealth = collision.GetComponent < Health > ();

        if (!triggered)
          StartCoroutine(ActivateFiretrap());

        if (active)
          collision.GetComponent < Health > ().TakeDamage(damage);
      }
    }
    //When the player exits the firetrap's trigger, clear the player's health reference
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.tag == "Player")
        playerHealth = null;
    }
    //Activation sequence for the firetrap, including delays, sound effects, animation, and deactivation
    private IEnumerator ActivateFiretrap() {
      //Set the trigger variable to true, change the sprite color to red to notify the player, and trigger the trap
      triggered = true;
      spriteRend.color = Color.red;

      //Wait for the activation delay, play the firetrap sound effect, change the sprite color back to normal, set the active variable to true, and turn on the activation animation
      yield
      return new WaitForSeconds(activationDelay);
      SoundManager.instance.PlaySound(firetrapSound);
      spriteRend.color = Color.white;
      active = true;
      anim.SetBool("activated", true);

      //Wait for the active time, set the active variable to false, set the trigger variable to false, and turn off the activation animation
      yield
      return new WaitForSeconds(activeTime);
      active = false;
      triggered = false;
      anim.SetBool("activated", false);
    }
  }
