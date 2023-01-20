// This script is used to control the behavior of a door object in Unity
using UnityEngine;

public class Door: MonoBehaviour {
  // SerializeField allows the variable to be visible in the Unity editor, but keeps it private in the code
  [SerializeField] private Transform previousRoom; // Reference to the previous room the player was in
  [SerializeField] private Transform nextRoom; // Reference to the next room the player will enter
  [SerializeField] private CameraController cam; // Reference to the camera controller script
  [SerializeField] private GameObject winScreen;
  [SerializeField] private AudioClip winSound;

  private void Awake() {
    // Get the main camera's CameraController component and store it in the cam variable
    cam = Camera.main.GetComponent < CameraController > ();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    // Check if the object that entered the trigger has the "Player" tag
    if (collision.tag == "Player") {
      if(nextRoom == null) {
        winScreen.SetActive(true);
        //SoundManager.instance.PlaySound(winSound);
      }
      // Check if the player is entering the door from the left or right
     else if (collision.transform.position.x < transform.position.x) {
        // Move the camera to the next room
        cam.MoveToNewRoom(nextRoom);
        // Activate the next room and deactivate the previous room
        nextRoom.GetComponent < Room > ().ActivateRoom(true);
        previousRoom.GetComponent < Room > ().ActivateRoom(false);
      } else {
        // Move the camera to the previous room
        cam.MoveToNewRoom(previousRoom);
        // Activate the previous room and deactivate the next room
        previousRoom.GetComponent < Room > ().ActivateRoom(true);
        nextRoom.GetComponent < Room > ().ActivateRoom(false);
      }
    }
  }

}