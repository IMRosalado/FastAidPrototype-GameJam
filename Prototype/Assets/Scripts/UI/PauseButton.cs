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
