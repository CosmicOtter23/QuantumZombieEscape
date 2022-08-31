using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public Transform start, end;

    private GameObject gameManager;

    private bool playerHit;

    private Vector2 debugHit;

    public LayerMask player;

    public float timeBeforeWarning, warningTime;
    private float countdown;
    private bool warning = false;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.Log("Game Manager not found");
        }
        playerHit = false;

        countdown = timeBeforeWarning;
    }
    
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !warning)
        {
            Debug.Log("Warning");
            warning = true;
            countdown = warningTime;
            _lineRenderer.SetPosition(0, new Vector2(start.position.x - 0.06f, start.position.y - 1));
            _lineRenderer.SetPosition(1, new Vector2(end.position.x - 0.06f, end.position.y + 1));
        }
        if (countdown <= 0 && warning)
        {
            Debug.Log("Fire");
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
            _lineRenderer.startWidth = 0.4f;
            _lineRenderer.endWidth = 0.4f;
            if (Physics2D.Raycast(start.position, transform.up, 1000f, player) && !gameManager.GetComponent<PlayerLives>().invulnerable)
            {
                //playerHit = true;
                Debug.Log("Player hit");
                gameManager.GetComponent<PlayerLives>().LoseLife();
            }
        }

        //Debug.DrawRay(start.position, end.position, Color.blue);
        Debug.DrawRay(start.position, transform.up, Color.blue);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(start.position, 0.3f);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(end.position, 0.3f);
    //}
}
