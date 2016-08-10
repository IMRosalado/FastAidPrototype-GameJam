/*Bed.cs
 * Author: Alvin Carpio
 * 
 * This script handles the bed to determine if it is occupied or not
 * 
 * 
 * */
using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour {
	public bool isOccupied=false; // boolean if the bed is occupied by a patient

	#region Monobehavior
	void OnTriggerEnter(Collider x){ //colission detection
		if (x.gameObject.tag=="Patient"){
			isOccupied=true;
		}
	}
	void OnTriggerExit(Collider x){
		if (x.gameObject.tag=="Patient"){//collision exit
			isOccupied=false;
		}
	}
	#endregion
}
	