using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance = null;

	public float speedPlayer = 3f;
	public Pathfinding pathFinding;
	[HideInInspector]public bool playerIsMoving = false;

	private GameObject player;
	private GameObject clicked;
	private Ray ray;
	private RaycastHit rayHit;
	//public GameObject bubble;
	public Sprite[] bubSprite;
	public SpriteRenderer rend;


	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		//DontDestroyOnLoad (gameObject);
		pathFinding = GameObject.Find ("GameManager").GetComponent<Pathfinding> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update () {
		MovePlayer ();
	}

	//move player on click
	void MovePlayer(){
		if (!playerIsMoving) {
			if (Input.GetMouseButtonDown (0)) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if(Physics.Raycast(ray, out rayHit)) {
					clicked = rayHit.collider.gameObject;
					if (clicked.tag == "Bed" || clicked.tag == "Cure" || clicked.tag=="Patient") {
						playerIsMoving = true;
						//pathFinding.MovePlayer (player, clicked, speedPlayer, -1);
						StartCoroutine("TeleportPlayer",clicked.transform.position);


					} 
				}
			}
		}
	}

	public void MoveMedic(GameObject x){
		StartCoroutine("TeleportPlayer",x.transform.position);
	}

	IEnumerator TeleportPlayer(Vector3 des){
		//yield return new WaitForSeconds (2f);
		while (Vector3.Distance (transform.position, des) > 0.001f) {
			transform.position = Vector3.MoveTowards(transform.position, des, speedPlayer*Time.deltaTime);
			yield return 0;
		
		}

		playerIsMoving = false;
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders[i].gameObject.tag=="Patient"){
				rend.gameObject.SetActive (false);
				hitColliders[i].gameObject.GetComponent<Patient>().curePatient ();
			}if (hitColliders[i].gameObject.tag== "Cure") {
				StartCoroutine(ShowBubble(clicked.GetComponent<TypeOfInjury>().type));
			}
		}
	}



	IEnumerator ShowBubble(int x){
				rend.sprite = bubSprite [x];
				rend.gameObject.SetActive (true);
			


		yield return 0;
	}
	public void DontMove(){
		StopCoroutine ("TeleportPlayer");
	}
}

