/*PlayerManager.cs
 * Author: Isabela Rosalado
 * 
 * This script handles the player mechanics like moving the player on click, changing player gender, etc.
 * 
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance = null;

	[HideInInspector]public bool playerIsMoving = false;
	public float speedPlayer = 3f;
	public Pathfinding pathFinding;
	public SpriteRenderer rendPlayer;
	public Sprite[] playerSprites;

	private GameObject player;
	private GameObject clicked;
	private Ray ray;
	private RaycastHit rayHit;
	public Sprite[] bubSprite;
	public SpriteRenderer rend;
	private bool doesCarry=false;

	#region Monobehaviour
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		pathFinding = GameObject.Find ("GameManager").GetComponent<Pathfinding> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Start(){
		LoadPlayer ();

	}
	void Update () {
		MovePlayer ();
	}
	#endregion

	#region player movement
	/* This function is called per update and moves the player when playerIsMoving is false
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
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
	/* This function is called outside of the class and moves the player to the given destination
	 * 
	 * parameters: x:target destination
	 * return: none
	 * 
	 * */
	public void MoveMedic(GameObject x){
		StartCoroutine("TeleportPlayer",x.transform.position);
	}
	/* This Coroutine is called per to move the player gameobject ot the destination
	 * This does not have a path-finding algorithm
	 * 
	 * parameters: des:destination
	 * return: none
	 * 
	 * */
	IEnumerator TeleportPlayer(Vector3 des){
		//yield return new WaitForSeconds (2f);
		while (Vector3.Distance (transform.position, des) > 0.001f) {
			transform.position = Vector3.MoveTowards(transform.position, des, speedPlayer*Time.deltaTime);
			yield return 0;
		
		}
		playerIsMoving = false;
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders[i].gameObject.tag=="Patient" && doesCarry){
				rend.gameObject.SetActive (false);
				doesCarry =false;
				if(hitColliders[i].gameObject.GetComponent<Patient>().pInjury.type == ){
				hitColliders[i].gameObject.GetComponent<Patient>().curePatient ();
				}
			}if (hitColliders[i].gameObject.tag== "Cure") {
				doesCarry =true;
				ShowBubble(clicked.GetComponent<TypeOfInjury>().type);
			}
		}
	}
	/* This function is called to stop the movement of the player
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
	public void DontMove(){
		StopCoroutine ("TeleportPlayer");
	}
	#endregion

	#region player display
	/* This function is called to display an item the player is holding
	 * 
	 * parameters: x:the type of item the player will hold
	 * return: none
	 * 
	 * */
	void ShowBubble(int x){
		rend.sprite = bubSprite [x];
		rend.gameObject.SetActive (true);
	}
	/* This function is called at the start of the scene to render the player sprite and speed
	 * 
	 * parameters: none
	 * return: none
	 * 
	 * */
	void LoadPlayer(){
		string x = PlayerPrefs.GetString ("GENDER");
		if (x == "Female") {
			rendPlayer.sprite = playerSprites [0];
			speedPlayer = 3f;
		} else {
			rendPlayer.sprite = playerSprites [1];
			speedPlayer = 5f;
		}

	}
	#endregion
}

