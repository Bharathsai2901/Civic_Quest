using UnityEngine;

// This script is a camera controller for a Unity project. It allows for two different camera behaviors - moving to a new room and following a player.
public class CameraController: MonoBehaviour {
  // Room camera:
  [SerializeField] private float speed; // The speed at which the camera moves to a new room.
  private float currentPosX; // The current X position of the camera.
  private Vector3 velocity = Vector3.zero; // The velocity at which the camera moves.

  // Follow player:
  [SerializeField] private Transform player; // The player object that the camera will follow.
  [SerializeField] private float aheadDistance; // How far ahead of the player the camera should be.
  [SerializeField] private float cameraSpeed; // The speed at which the camera follows the player.
  private float lookAhead; // The distance the camera should be ahead of the player.

  private void Update() {
    // Room camera:
    // Uncomment this line to move the camera to a new room at the specified speed, using smooth damping.
    //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

    // Follow player
    transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z); // Move the camera to a position ahead of the player.
    lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed); // Smoothly change the lookAhead variable to match the aheadDistance variable.
  }

  public void MoveToNewRoom(Transform _newRoom) {
    print("here");
    currentPosX = _newRoom.position.x; // Move the camera to the X position of the new room.
  }
  // The MoveToNewRoom function is called when the player enters a new room, and it moves the camera to the new room's position.
}