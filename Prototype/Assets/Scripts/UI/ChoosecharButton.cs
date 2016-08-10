/*ChooscharButton.cs
 * Author: Isabela Rosalado
 * 
 * This script handles the button in the main menu. It loads up the ChooseChar scene 
 * where the player will choose his/her character
 * 
 * 
 * */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChoosecharButton : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene ("ChooseChar");

	}
}
