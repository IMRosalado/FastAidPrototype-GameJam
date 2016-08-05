using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour {
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
		
}
