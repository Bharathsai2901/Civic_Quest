using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager: MonoBehaviour {
  // This script is a loading manager for a Unity project. It allows for easy loading of different levels in the game.

  public static LoadingManager instance {
    get;
    private set;
  } // The singleton instance of the LoadingManager.

  private void Awake() {
    //Keep this object even when we go to new scene
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject); // Keep the LoadingManager object alive when loading a new scene.
    }
    //Destroy duplicate gameobjects
    else if (instance != null && instance != this)
      Destroy(gameObject); // Destroy any duplicate LoadingManager objects.
  }

  public void LoadCurrentLevel() {
    int currentLevel = PlayerPrefs.GetInt("currentLevel", 1); // Get the current level from player preferences.
    SceneManager.LoadScene(currentLevel); // Load the current level.
  }
  public void Restart() {
    //SceneManager.LoadScene(currentLevel);
  }
  // The LoadCurrentLevel function is called to load the current level, as determined by the player preferences.
  // The Restart function is empty, it should be implemented to restart the current level.
}