using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {
	public int injury=1; //0 for none, 1 - 3 injury
	public float health=500;
	public int rateOfDeath=20;
	public bool isWaiting = true;
	public GameObject bubble;
	public SpriteRenderer rend;
	public Sprite[] patientSprites;

	private Vector3 screenPoint;
	private Vector3 offset;
	private Collision col;
	[HideInInspector]public bool b,onBed;
	private GameObject player;
	private GameObject clicked,bed;
	private Ray ray;
	private RaycastHit rayHit;

	void Awake(){
		b=false;
		onBed=false;
	}
		
	void OnEnable(){
		rend.sprite = patientSprites [Random.Range (0, patientSprites.Length)];
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
		PlayerManager.instance.playerIsMoving = b;

		if (Input.GetMouseButtonDown (0) && b) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray, out rayHit)) {
				bed = rayHit.collider.gameObject;
				if ((bed.tag == "Bed") && !bed.GetComponent<Bed>().isOccupied) {
					transform.position = new Vector3(bed.transform.position.x,bed.transform.position.y,-1);
					b=false;
					onBed=true;
				} 
			}
		}
		else if (Input.GetMouseButtonDown (0) && !onBed) {

			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)) {
				clicked = rayHit.collider.gameObject;
				if (clicked == this.gameObject) {
					b=true;
				}else{
					b=false;
				}
			}
		}

		decreaseHealth ();

		if (!isWaiting) {
			bubble.SetActive (true);
		}
	}

	public void addHealth(float x){
		health += x;
	
	}
	private void decreaseHealth(){
		health -= rateOfDeath * Time.deltaTime;
		if (health < 300)
			isWaiting = false;
		if (health <= 0) {
			Debug.Log ("DIE!");
			UIManager.instance.loseLife ();
			Destroy (gameObject);
		}

	}
}
