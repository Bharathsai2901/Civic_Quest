using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour {
  // The MenuManager class is used to handle the behavior of the main menu in a game.

  [SerializeField] private RectTransform arrow; // A reference to the arrow that indicates the currently selected menu option.
  [SerializeField] private RectTransform[] buttons; // An array of references to the buttons that represent the menu options.
  [SerializeField] private AudioClip changeSound; // The sound effect that is played when the arrow is moved to a different button.
  [SerializeField] private AudioClip interactSound; // The sound effect that is played when a button is selected.
  private int currentPosition; // An integer that keeps track of the currently selected button.

  private void Awake() {
    ChangePosition(0); // Set the initial position of the arrow to the first button.
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.UpArrow))
      ChangePosition(-1); // Move the arrow up if the up arrow key is pressed.
    else if (Input.GetKeyDown(KeyCode.DownArrow))
      ChangePosition(1); // Move the arrow down if the down arrow key is pressed.

    if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
      Interact(); // Select the currently highlighted button if the enter key or submit button is pressed.
  }

  public void ChangePosition(int _change) {
    currentPosition += _change; // Update the current position based on the change passed in.

    if (_change != 0)
      SoundManager.instance.PlaySound(changeSound); // Play the change sound effect if the arrow is being moved.

    if (currentPosition < 0)
      currentPosition = buttons.Length - 1; // Wrap around to the last button if the current position is less than 0.
    else if (currentPosition > buttons.Length - 1)
      currentPosition = 0; // Wrap around to the first button if the current position is greater than the number of buttons.

    AssignPosition(); // Update the position of the arrow to match the currently selected button.
  }

  private void AssignPosition() {
    arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y); // Set the position of the arrow to match the Y position of the currently selected button.
  }

  private void Interact() {
    SoundManager.instance.PlaySound(interactSound); // Play the interact sound effect when a button is selected.

    if (currentPosition == 0) {
      //Start game
      SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1)); // Load the level saved in player prefs or level 1 if none is found
    } else if (currentPosition == 1) {
      //Open Settings
    } else if (currentPosition == 2) {
      //Open Credits
    } else if (currentPosition == 3)
      Application.Quit(); // Exit the application if the last button is selected.
  }
}