using UnityEngine;

public class SoundManager: MonoBehaviour {
  public static SoundManager instance {
    get;
    private set;
  } // A static instance of the SoundManager class that can be accessed from other scripts
  private AudioSource soundSource; // AudioSource component for sound effects
  private AudioSource musicSource; // AudioSource component for music

  private void Awake() {
    soundSource = GetComponent < AudioSource > (); // Assign the AudioSource component on the current GameObject to the soundSource variable
    musicSource = transform.GetChild(0).GetComponent < AudioSource > (); // Assign the AudioSource component on the first child GameObject of the current GameObject to the musicSource variable

    //Keep this object even when we go to new scene
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    //Destroy duplicate gameobjects
    else if (instance != null && instance != this)
      Destroy(gameObject);

    //Assign initial volumes
    ChangeMusicVolume(0);
    ChangeSoundVolume(0);
  }
  public void PlaySound(AudioClip _sound) {
    soundSource.PlayOneShot(_sound); // Play the passed in AudioClip as a one-shot sound using the soundSource variable
  }

  public void ChangeSoundVolume(float _change) {
    ChangeSourceVolume(1, "soundVolume", _change, soundSource); // Call the ChangeSourceVolume method to change the volume of the soundSource variable
  }
  public void ChangeMusicVolume(float _change) {
    ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource); // Call the ChangeSourceVolume method to change the volume of the musicSource variable
  }

  private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source) {
    //Get initial value of volume and change it
    float currentVolume = PlayerPrefs.GetFloat(volumeName, 1); // Retrieve the current volume level from PlayerPrefs, or set it to 1 if it doesn't exist
    currentVolume += change; // Add the passed in change value to the current volume level

    //Check if we reached the maximum or minimum value
    if (currentVolume > 1)
      currentVolume = 0;
    else if (currentVolume < 0)
      currentVolume = 1;

    //Assign final value
    float finalVolume = currentVolume * baseVolume;
    source.volume = finalVolume;

    //Save final value to player prefs
    PlayerPrefs.SetFloat(volumeName, currentVolume); // Save the final volume level to PlayerPrefs
  }

}