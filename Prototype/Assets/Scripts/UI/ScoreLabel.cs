﻿using UnityEngine;
using System.Collections;

public class ScoreLabel : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel>().text = "Score: " + PlayerPrefs.GetInt("SCORE");
	}
}
