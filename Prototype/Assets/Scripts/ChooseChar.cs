/*ChooseChar.cs
 * Author: Isabela Rosalado 
 * 
 * This script handles the choose-character scene. 
 * This script loads the next scene when the player chooses
 * 
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChooseChar : MonoBehaviour {
	/* This function is called when the male character is chosen
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
	public void OnClickMale(){
		PlayerPrefs.SetString ("GENDER", "Male");
		load ();
	}
	/* This function is called when the female character is chosen
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
	public void OnClickFemale(){
		PlayerPrefs.SetString ("GENDER", "Female");
		load ();
	}
	/* This function is called after choosing the character. It loads the next scene
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
	void load(){
		SceneManager.LoadScene ("Main");
	}


}
