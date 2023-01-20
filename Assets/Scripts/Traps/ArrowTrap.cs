using UnityEngine;

public class ArrowTrap: MonoBehaviour {
    // The ArrowTrap class is a MonoBehaviour script that controls the behavior of a arrow trap in a game using UnityEngine.
    [SerializeField] private float attackCooldown;
    // attackCooldown is a float value that determines how frequently the arrow trap can attack.
    [SerializeField] private Transform firePoint;
    // firePoint is a Transform component that determines where the arrow should be fired from.
    [SerializeField] private GameObject[] arrows;
    // arrows is an array of GameObjects that are the prefabs of the arrows that the arrow trap can fire.
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    //The arrowSound variable is an AudioClip that represents the sound that the arrow makes when it is fired.

    private void Attack() {
      cooldownTimer = 0;
      //Attack function resets the cooldownTimer to 0
      SoundManager.instance.PlaySound(arrowSound);
      //Plays the arrowSound
      arrows[FindArrow()].transform.position = firePoint.position;
      //Sets the position of the arrow to the firePoint's position
      arrows[FindArrow()].GetComponent < EnemyProjectile > ().ActivateProjectile();
      //Activates the arrow's projectile component
    }
    private int FindArrow() {
      for (int i = 0; i < arrows.Length; i++) {
        if (!arrows[i].activeInHierarchy)
          return i;
      }
      return 0;
    }
    // The FindArrow function is used to find an inactive arrow in the arrows array and return its index.

    private void Update() {
      cooldownTimer += Time.deltaTime;

      if (cooldownTimer >= attackCooldown)
        Attack();
    }
}
    // The Update function is called every frame. 
    //It increases the cooldownTimer by Time.deltaTime and checks if the cooldownTimer has reached the attackCooldown value.
    // If it has, it calls the Attack function.