/*spawn.cs
 * Author: Alvin Carpio
 * 
 * This script manages how and when the patients spawn on the level
 * 
 * 
 * */
using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
	public int max=10;
	int i=0;
	public GameObject _spawn;
	#region Monobehavior 
	void Start () {
		StartCoroutine(Spawn(_spawn));
	}
	#endregion

	IEnumerator Spawn(GameObject toSpawn){
		while(i<max){
			GameObject s = (GameObject)Instantiate(toSpawn, new Vector3(-8f, -0.44f, 0), Quaternion.identity); //duplicates the patient every
			s.SetActive(true);
			yield return new WaitForSeconds(10f);//10 seconds
			i++;
		}

	}
}
