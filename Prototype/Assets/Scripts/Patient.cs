using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {
	[HideInInspector]public int injury; //0 for none, 1 - 3 injury
	public float health=500;
	public int rateOfDeath=20;
	public bool isWaiting = true;
	public SpriteRenderer bubbleRenderer;
	public SpriteRenderer rend;
	public Sprite[] patientSprites;
	public Sprite[] injurySprites;
	public string injuryType;


	private Vector3 screenPoint;
	private Vector3 offset;
	private Collision col;
	[HideInInspector]public bool isClicked,onBed;
	private GameObject player;
	private GameObject clicked;
	private Ray ray;
	private RaycastHit rayHit;

	void Awake(){
		isClicked=false;
		onBed=false;
	}
		
	void OnEnable(){
		rend.sprite = patientSprites [Random.Range (0, patientSprites.Length)];
		injury = 2;
		if (injury == 1) {
			rend.color = new Color(255f,255f,0f);
			rateOfDeath = 5;
		} else if (injury == 2) {
			rend.color = new Color(255f,0f,255f);
			rateOfDeath = 10;
		} else if (injury == 3) {
			rend.color = new Color(255f,0f,0f);
			rateOfDeath = 15;
		} else 
			rend.color = new Color(255f,255f,255f);
			
	
	}

	void Update(){
		PlayerManager.instance.playerIsMoving =isClicked;

		if (Input.GetMouseButtonDown (0) && !onBed) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)) {
				clicked = rayHit.collider.gameObject;
				if ((clicked.tag == "Bed") && isClicked&& !clicked.GetComponent<Bed>().isOccupied) {
					transform.position = new Vector3(clicked.transform.position.x,clicked.transform.position.y,-1);
					isClicked=false;
					onBed=true;
					isWaiting = false;
				} else if (clicked == this.gameObject && !onBed) {
					isClicked=true;
				}else{
					isClicked=false;
				}
			}
		}

		decreaseHealth ();

		if (!isWaiting) {
			showBubble ();
			isWaiting = true;
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
	private void showBubble(){
		int randNum = Random.Range (0, injurySprites.Length);
		bubbleRenderer.sprite = injurySprites [randNum];
		if (randNum == 0) {
			injuryType = "gunshot";
		} else if (randNum == 1) {
			injuryType = "brokenBone";
		} else if (randNum == 2) {
			injuryType = "burn";
		}

		bubbleRenderer.gameObject.SetActive (true);

	}
}
