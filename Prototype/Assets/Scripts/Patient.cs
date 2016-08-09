﻿using UnityEngine;
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
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}

	void Update(){
		PlayerManager.instance.playerIsMoving =isClicked;
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
				if(clicked.tag == "Cure"){
					if (clicked.GetComponent<TypeOfInjury> ().type == pInjury.type) {
						pInjury.Cured = 1;
					}
				}else if ((clicked.tag == "Bed") && isClicked&& !clicked.GetComponent<Bed>().isOccupied) {
					transform.position = new Vector3(clicked.transform.position.x,clicked.transform.position.y,-1);
					isClicked=false;
					onBed=true;
					isWaiting = false;
				} else if (clicked == this.gameObject && !onBed) {
					player.GetComponent<PlayerManager> ().DontMove();
					isClicked=true;
				} else if (clicked == this.gameObject) {
					if (pInjury.Cured==1) {
						player.GetComponent<PlayerManager> ().MoveMedic (clicked);
					}
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
			destroyMe ();
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

	public void curePatient(){
		bubbleRenderer.gameObject.SetActive (false);
		UIManager.instance.updateScore (100);
		Invoke ("destroyMe",2f);

	}
	void destroyMe(){
		Destroy (this.gameObject);
	}
}
