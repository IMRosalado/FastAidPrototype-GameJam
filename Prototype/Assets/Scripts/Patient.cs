using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {
	[HideInInspector]public int injury; //0 for none, 1 - 3 injury
	[HideInInspector]public int injured;
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
	public GameObject player;
	private GameObject clicked;
	private GameObject cureObj;
	private GameObject patientObj;
	private Ray ray;
	private RaycastHit rayHit;
	public Injury pInjury;

	void Awake(){
		isClicked=false;
		onBed=false;
		injured = 0;
		pInjury = new Injury (-999, 0);
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
		
	}

	void Update(){
		PlayerManager.instance.playerIsMoving =isClicked;

		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)) {
				cureObj = rayHit.collider.gameObject;
				if(cureObj.tag == "Cure"){
					if (cureObj.GetComponent<TypeOfInjury> ().type == pInjury.type) {
						pInjury.Cured = 1;

						Debug.Log ("CURE = "+pInjury.Cured);
					}
				}
			}
		}

		decreaseHealth ();

		if (!isWaiting) {
			showBubble ();
			isWaiting = true;
		}

		if (Physics.Raycast (ray, out rayHit)) {
			clicked = rayHit.collider.gameObject;
			patientObj = rayHit.collider.gameObject;
			 if ((clicked.tag == "Bed") && isClicked&& !clicked.GetComponent<Bed>().isOccupied) {
				transform.position = new Vector3(clicked.transform.position.x,clicked.transform.position.y,-1);
				isClicked=false;
				onBed=true;
				isWaiting = false;
			} else if (patientObj == this.gameObject && !onBed) {
				isClicked=true;
			} else if (patientObj == this.gameObject) {
				if (pInjury.Cured==1) {
				player.transform.position = Vector3.MoveTowards(transform.position, patientObj.transform.position, Time.deltaTime*0.1f);
					if(Vector3.Distance(player.transform.position,patientObj.transform.position)<=1f){
					DestroyObject(this.gameObject);
					}
				}
			}else{
				isClicked=false;
			}
			//Debug.Log (pInjury.type);

		}




		if (Physics.Raycast (ray, out rayHit)) {
			clicked = rayHit.collider.gameObject;
			 if(clicked.tag == "Cure"){
				Debug.Log ("BAM!  "+ clicked.GetComponent<TypeOfInjury> ().type + " "+pInjury.type);
				if (clicked.GetComponent<TypeOfInjury> ().type == pInjury.type) {
					pInjury.Cured = 1;

					Debug.Log ("CURE = "+pInjury.Cured);
				}
			}else if ((clicked.tag == "Bed") && isClicked&& !clicked.GetComponent<Bed>().isOccupied) {
				transform.position = new Vector3(clicked.transform.position.x,clicked.transform.position.y,-1);
				isClicked=false;
				onBed=true;
				isWaiting = false;
			} else if (clicked == this.gameObject && !onBed) {
				isClicked=true;
			} else if (clicked == this.gameObject) {
				if (pInjury.Cured==1) {
					DestroyObject(this.gameObject);
				}
			}else{
				isClicked=false;
			}
			//Debug.Log (pInjury.type);
		
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
		if (injured == 0) {
			int randNum = Random.Range (0, injurySprites.Length);
			injured = 1;
			pInjury = new Injury (randNum,0);
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
}
