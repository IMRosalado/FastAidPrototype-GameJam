using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	public LayerMask targetLayer;
	public float percentage = 50f;

	private GameObject playerGameObject;
	private List<Node> pathPlayer= new List<Node> ();
	private Grid grid;

	private int []prev;


	void Awake() {
		grid = GameObject.Find("GameManager").GetComponent<Grid>();

		playerGameObject = GameObject.FindGameObjectWithTag ("Player");
	}		

	public void MovePlayer (GameObject source, GameObject destination, float sourceSpeed, int index){
		FindPath (source,destination, index);
		StartCoroutine (MovePlayerTransition (source, destination, sourceSpeed));
	}

	IEnumerator MovePlayerTransition(GameObject source, GameObject destination, float sourceSpeed){
		for (int i = 0; i < pathPlayer.Count; i++) {
			while (Vector3.Distance (source.transform.position, pathPlayer[i].worldPosition) > 0.001f) {
				source.transform.position = Vector3.MoveTowards(source.transform.position, pathPlayer[i].worldPosition, sourceSpeed*Time.deltaTime);
				yield return 0; 
			}
			if(i==pathPlayer.Count-1 && Physics.CheckSphere(source.transform.position, 0.5f, targetLayer)){
				Collider[] cols = Physics.OverlapSphere (source.transform.position, 0.5f, targetLayer);
				yield return 0;
				break;
			}
		}
		PlayerManager.instance.playerIsMoving = false;
	}

		


	void FindPath(GameObject startPos, GameObject targetPos, int index) {
		
		Node startNode = grid.NodeFromWorldPoint(startPos.transform.position);

		Node targetNode = grid.NodeFromWorldPoint(targetPos.transform.position);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if (currentNode == targetNode) {
				RetracePath(startNode,targetNode, startPos, index);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode, GameObject source,int index) {
		List<Node> path = new List<Node> ();
		List<Vector3> dir = new List<Vector3>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			grid.GetDirections(dir, currentNode.direction);
			currentNode = currentNode.parent;
		}
		path.Reverse();

	
			pathPlayer = path;
			grid.path = path;

	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
			
}
	