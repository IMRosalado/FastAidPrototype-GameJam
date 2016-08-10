/*ScoreLabel.cs
 * Author: Isabela Rosalado
 * 
 * This script handles the score label in the game over scene
 * This script changes the text inside the label to the current score achieved by the player
 * 
 * */

using UnityEngine;
using System.Collections;

public class ScoreLabel : MonoBehaviour {
	void Update () {
		gameObject.GetComponent<UILabel>().text = "Score: " + PlayerPrefs.GetInt("SCORE");
	}
}
