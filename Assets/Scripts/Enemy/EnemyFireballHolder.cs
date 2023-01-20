using UnityEngine;

public class EnemyFireballHolder: MonoBehaviour {
  [SerializeField] private Transform enemy; // The Transform component of the enemy GameObject that this script is attached to

  private void Update() {
    transform.localScale = enemy.localScale; // updates the localScale of this GameObject to match the localScale of the enemy GameObject.
  }

}