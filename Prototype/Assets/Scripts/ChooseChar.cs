using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChooseChar : MonoBehaviour {

	public void OnClickMale(){
		PlayerPrefs.SetString ("GENDER", "Male");
		load ();
	}
	public void OnClickFemale(){
		PlayerPrefs.SetString ("GENDER", "Female");
		load ();
	}

	void load(){
		SceneManager.LoadScene ("Main");
	}


}
