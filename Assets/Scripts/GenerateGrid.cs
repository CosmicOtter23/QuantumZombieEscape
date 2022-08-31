using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GenerateGrid : MonoBehaviour
{
    public int xSize, ySize;
    
    public Vector2[,] vertices;

    private Mesh mesh;

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.005f);

        //GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        //mesh.name = "Procedural Grid";

        //x no. of boxes so x+1 no of vertices
        //vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        vertices = new Vector2[(xSize) , (ySize)];

        Debug.Log("No of vertices: " + vertices.Length);

        //for (int i = 0, y = 0; y <= ySize; y++)
        //{
        //    for (int x = 0; x <= xSize; x++, i++)
        //    {
        //        //vertices[i] = new Vector3(x - 12.5f, y - 6.5f);
        //        vertices2[x, y] = new Vector3(x, y);
        //        yield return wait;
        //    }
        //}

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                vertices[x, y] = new Vector3(x - 12.5f, y - 6.5f);
                //yield return wait;
            }
        }

        //int[] triangles = new int[6];
        //triangles[0] = 0;
        //triangles[3] = triangles[2] = 1;
        //triangles[4] = triangles[2] = xSize + 1;
        //triangles[5] = xSize + 2;
        //mesh.triangles = triangles;

        yield return wait;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            Debug.Log("No vertices");
            return;
        }

        //Gizmos.color = Color.black;
        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Gizmos.DrawSphere(vertices[i], 0.1f);
        //}

        //Debug.Log("test");

        Gizmos.color = Color.black;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                //Debug.Log(x);

                Gizmos.DrawSphere(vertices[x, y], 0.1f);
            }
        }
    }
}
