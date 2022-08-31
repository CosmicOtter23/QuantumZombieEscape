using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarPathfinding2 : MonoBehaviour
{
    private List<Vector2> openList = new List<Vector2>();
    private List<Vector2> closedList = new List<Vector2>();

    private Vector2 currentNode;
    private int currentX, currentY;

    private Vector2 A; //starting point
    private Vector2 Z; //target
    private GameObject player;

    private float F = 0, G = 0, H = 0;

    public GameObject gameManager;
    private Vector2[,] vertices;

    private Vector2 nextDestination;
    private int nextX = -1, nextY = -1;

    public float speed;

    private int xSize, ySize;

    int finalX;
    int finalY;

    //The x and y values of the nodes in the open list
    private List<int> xValues = new List<int>();
    private List<int> yValues = new List<int>();

    bool foundPlayer = false;

    private float countdown;

    public bool overlapsWithCollider;

    public float obstacleCheckRadius;
    public LayerMask whatIsObstacle;

    private bool findNextPoint = false;

    float smallestF;
    int smallestFIndex;

    int nextPoint = 0;

    public float refreshRate;

    private Vector2 playerNode;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.Log("Game manager not found");
        }
        else
        {
            Debug.Log("Game manager found");
        }

        vertices = gameManager.GetComponent<GenerateGrid>().vertices;

        if (gameManager == null)
        {
            Debug.Log("Vertices not found");
        }
        else
        {
            Debug.Log("Vertices found");
        }

        xSize = gameManager.GetComponent<GenerateGrid>().xSize;
        ySize = gameManager.GetComponent<GenerateGrid>().ySize;

        A = transform.position;
        Z = GameObject.Find("Player").transform.position;

        FindStartingNode();

        countdown = refreshRate;
    }

    public void RefreshPath()
    {
        nextPoint = 0;

        openList.Clear();
        closedList.Clear();
        xValues.Clear();
        yValues.Clear();

        currentX = 0;
        currentY = 0;

        FindStartingNode();

        foundPlayer = false;
    }

    void Update()
    {
        vertices = gameManager.GetComponent<GenerateGrid>().vertices;

        player = GameObject.Find("Player");

        Z = player.transform.position;

        if (nextPoint <= closedList.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedList[nextPoint], speed * Time.deltaTime);
        }

        if (new Vector2(transform.position.x, transform.position.y) == closedList[nextPoint])
        {
            nextPoint += 1;
        }

        if (foundPlayer)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            RefreshPath();
            countdown = refreshRate;
        }

        if (!foundPlayer)
        {
            GenerateNextPoints();
            CalculateFValues();
        }

        if (Vector2.Distance(vertices[currentX, currentY], player.transform.position) <= 1.5)
        {
            //Debug.Log("FOUND PLAYER");
            playerNode = vertices[currentX, currentY];
            foundPlayer = true;
            countdown = refreshRate;
        }

        if (nextPoint == closedList.Count)
        {
            RefreshPath();
            countdown = refreshRate;
        }
        
        if (player.transform.position.x >= transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (player.transform.position.x <= transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FindStartingNode()
    {
        int shortestX = 0, shortestY = 0;

        float shortestDistance = 1000f;

        float distance;

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                //Debug.Log("Current node: (" + x + "," + y + ")");

                Debug.Log(vertices[x, y]);

                distance = Mathf.Sqrt(Mathf.Pow(vertices[x, y].x - transform.position.x, 2f) + Mathf.Pow(vertices[x, y].y - transform.position.y, 2f));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;

                    shortestX = x;
                    shortestY = y;
                }
            }
        }

        //Debug.Log("Starting node: (" + shortestX + "," + shortestY + ")");

        A = vertices[shortestX, shortestY];

        currentNode = A;
        currentX = shortestX;
        currentY = shortestY;
        
        closedList.Add(A);
        
        findNextPoint = true;
    }

    private void GenerateNextPoints()
    {
        //openList.Clear();

        //The x and y values for the nodes up, left, right and down
        int[] finalXs = { currentX, currentX - 1, currentX + 1, currentX };
        int[] finalYs = { currentY + 1, currentY, currentY, currentY - 1 };

        for (int i = 0; i < 4; i++)
        {
            finalX = finalXs[i];
            finalY = finalYs[i];

            //overlapsWithCollider = Physics2D.OverlapCircle(vertices[finalX, finalY], obstacleCheckRadius, whatIsObstacle);

            // makes sure node is in the range of nodes, so it doesn't return an error
            if (finalX >= 0 && finalX <= gameManager.GetComponent<GenerateGrid>().xSize && finalY >= 0 && finalY <= gameManager.GetComponent<GenerateGrid>().ySize && !closedList.Contains(vertices[finalX, finalY]) && !Physics2D.OverlapCircle(vertices[finalX, finalY], obstacleCheckRadius, whatIsObstacle) && !openList.Contains(vertices[finalX, finalY]))
            {
                //Debug.Log("Adding node [" + finalX + ", " + finalY + "]");

                openList.Add(vertices[finalX, finalY]);
                //openList.Add(vertices[4, 4]);
                //adjacentNodes.Add(vertices[finalX, finalY]);

                xValues.Add(finalX);
                yValues.Add(finalY);
            }
            else
            {
                //Debug.Log("Node [" + finalX + ", " + finalY + "] not added");
            }
        }




        ////The 8 nodes surrounding the current node
        //for (int x = 0; x < 3; x++)
        //{
        //    for (int y = 2; y > -1; y--)
        //    {
        //        if (x == 1 && y == 1)
        //        {
        //            //Current Node
        //        }
        //        else
        //        {
        //            overlapsWithCollider = false;

        //            int xChange = x - 1;
        //            int yChange = y - 1;

        //            //Debug.Log("CurrentNode: " + currentX + ", " + currentY);

        //            finalX = currentX + xChange;
        //            finalY = currentY + yChange;

        //            overlapsWithCollider = Physics2D.OverlapCircle(vertices[finalX, finalY], obstacleCheckRadius, whatIsObstacle);

        //            if (overlapsWithCollider)
        //            {
        //                //Debug.Log("Overlaps");
        //            }
        //            else
        //            {
        //                //Debug.Log("Does not overlap");
        //            }
        //            if (closedList.Contains(vertices[finalX, finalY]))
        //            {
        //                Debug.Log("Node is in the closed list");
        //            }
        //            else
        //            {
        //                //Debug.Log("Not in closed list");
        //            }

        //            // makes sure node is in the range of nodes, so it doesn't return an error
        //            if (finalX > 0 && finalX <= gameManager.GetComponent<GenerateGrid>().xSize && finalY > 0 && finalY <= gameManager.GetComponent<GenerateGrid>().ySize && !closedList.Contains(vertices[finalX, finalY]) && !overlapsWithCollider && !openList.Contains(vertices[finalX, finalY]))
        //            {
        //                //Debug.Log("Adding node [" + finalX + ", " + finalY + "]");

        //                openList.Add(vertices[finalX, finalY]);
        //                adjacentNodes.Add(vertices[finalX, finalY]);

        //                xValues.Add(finalX);
        //                yValues.Add(finalY);
        //            }
        //            else
        //            {
        //                //Debug.Log("Node not added");
        //            }
        //        }
        //    }
        //}

        closedList.Add(vertices[currentX, currentY]);
    }

    private void CalculateFValues()
    {
        smallestF = 1000f;

        for (int i = 0; i < openList.Count; i++)
        {

            G = Vector2.Distance(openList[i], vertices[currentX, currentY]);

            //Debug.Log("G distance: " + G);

            H = Vector2.Distance(openList[i], Z);

            //Debug.Log("H distance: " + H);

            F = G + H;

            if (F < smallestF)
            {
                //Debug.Log("New smallest F");

                smallestF = F;
                smallestFIndex = i;
                //Debug.Log("SmallestF: " + smallestFIndex);

                //Debug.Log("Current: (" + currentX + ", " + currentY + ")");
            }
        }
        
        nextDestination = openList[smallestFIndex];
        closedList.Add(nextDestination);
        currentX = xValues[smallestFIndex];
        currentY = yValues[smallestFIndex];
        //Debug.Log("Current x: " + currentX + ", current y: " + currentY);

        openList.RemoveAt(smallestFIndex);
        xValues.RemoveAt(smallestFIndex);
        yValues.RemoveAt(smallestFIndex);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(nextDestination, 0.3f);

        for (int i = 0; i < openList.Count; i++)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawSphere(openList[i], 0.2f);
        }
        for (int i = 0; i < closedList.Count; i++)
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawSphere(closedList[i], 0.2f);
        }
    }
}