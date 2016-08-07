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
					Debug.Log (clicked.tag);
					if (clicked.tag == "Bed" || clicked.tag == "Cure") {
						playerIsMoving = true;
						//pathFinding.MovePlayer (player, clicked, speedPlayer, -1);
						Debug.Log("move");
						StartCoroutine("TeleportPlayer",clicked.transform.position);
					} 
				}
			}
		}
	}

	IEnumerator TeleportPlayer(Vector3 des){
		//yield return new WaitForSeconds (2f);
		while (Vector3.Distance (transform.position, des) > 0.001f) {
			transform.position = Vector3.MoveTowards(transform.position, des, speedPlayer*Time.deltaTime);
			yield return 0;
		
		}

		playerIsMoving = false;

	}
}

