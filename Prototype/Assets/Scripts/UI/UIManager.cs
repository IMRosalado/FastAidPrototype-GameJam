using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public UISprite[] lives;
	private int livesLeft = 3;
	public static UIManager instance;


	void Start(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

	}

	public void loseLife(){
		lives [3 - livesLeft].spriteName = "grave";
		livesLeft--;

	}
}
