﻿using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
	int i =0;
	public GameObject _spawn;
	void Start () {
		StartCoroutine(Spawn(_spawn));
	}


	IEnumerator Spawn(GameObject toSpawn){
		while(i<5){
			Debug.Log("SPAWN TIME!");
			GameObject s = (GameObject)Instantiate(toSpawn, new Vector3(-8f, -0.44f, 0), Quaternion.identity);
			s.SetActive(true);
			yield return new WaitForSeconds(10f);
			i++;
		}

	}
}
