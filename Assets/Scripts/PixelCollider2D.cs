using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class PixelCollider2D : MonoBehaviour
{
    //private void Update()
    //{
    //    Regenerate();
    //}

    [Range(0, 1)]
    public float alphaCutoff = 0.5f;
    public void Regenerate()
    {
        //Debug.Log("Regenerate");
        alphaCutoff = Mathf.Clamp(alphaCutoff, 0, 1);
        PolygonCollider2D PolygonCollider = GetComponent<PolygonCollider2D>();
        if (PolygonCollider == null)
        {
            throw new Exception($"PixelCollider2D could not be regenerated because there is no PolygonCollider2D component on \"{gameObject.name}\".");
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            PolygonCollider.pathCount = 0;
            throw new Exception($"PixelCollider2D could not be regenerated because there is no SpriteRenderer component on \"{gameObject.name}\".");
        }
        if (spriteRenderer.sprite == null)
        {
            PolygonCollider.pathCount = 0;
            return;
        }
        if (spriteRenderer.sprite.texture == null)
        {
            PolygonCollider.pathCount = 0;
            return;
        }
        if (spriteRenderer.sprite.texture.isReadable == false)
        {
            PolygonCollider.pathCount = 0;
            throw new Exception($"PixelCollider2D could not be regenerated because on \"{gameObject.name}\" because the sprite does not allow read/write operations.");
        }
        List<List<Vector2Int>> pixelPaths = new List<List<Vector2Int>>();
        pixelPaths = GetUnitPaths(spriteRenderer.sprite.texture, alphaCutoff);
        pixelPaths = SimplifyPathsPhase1(pixelPaths);
        pixelPaths = SimplifyPathsPhase2(pixelPaths);
        List<List<Vector2>> worldPaths = new List<List<Vector2>>();
        worldPaths = FinalisePaths(pixelPaths, spriteRenderer.sprite);
        PolygonCollider.pathCount = worldPaths.Count;
        for (int p = 0; p < worldPaths.Count; p++)
        {
            PolygonCollider.SetPath(p, worldPaths[p].ToArray());
        }
    }
    private List<List<Vector2>> FinalisePaths(List<List<Vector2Int>> pixelPaths, Sprite sprite)
    {
        Vector2 pivot = sprite.pivot;
        pivot.x *= Mathf.Abs(sprite.bounds.max.x - sprite.bounds.min.x);
        pivot.x /= sprite.texture.width;
        pivot.y *= Mathf.Abs(sprite.bounds.max.y - sprite.bounds.min.y);
        pivot.y /= sprite.texture.height;

        List<List<Vector2>> Output = new List<List<Vector2>>();
        for (int p = 0; p < pixelPaths.Count; p++)
        {
            List<Vector2> currentList = new List<Vector2>();
            for (int o = 0; o < pixelPaths[p].Count; o++)
            {
                Vector2 point = pixelPaths[p][o];
                point.x *= Mathf.Abs(sprite.bounds.max.x - sprite.bounds.min.x);
                point.x /= sprite.texture.width;
                point.y *= Mathf.Abs(sprite.bounds.max.y - sprite.bounds.min.y);
                point.y /= sprite.texture.height;
                point -= pivot;
                currentList.Add(point);
            }
            Output.Add(currentList);
        }
        return Output;
    }
    private static List<List<Vector2Int>> SimplifyPathsPhase1(List<List<Vector2Int>> unitPaths)
    {
        List<List<Vector2Int>> Output = new List<List<Vector2Int>>();
        while (unitPaths.Count > 0)
        {
            List<Vector2Int> currentPath = new List<Vector2Int>(unitPaths[0]);
            unitPaths.RemoveAt(0);
            bool loop = true;
            while (loop)
            {
                loop = false;
                for (int p = 0; p < unitPaths.Count; p++)
                {
                    if (currentPath[currentPath.Count - 1] == unitPaths[p][0])
                    {
                        loop = true;
                        currentPath.RemoveAt(currentPath.Count - 1);
                        currentPath.AddRange(unitPaths[p]);
                        unitPaths.RemoveAt(p);
                        p--;
                    }
                    else if (currentPath[0] == unitPaths[p][unitPaths[p].Count - 1])
                    {
                        loop = true;
                        currentPath.RemoveAt(0);
                        currentPath.InsertRange(0, unitPaths[p]);
                        unitPaths.RemoveAt(p);
                        p--;
                    }
                    else
                    {
                        List<Vector2Int> flippedPath = new List<Vector2Int>(unitPaths[p]);
                        flippedPath.Reverse();
                        if (currentPath[currentPath.Count - 1] == flippedPath[0])
                        {
                            loop = true;
                            currentPath.RemoveAt(currentPath.Count - 1);
                            currentPath.AddRange(flippedPath);
                            unitPaths.RemoveAt(p);
                            p--;
                        }
                        else if (currentPath[0] == flippedPath[flippedPath.Count - 1])
                        {
                            loop = true;
                            currentPath.RemoveAt(0);
                            currentPath.InsertRange(0, flippedPath);
                            unitPaths.RemoveAt(p);
                            p--;
                        }
                    }
                }
            }
            Output.Add(currentPath);
        }
        return Output;
    }
    private static List<List<Vector2Int>> SimplifyPathsPhase2(List<List<Vector2Int>> inputPaths)
    {
        for (int i = 0; i < inputPaths.Count; i++)
        {
            for (int j = 0; j < inputPaths[i].Count; j++)
            {
                Vector2Int start = new Vector2Int();
                if (j == 0)
                {
                    start = inputPaths[i][inputPaths[i].Count - 1];
                }
                else
                {
                    start = inputPaths[i][j - 1];
                }
                Vector2Int end = new Vector2Int();
                if (j == inputPaths[i].Count - 1)
                {
                    end = inputPaths[i][0];
                }
                else
                {
                    end = inputPaths[i][j + 1];
                }
                Vector2Int currentPoint = inputPaths[i][j];
                Vector2 direction = currentPoint - (Vector2)start;
                direction /= direction.magnitude;
                Vector2 direction2 = end - (Vector2)start;
                direction2 /= direction2.magnitude;
                if (direction == direction2)
                {
                    inputPaths[i].RemoveAt(j);
                    j--;
                }
            }
        }
        return inputPaths;
    }
    private static List<List<Vector2Int>> GetUnitPaths(Texture2D texture, float alphaCutoff)
    {
        List<List<Vector2Int>> Output = new List<List<Vector2Int>>();
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (pixelSolid(texture, new Vector2Int(x, y), alphaCutoff))
                {
                    if (!pixelSolid(texture, new Vector2Int(x, y + 1), alphaCutoff))
                    {
                        Output.Add(new List<Vector2Int>() { new Vector2Int(x, y + 1), new Vector2Int(x + 1, y + 1) });
                    }
                    if (!pixelSolid(texture, new Vector2Int(x, y - 1), alphaCutoff))
                    {
                        Output.Add(new List<Vector2Int>() { new Vector2Int(x, y), new Vector2Int(x + 1, y) });
                    }
                    if (!pixelSolid(texture, new Vector2Int(x + 1, y), alphaCutoff))
                    {
                        Output.Add(new List<Vector2Int>() { new Vector2Int(x + 1, y), new Vector2Int(x + 1, y + 1) });
                    }
                    if (!pixelSolid(texture, new Vector2Int(x - 1, y), alphaCutoff))
                    {
                        Output.Add(new List<Vector2Int>() { new Vector2Int(x, y), new Vector2Int(x, y + 1) });
                    }
                }
            }
        }
        return Output;
    }
    private static bool pixelSolid(Texture2D texture, Vector2Int point, float alphaCutoff)
    {
        if (point.x < 0 || point.y < 0 || point.x >= texture.width || point.y >= texture.height)
        {
            return false;
        }
        float pixelAlpha = texture.GetPixel(point.x, point.y).a;
        if (alphaCutoff == 0)
        {
            if (pixelAlpha != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (alphaCutoff == 1)
        {
            if (pixelAlpha == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return pixelAlpha >= alphaCutoff;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PixelCollider2D))]
public class PixelColider2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PixelCollider2D PixelCollider = (PixelCollider2D)target;
        if (GUILayout.Button("Regenerate Collider"))
        {
            PixelCollider.Regenerate();
        }
    }
}
#endif