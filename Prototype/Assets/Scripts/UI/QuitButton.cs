/*QuitButton.cs
 * Author: Isabela Rosalado
 * 
 * This script handles the quit Button in the game
 * This button loads the main menu scene
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene ("MainMenu");

	}
}
