using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour {
	public bool isOccupied=false;

	void OnTriggerEnter(Collider x){
		if (x.gameObject.tag=="Patient"){
			isOccupied=true;
		}
	}
	void OnTriggerExit(Collider x){
		if (x.gameObject.tag=="Patient"){
			isOccupied=false;
		}
	}

	void Update(){
	}
}
	