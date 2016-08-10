/*UIManager.cs
 * Author: Isabela Rosalado
 * 
 * This script handles UI mechanics like displaying and updating the score and losing lives 
 *
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public UISprite[] lives;
	private int livesLeft = 3;
	public static UIManager instance;
	private static int score = 0;
	public UILabel scoreLabel;

	#region Monobehaviour
	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

	}
	void Update(){
		if (livesLeft <= 0) {
			PlayerPrefs.SetInt ("SCORE", score);
			SceneManager.LoadScene ("GameOver");
		}

	}
	#endregion

	#region lives
	/* This function is called when a patient dies
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
	public void loseLife(){
		lives [3 - livesLeft].spriteName = "grave";
		livesLeft--;

	}
	#endregion

	#region scores
	/* This function is called when a patient is successfully cured
	 * 
	 * parameters: x:amount to increase score
	 * return: none
	 * 
	 * */
	public void updateScore(int x){
		score += x;
		scoreLabel.text = "Score: " + score;
	}
	#endregion
}
