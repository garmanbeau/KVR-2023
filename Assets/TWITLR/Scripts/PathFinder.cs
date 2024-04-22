// 2024-01-26 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections.Generic;
using UnityEngine;


public class Pathfinder : MonoBehaviour
{
    private Vector2Int currentPosition = new Vector2Int(0, 0);
    private List<Vector2Int> path = new List<Vector2Int>();

    public GameObject pathPrefab;
    public GameObject nonPathPrefab;

    void Start()
    {
        path.Add(currentPosition);

        // 2024-01-27 AI-Tag 
        // This was created with assistance from Muse, a Unity Artificial Intelligence product

        int failSafeCounter = 0;

        while (currentPosition != new Vector2Int(11, 11))
        {
            Vector2Int direction = ChooseRandomDirection();

            if (IsValidMove(currentPosition + direction))
            {
                currentPosition += direction;
                path.Add(currentPosition);
                failSafeCounter = 0; // Reset counter when a valid move is made
            }
            else
            {
                failSafeCounter++; // Increment counter when no valid move can be made

                // If no valid move is found after a certain number of tries, break the loop
                if (failSafeCounter > 1000)
                {
                    Debug.Log("Failed to find a path after 1000 attempts. Breaking loop.");
                    break;
                }
            }
        }


        // 2024-01-27 AI-Tag 
        // This was created with assistance from Muse, a Unity Artificial Intelligence product

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Vector2Int gridPosition = new Vector2Int(i, j);
                GameObject prefabToInstantiate = path.Contains(gridPosition) ? pathPrefab : nonPathPrefab;

                Instantiate(
                    prefabToInstantiate,
                    new Vector3(gridPosition.x, 0, gridPosition.y), // Change here
                    Quaternion.identity
                );
            }
        }

    }

    Vector2Int ChooseRandomDirection()
    {
        Vector2Int[] directions = new Vector2Int[4] {
        new Vector2Int(0, 1),  // Up
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0), // Left
        new Vector2Int(1, 0)   // Right
    };

        int randomIndex = UnityEngine.Random.Range(0, directions.Length);
        return directions[randomIndex];
    }
    // 2024-01-26 AI-Tag 
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    bool IsValidMove(Vector2Int newPosition)
    {
        // Check if newPosition is within grid bounds
        if (newPosition.x < 0 || newPosition.x > 11 || newPosition.y < 0 || newPosition.y > 11)
        {
            return false;
        }

        // Check if newPosition is not already in the path
        if (path.Contains(newPosition))
        {
            return false;
        }

        return true;
    }


    public void RebuildGrid()
    {
        // Clear out the existing path
        path.Clear();

        // Destroy all existing grid GameObjects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Reset the current position to the start of the grid
        currentPosition = new Vector2Int(0, 0);
        path.Add(currentPosition);

        // Rebuild the path
        BuildPath();

        // Rebuild the grid
        BuildGrid();
    }

    void BuildPath()
    {
        while (currentPosition != new Vector2Int(11, 11))
        {
            Vector2Int direction = ChooseRandomDirection();

            if (IsValidMove(currentPosition + direction))
            {
                currentPosition += direction;
                path.Add(currentPosition);
            }
        }
    }

    void BuildGrid()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Vector2Int gridPosition = new Vector2Int(i, j);
                GameObject prefabToInstantiate = path.Contains(gridPosition) ? pathPrefab : nonPathPrefab;

                Instantiate(
                    prefabToInstantiate,
                    new Vector3(gridPosition.x, 0, gridPosition.y),
                    Quaternion.identity,
                    transform // Make the new GameObject a child of the current GameObject
                );
            }
        }
    }



}
