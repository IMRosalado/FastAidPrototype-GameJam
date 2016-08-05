using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {
<<<<<<< HEAD
	private Vector3 screenPoint;
	private Vector3 offset;
	private Collision col;
	public bool b,onBed;
	private GameObject player;
	private GameObject clicked,bed;
	private Ray ray;
	private RaycastHit rayHit;

	void Awake(){
		b=false;
		onBed=false;
	}



	void Update(){
		PlayerManager.instance.playerIsMoving = b;

		if (Input.GetMouseButtonDown (0) && b) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)) {
				bed = rayHit.collider.gameObject;
				Debug.Log("BED=----"+bed.tag+bed.name);
				if ((bed.tag == "Target" || bed.tag == "Target") && !bed.GetComponent<Bed>().mayTao) {
					transform.position = bed.transform.position;
					b=false;
					onBed=true;
				} 
			}
		}
		else if (Input.GetMouseButtonDown (0) && !onBed) {
			
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)) {
				clicked = rayHit.collider.gameObject;
				Debug.Log (clicked.tag);
				if (clicked == this.gameObject) {
					b=true;
				}else{
					b=false;
				}
			}
		}


	}
		
=======

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
>>>>>>> e62cd4738a78c0ffc487b58cf3e1dc19af0ec63f
}
