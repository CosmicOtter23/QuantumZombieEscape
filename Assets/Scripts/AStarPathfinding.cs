//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AStarPathfinding : MonoBehaviour
//{
//    private List<Vector2> openList = new List<Vector2>();
//    private List<Vector2> closedList = new List<Vector2>();

//    private Vector2 currentNode;
//    private int currentX, currentY;

//    private Vector2 A; //starting point
//    private Vector2 Z; //target

//    private Vector2 topLeft, topMid, topRight, midLeft, midRight, lowLeft, lowMid, lowRight;

//    private float F = 0, G = 0, H = 0;

//    public GameObject gameManager;
//    private Vector2[,] vertices;

//    private Vector2 nextDestination;
//    private int nextX, nextY;

//    public float speed;

//    private int xSize, ySize;

//    int finalX;
//    int finalY;

//    //The x and y values of the nodes in the open list
//    private List<int> xValues = new List<int>();
//    private List<int> yValues = new List<int>();

//    private void Awake()
//    {
//        gameManager = GameObject.Find("GameManager");

//        vertices = gameManager.GetComponent<GenerateGrid>().vertices;

//        xSize = gameManager.GetComponent<GenerateGrid>().xSize;
//        ySize = gameManager.GetComponent<GenerateGrid>().ySize;

//        A = transform.position;
//        Z = GameObject.Find("Player").transform.position;

//        FindStartingNode();
        
//        GenerateNextPoints(currentNode, currentX, currentY);

//        CalculateFValues();

//        //GenerateNextPoints();

//        //for (int i = 0; i < openList.Count; i++)
//        //{
//        //    Debug.Log(openList[i]);
//        //}

//        //nextDestination = A;

//        //transform.position = currentNode;

//        //openList.Add(A);

//        //GenerateNextPoints();
//    }

//    private void Update()
//    {
//        vertices = gameManager.GetComponent<GenerateGrid>().vertices;
//        //Debug.Log(vertices[5, 5].x + ", " + vertices[5, 5].y);

//        Z = GameObject.Find("Player").transform.position;

//        transform.position = Vector3.MoveTowards(transform.position, nextDestination, speed * Time.deltaTime);

//        //GenerateNextPoints();

//        //CalculateFValues();

//        currentX = nextX;
//        currentY = nextY;

//        currentNode = nextDestination;

//        openList.Clear();

//        GenerateNextPoints(currentNode, currentX, currentY);

//        CalculateFValues();

//        //if (nextDestination == new Vector2(transform.position.x, transform.position.y))
//        //{
//        //    currentX = nextX;
//        //    currentY = nextY;

//        //    currentNode = nextDestination;

//        //    openList.Clear();

//        //    GenerateNextPoints(currentNode, currentX, currentY);

//        //    CalculateFValues();
//        //}
//    }

//    private void GenerateNextPoints(Vector2 centreNode, int centreX, int centreY)
//    {
//        openList.Clear();

//        for (int x = 0; x < 3; x++)
//        {
//            for (int y = 2; y > -1; y--)
//            {
//                if (x == 1 && y == 1)
//                {
//                    //Current Node
//                }
//                else
//                {
//                    int xChange = x - 1;
//                    int yChange = y - 1;

//                    //Debug.Log("yChange: " + yChange);

//                    finalX = centreX + xChange;
//                    finalY = centreY + yChange;

//                    // makes sure node is in the range of nodes, so it doesn't return an error
//                    if (finalX >= 0 && finalX <= gameManager.GetComponent<GenerateGrid>().xSize && finalY >= 0 && finalY <= gameManager.GetComponent<GenerateGrid>().ySize/* && closedList.Contains(vertices[finalX, finalY]) == false*/)
//                    {
//                        //Debug.Log("Adding node [" + finalX + ", " + finalY + "]");

//                        openList.Add(vertices[finalX, finalY]);

//                        xValues.Add(finalX);
//                        yValues.Add(finalY);

//                        //Debug.Log("Open list: " + openList.Count);
//                        //Debug.Log("XList: " + xValues.Count);
//                        //Debug.Log("Ylist: " + yValues.Count);
//                    }
//                }
//            }
//        }
//    }

//    private void CalculateFValues()
//    {
//        //Debug.Log("length: " + openList.Count);

//        float smallestF = 1000f;
//        int smallestFIndex = 1000;

//        for (int i = 0; i < openList.Count; i++)
//        {
//            G = Mathf.Sqrt(Mathf.Pow(openList[i].x - currentX, 2f) + Mathf.Pow(openList[i].y - currentY, 2f));

//            //Debug.Log("G distance: " + G);

//            H = Mathf.Sqrt(Mathf.Pow(openList[i].x - Z.x, 2f) + Mathf.Pow(openList[i].y - Z.y, 2f));

//            //Debug.Log("H distance: " + H);

//            F = G + H;

//            if (F < smallestF)
//            {
//                smallestF = F;
//                smallestFIndex = i;
//                Debug.Log("SmallestF: " + smallestFIndex);

//                Debug.Log("Current: (" + currentX + ", " + currentY + ")");
//            }
//        }

//        //Debug.Log("Next node index: " + smallestFIndex);

//        //openList.Remove(nextDestination);

//        nextDestination = openList[smallestFIndex];
//        nextX = xValues[smallestFIndex];
//        nextY = yValues[smallestFIndex];

//        //openList.Remove(nextDestination);
//        //closedList.Add(nextDestination);
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;

//        Gizmos.DrawSphere(nextDestination, 0.3f);
        
//        for (int i = 0; i < openList.Count; i++)
//        {
//            Gizmos.color = Color.blue;

//            Gizmos.DrawSphere(openList[i], 0.2f);
//        }
//    }

//    private void FindStartingNode()
//    {
//        //Debug.Log("FindStartingNode");

//        int shortestX = 0, shortestY = 0;

//        //Debug.Log("ySize: " + ySize);

//        float shortestDistance = 1000f;

//        float distance;

//        for (int y = 0; y < ySize; y++)
//        {
//            for (int x = 0; x < xSize; x++)
//            {
//                //Debug.Log("Current node: (" + x + "," + y + ")");

//                distance = Mathf.Sqrt(Mathf.Pow(vertices[x, y].x - transform.position.x, 2f) + Mathf.Pow(vertices[x, y].y - transform.position.y, 2f));

//                //distance = Vector2.Distance(transform.position, vertices[x, y]);

//                //Debug.Log("distance: " + distance);
//                //Debug.Log("shortestDistance: " + shortestDistance);

//                if (distance < shortestDistance)
//                {
//                    shortestDistance = distance;

//                    shortestX = x;
//                    shortestY = y;

//                    //Debug.Log("New closest node: (" + x + "," + y + ")");
//                }
//            }
//        }

//        Debug.Log("Starting node: (" + shortestX + "," + shortestY + ")");

//        A = vertices[shortestX, shortestY];

//        //closedList.Add(A);

//        currentNode = A;
//        currentX = shortestX;
//        currentY = shortestY;

//        //A = vertices[1, 2];
//        //nextDestination = A;
//    }
//}

