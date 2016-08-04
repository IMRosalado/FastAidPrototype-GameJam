using UnityEngine;
using System.Collections;
using System;
public class Node {

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;

	public Vector3 direction;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		direction = new Vector3(0,0,0);
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}