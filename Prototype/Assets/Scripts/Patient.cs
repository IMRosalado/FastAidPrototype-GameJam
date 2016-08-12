/*Patient.cs
 * Author: Isabela Rosalado and Alvin Carpio
 * 
 * This script handles the patient mechanics like randomly generating the injury type and priority level
 * 
 * 
 * */
using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {
	
	[HideInInspector]public bool isClicked,onBed;
	[HideInInspector]public int injury; //0 for none, 1 - 3 injury
	[HideInInspector]public int injured;
	public GameObject player;
	public float health=500;
	public int rateOfDeath=20;
	public bool isWaiting = true;
	public SpriteRenderer bubbleRenderer;
	public SpriteRenderer rend;
	public Sprite[] patientSprites;
	public Sprite[] injurySprites;
	public string injuryType;
	public Injury pInjury;

	private Vector3 screenPoint;
	private Vector3 offset;
	private Collision col;
	private GameObject clicked;
	private Ray ray;
	private RaycastHit rayHit;
	private GameObject clickedCure;

	#region Monobehaviour
	void Awake(){
		isClicked=false;
		onBed=false;
		injured = 0;
		pInjury = new Injury ("99999", 0);
		}
		
	void OnEnable(){
		rend.sprite = patientSprites [Random.Range (0, patientSprites.Length)];
		injury = Random.Range (1, 4);
	
			if (injury == 1) {
				rend.color = new Color (255f, 255f, 0f);
				rateOfDeath = 5;
			} else if (injury == 2) {
				rend.color = new Color (255f, 0f, 255f);
				rateOfDeath = 10;
			} else if (injury == 3) {
				rend.color = new Color (255f, 0f, 0f);
				rateOfDeath = 15;
			} else
				rend.color = new Color (255f, 255f, 255f);
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}

	void Update(){
		PlayerManager.instance.playerIsMoving = isClicked;
		if (isClicked) {
			rend.color = new Color (255f, 255f, 255f);
		} else {
			if (injury == 1) {
				rend.color = new Color (255f, 255f, 0f);
			} else if (injury == 2) {
				rend.color = new Color (255f, 0f, 255f);
			} else if (injury == 3) {
				rend.color = new Color (255f, 0f, 0f);
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)) {
				clicked = rayHit.collider.gameObject;
				//patientObj = rayHit.collider.gameObject;
					if ((clicked.tag == "Bed") && isClicked&& !clicked.GetComponent<Bed>().isOccupied) { //checks if the clicked obejct is a bed and occupide
					transform.position = new Vector3(clicked.transform.position.x,clicked.transform.position.y,-1);
					isClicked=false;
					onBed=true;
					isWaiting = false;
				} else if (clicked == this.gameObject && !onBed) { //checks if the patient is on bed
					player.GetComponent<PlayerManager> ().DontMove();
					isClicked=true;
				} else{
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
	#endregion

	#region health
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
			destroyMe ();
		}

	}
	#endregion
	private void showBubble(){
		if (injured == 0) {
			int randNum = Random.Range (0, injurySprites.Length);
			injured = 1;

			bubbleRenderer.sprite = injurySprites [randNum];
			if (randNum == 0) {
				injuryType = "gunshot";
				pInjury = new Injury (injuryType,0);
			} else if (randNum == 1) {
				injuryType = "brokenBone";
				pInjury = new Injury (injuryType,0);
			} else if (randNum == 2) {
				injuryType = "burn";
				pInjury = new Injury (injuryType,0);
			}
			Debug.Log(injuryType+"-----");
			bubbleRenderer.gameObject.SetActive (true);
		}
	}

	public void curePatient(){
		
		bubbleRenderer.gameObject.SetActive (false);
		UIManager.instance.updateScore (100);

		Invoke ("destroyMe",2f);

	}
	void destroyMe(){
		Destroy (this.gameObject);
	}
}
