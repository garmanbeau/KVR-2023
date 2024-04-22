using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject pathPrefab;
    public GameObject wallPrefab;

    private int width = 12;
    private int height = 12;
    private bool[,] isPath;

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        isPath = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                isPath[x, z] = false; // Initialize all cells as walls
            }
        }

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int current = new Vector2Int(0, 0);
        isPath[current.x, current.y] = true;
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Peek();
            var neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                Vector2Int chosen = neighbors[Random.Range(0, neighbors.Count)];
                // Remove the wall between the current cell and the chosen cell
                Vector2Int wall = current + (chosen - current) / 2;
                isPath[wall.x, wall.y] = true;

                chosen = current + 2 * (chosen - current);
                stack.Push(chosen);
                isPath[chosen.x, chosen.y] = true;
            }
            else
            {
                stack.Pop();
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Vector2Int[] directions = { new Vector2Int(0, 2), new Vector2Int(0, -2), new Vector2Int(2, 0), new Vector2Int(-2, 0) };
        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighbor = cell + dir;
            if (neighbor.x >= 0 && neighbor.y >= 0 && neighbor.x < width && neighbor.y < height && !isPath[neighbor.x, neighbor.y])
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    void BuildMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject prefabToUse = isPath[x, z] ? pathPrefab : wallPrefab;
                Instantiate(prefabToUse, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
}
