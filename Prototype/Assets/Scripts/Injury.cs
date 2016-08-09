using UnityEngine;
using System.Collections;

public class Injury {
	public int type;
	public int cured;

	public Injury(int _type, int _cured){
		this.type = _type;
		this.cured = _cured;
	}

	public int Cured {
		get;
		set;
	}
}
