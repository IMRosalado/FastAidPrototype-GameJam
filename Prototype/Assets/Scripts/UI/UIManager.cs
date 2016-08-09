using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public UISprite[] lives;
	private int livesLeft = 3;
	public static UIManager instance;
	private static int score = 0;
	public UILabel scoreLabel;


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
	public void loseLife(){
		lives [3 - livesLeft].spriteName = "grave";
		livesLeft--;

	}
	public void updateScore(int x){
		score += x;
		scoreLabel.text = "Score: " + score;
	}
	public int getScore(){
		return score;
	}
}
