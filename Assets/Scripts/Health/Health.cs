using UnityEngine;
using System.Collections;

public class Health: MonoBehaviour {
  // Declare variables for starting health, current health, animator, and dead status
  [Header("Health")]
  [SerializeField] private float startingHealth;
  public float currentHealth {
    get;
    private set;
  }
  private Animator anim;
  private bool dead;

  // Declare variables for invunerability frames, number of flashes, and sprite renderer
  [Header("iFrames")]
  [SerializeField] private float iFramesDuration;
  [SerializeField] private int numberOfFlashes;
  private SpriteRenderer spriteRend;

  // Declare array of behaviours (components) and invulnerability status
  [Header("Components")]
  [SerializeField] private Behaviour[] components;
  private bool invulnerable;

  // Declare death sound and hurt sound
  [Header("Death Sound")]
  [SerializeField] private AudioClip deathSound;
  [SerializeField] private AudioClip hurtSound;

  // On awake, set current health to starting health, get the animator and sprite renderer
  private void Awake() {
    currentHealth = startingHealth;
    anim = GetComponent < Animator > ();
    spriteRend = GetComponent < SpriteRenderer > ();
  }

  // Public function for taking damage, which sets invunerability and plays hurt sound if not dead
  public void TakeDamage(float _damage) {
    if (invulnerable) return;
    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

    if (currentHealth > 0) {
      anim.SetTrigger("hurt");
      StartCoroutine(Invunerability());
      SoundManager.instance.PlaySound(hurtSound);
    } else {
      if (!dead) {
        //Deactivate all attached component classes
        foreach(Behaviour component in components)
        component.enabled = false;

        anim.SetBool("grounded", true);
        anim.SetTrigger("die");

        dead = true;
        SoundManager.instance.PlaySound(deathSound);
      }
    }

  }
  // Public function for adding health
  public void AddHealth(float _value) {
    currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
  }

  // Coroutine for invunerability frames
  private IEnumerator Invunerability() {
    invulnerable = true;
    Physics2D.IgnoreLayerCollision(10, 11, true);
    for (int i = 0; i < numberOfFlashes; i++) {
      spriteRend.color = new Color(1, 0, 0, 0.5f);
      yield
      return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
      spriteRend.color = Color.white;
      yield
      return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    }
    Physics2D.IgnoreLayerCollision(10, 11, false);
    invulnerable = false;
  }

  // Function for deactivating game object
  private void Deactivate() {
    gameObject.SetActive(false);
  }

  //Respawn
  public void Respawn() {
    AddHealth(startingHealth);
    anim.ResetTrigger("die");
    anim.Play("Idle");
    StartCoroutine(Invunerability());
    dead = false;

    //Activate all attached component classes
    foreach(Behaviour component in components)
    component.enabled = true;
  }
}