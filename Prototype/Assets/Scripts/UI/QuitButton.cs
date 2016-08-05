using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene ("MainMenu");

	}
}
