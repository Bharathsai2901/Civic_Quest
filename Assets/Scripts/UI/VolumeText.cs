using UnityEngine;
using UnityEngine.UI;

public class VolumeText: MonoBehaviour {
  // The VolumeText class is used to display the current volume level for a specific audio source in a game.

  [SerializeField] private string volumeName; // The name of the player pref used to store the volume level for the audio source.
  [SerializeField] private string textIntro; // A string that is displayed before the volume level, such as "Sound: " or "Music: ".
  private Text txt; // A reference to the Text component used to display the volume level.

  private void Awake() {
    txt = GetComponent < Text > (); // Get the Text component on the same GameObject as this script.
  }

  private void Update() {
    UpdateVolume(); // Update the volume level displayed on screen each frame.
  }

  private void UpdateVolume() {
    float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100; // Retrieve the volume level from player prefs and multiply by 100 to display as a percentage.
    txt.text = textIntro + volumeValue.ToString(); // Set the text of the Text component to the textIntro string, followed by the volume level.
  }
}