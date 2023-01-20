using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
//Reference to the player's health script
[SerializeField] private Health playerHealth;
    //References to the total and current health bars in the UI
[SerializeField] private Image totalhealthBar;
[SerializeField] private Image currenthealthBar;

private void Start()
{
    //Initialize the total health bar to the player's starting health
    totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
}
private void Update()
{
    //Update the current health bar to the player's current health
    currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
}

}