using UnityEngine;

public class PlayerRespawn: MonoBehaviour {
  //This script handles player respawning in the game.

  [SerializeField] private AudioClip checkpoint; //Sound clip that plays when the player reaches a checkpoint
  private Transform currentCheckpoint; //The current checkpoint that the player will respawn at
  private Health playerHealth; //Reference to the player's health script
  private UIManager uiManager; //Reference to the UI Manager script

  private void Awake() {
    playerHealth = GetComponent < Health > (); //Get the health script component on the player object
    uiManager = FindObjectOfType < UIManager > (); //Find the UI Manager script in the scene
  }

  public void RespawnCheck() {
    if (currentCheckpoint == null) //If there is no checkpoint set
    {
      uiManager.GameOver(); //Call the game over function in the UI Manager
      return;
    }

    playerHealth.Respawn(); //Restore player health and reset animation
    transform.position = currentCheckpoint.position; //Move the player to the checkpoint's position

    //Move the camera to the checkpoint's room
    Camera.main.GetComponent < CameraController > ().MoveToNewRoom(currentCheckpoint.parent);
  }
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Checkpoint") {
      currentCheckpoint = collision.transform; //Set the current checkpoint to the collision object's transform
      SoundManager.instance.PlaySound(checkpoint); //Play the checkpoint sound
      collision.GetComponent < Collider2D > ().enabled = false; //Disable the checkpoint's collider
      collision.GetComponent < Animator > ().SetTrigger("activate"); //Activate the checkpoint's animation
    }
  }

  //This script listens for the player to enter a trigger collider with the tag "Checkpoint". 
  //When it does, it sets the currentCheckpoint to that object's transform and plays a sound.
  // It also disables the checkpoint's collider and triggers the checkpoint's animation. 
  //The RespawnCheck() function is called when the player dies and it moves the player to the last checkpoint reached and restores the player's health. 
  //If there is no checkpoint set, it calls the game over function in the UI Manager.
}