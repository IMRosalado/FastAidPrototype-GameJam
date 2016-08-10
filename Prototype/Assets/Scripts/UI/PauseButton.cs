/*PauseButton.cs
 * Author: Isabela Rosalado
 * 
 * This script handles the Pause Button in the game
 *
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour {

	public GameObject pauseMenu;

	public void OnClick(){
		Time.timeScale = 0;
		pauseMenu.SetActive (true);

	}
}
