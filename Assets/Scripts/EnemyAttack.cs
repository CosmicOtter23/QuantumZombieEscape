using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;

    private Vector2 playerPosition, enemyPosition, distanceVect;

    private float distanceToPlayer;

    private Animator anim;

    void Start()
    {
        //player = transform.Find("Player");
        player = GameObject.Find("Player").transform;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        enemyPosition = transform.position;
        playerPosition = player.position;

        distanceVect = playerPosition - enemyPosition;

        distanceToPlayer = Mathf.Sqrt(distanceVect.x * distanceVect.x + distanceVect.y * distanceVect.y);

        anim.SetFloat("Distance", distanceToPlayer);

        //Debug.Log(distanceToPlayer);
    }
}
