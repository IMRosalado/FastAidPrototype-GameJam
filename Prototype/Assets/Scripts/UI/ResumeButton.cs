/*ResumeButton.cs
 * Author: Isabela Rosalado
 * 
 * This script resumes the gameplay
 * 
 * 
 * */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResumeButton: MonoBehaviour {

	public GameObject pauseMenu;

	public void OnClick(){
		Time.timeScale = 1;
		pauseMenu.SetActive (false);

	}
}
