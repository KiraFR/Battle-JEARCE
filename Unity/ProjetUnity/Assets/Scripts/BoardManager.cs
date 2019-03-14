using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int columns = 8;
    public int rows = 8;
    public GameObject floorTiles;

    private List<Vector3> gridPositions = new List<Vector3>();
    private List<GameObject> floorGameObjects = new List<GameObject>();
    private Transform boardHolder;
    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(floorTiles, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                floorGameObjects.Add(instance);
                instance.transform.SetParent(boardHolder);
            }
        }
    }
    public void SetupScene()
    {
        BoardSetup();
        InitialiseList();
    }


    public GameObject GetGameObject(int xDir, int yDir)
    {
        for (int i = 0; i < floorGameObjects.Count; i++)
        {
            GameObject obj = floorGameObjects[i];
            if ((int)obj.transform.position.x == xDir && (int)obj.transform.position.y == yDir)
            {
                return obj;
            }
        }
        return null;
    }
}
