using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour {
	public bool mayTao=false;

	void OnTriggerEnter(Collider x){
		if (x.gameObject.tag=="Patient"){
			mayTao=true;
		}
	}
	void OnTriggerExit(Collider x){
		if (x.gameObject.tag=="Patient"){
			mayTao=false;
		}
	}

	void Update(){
	}
}
	