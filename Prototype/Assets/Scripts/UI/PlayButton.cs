using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene ("Main");

	}
}
