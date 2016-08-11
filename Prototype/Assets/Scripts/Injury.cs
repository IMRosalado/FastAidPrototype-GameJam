/*Patient.cs
 * Author: Alvin Carpio
 * 
 * This script is for injury class that contains injury type and boolean cured
 * 
 * 
 * */
using UnityEngine;
using System.Collections;

public class Injury {
	public string type; //type of injury
	public int cured; //Boolean 1 if true else 0

	#region Constructor
	public Injury(string _type, int _cured){
		this.type = _type;
		this.cured = _cured;
	}
	#endregion

	#region Setter Getter
	public int Cured {
		get;
		set;
	}
	#endregion
}
