/*PlayButton.cs
 * Author: Isabela Rosalado
 * 
 * This script handles the play and retry Button in the game
 * This button reloads the main scene
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene ("Main");

	}
}
