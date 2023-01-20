using UnityEngine;

public class Room: MonoBehaviour {
  // The Room class is a MonoBehaviour script that controls the behavior of a room in a game using UnityEngine.
  [SerializeField] private GameObject[] enemies;
  //The SerializeField attribute allows the enemies array to be visible in the Unity editor and can be assigned from the inspector.
  private Vector3[] initialPosition;
  //initialPosition array stores the initial positions of the enemies in the room.

  private void Awake() {
    //Save the initial positions of the enemies
    initialPosition = new Vector3[enemies.Length];
    for (int i = 0; i < enemies.Length; i++) {
      if (enemies[i] != null)
        initialPosition[i] = enemies[i].transform.position;
    }

    //Deactivate rooms
    if (transform.GetSiblingIndex() != 0)
      ActivateRoom(false);

       // The ActivateRoom function is called to activate or deactivate enemies in the room. 
  //It sets the active status of the enemies to the value of _status and also sets their position to their initial position.

  }
  // The Awake function is called when the script instance is being loaded. 
  //It saves the initial positions of the enemies and deactivates all the rooms except the first one.

  public void ActivateRoom(bool _status) {
    //Activate/deactivate enemies
    for (int i = 0; i < enemies.Length; i++) {
      if (enemies[i] != null) {
        enemies[i].SetActive(_status);
        enemies[i].transform.position = initialPosition[i];
      }
    }
  }
}