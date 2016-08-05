using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {

	public int injury=1; //0 for none, 1 - 3 injury
	public float health=500;
	public int rateOfDeath=20;
	public bool isWaiting = true;
	public GameObject bubble;

	void OnEnable(){
		SpriteRenderer rend=GetComponent<SpriteRenderer> ();
		if (injury == 1) {
			rend.color = new Color(0f,255f,0f);
			rateOfDeath = 5;
		} else if (injury == 2) {
			rend.color = new Color(0f,255f,255f);
			rateOfDeath = 10;
		} else if (injury == 3) {
			rend.color = new Color(255f,0f,0f);
			rateOfDeath = 15;
		} else 
			rend.color = new Color(255f,255f,255f);
			
	
	}

	void Update(){
		health -= rateOfDeath * Time.deltaTime;
		Debug.Log (health);
		if (health < 300)
			isWaiting = false;
		if (health <= 0) {
			Debug.Log ("DIE!");
			UIManager.instance.loseLife ();
			Destroy (gameObject);
		}
		if (!isWaiting) {
			bubble.SetActive (true);
		}
	}

	public void addHealth(float x){
		health += x;
	
	}
}
