using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {

	public Transform[] openList;
	private Transform[] closedList;

	private int squaresMoved;
	private int squaresLeft;
	private int squareCost;

	private int noNodes;

	// Use this for initialization
	void Start () {
		//noNodes = GetLength(openList);
		//openList.Add(gameObject.transform.position.x, gameObject.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		squareCost = squaresMoved + squaresLeft;
	}
}
