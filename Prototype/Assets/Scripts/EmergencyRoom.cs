using UnityEngine;
using System.Collections;

public class EmergencyRoom : MonoBehaviour {

	public bool isOccupied;
	public SpriteRenderer rend;
	public Sprite emptySprite;
	public Sprite occupiedSprite;

	void Update(){
		if (isOccupied) {
			rend.sprite = occupiedSprite;
		} else {
			rend.sprite = emptySprite;
		}

	}

	void Occupy(){
		isOccupied = true;

	}
	void Empty(){
		isOccupied = false;
	}
}
