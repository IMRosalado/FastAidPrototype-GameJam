using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {

	public GameObject _spawn;
	void Start () {
		StartCoroutine(Spawn(_spawn));
	}


	IEnumerator Spawn(GameObject toSpawn){
		while(true){
			GameObject s = (GameObject)Instantiate(toSpawn, new Vector3(-8f, -0.44f, 0), Quaternion.identity);
			s.SetActive(true);
			yield return new WaitForSeconds(5f);
		}

	}
}
