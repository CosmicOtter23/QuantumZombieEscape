using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;

	public Transform[] nodes;
	private Transform closestNode;
	private Vector2 enemyPosition;
	private Vector2 distance;
	private Vector2 newDistance;
	public Transform startNode;
	public Transform endNode;

	private Rigidbody2D theRB;

	public Transform groundCheckPoint;

	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator anim;


	// Use this for initialization
	void Start () {
		theRB = GetComponent<Rigidbody2D>();

//		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
//		anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
//		anim.SetBool("Grounded", isGrounded);

		enemyPosition = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);

//		foreach (int i in nodes) 
//		{
//			distance = enemyPosition - new Vector2 (nodes[i].transform.position.x, nodes[i].transform.position.y);
//			closestNode = nodes [i];
//		}

		foreach (Transform item in nodes) 
		{
			newDistance = enemyPosition - new Vector2 (item.transform.position.x, item.transform.position.y);
			//if (float(newDistance - distance) >= 0) 
			//{
			//	closestNode = item;
			//}
		}

		transform.position = Vector3.MoveTowards (transform.position, closestNode.transform.position, moveSpeed * Time.deltaTime);

		isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

		if (theRB.velocity.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (theRB.velocity.x > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	public void MoveToPlayer()
	{
//		for (int i = 0; i <= 6; i++)
//		{
//			distance = enemyPosition - new Vector2 (nodes[i].transform.position.x, nodes[i].transform.position.y);
//			closestNode = nodes [i];
//		}

	}
}
