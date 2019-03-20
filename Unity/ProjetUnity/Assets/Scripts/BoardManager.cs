using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoardManager : MonoBehaviour
{
    public int columns = 8;
    public int rows = 8;
    public int nbObstacles;
    public GameObject floorTiles;

    // Every Floor's gameObject from Vector3
    public Dictionary<Vector3,GameObject> floorGameObjects = new Dictionary<Vector3, GameObject>();

    private List<Vector3> gridPositions = new List<Vector3>();
    private Transform boardHolder;
    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
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
                Vector3 pos = new Vector3(x, y, 0f);
                GameObject instance = Instantiate(floorTiles, pos, Quaternion.identity) as GameObject;
                floorGameObjects.Add(pos,instance);
                // TODELETE --> DEBUG CHARACTER
                if (x == 0 && y == 0)
                {
                    instance.GetComponent<Square>().SetCharacter(GameObject.Find("epeiste").GetComponent<Character>());
                }

                
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    void ObstaclesSetup()
    {
        for (int i = 0; i < nbObstacles; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            GameObject floor = GetGameObject((int)randomPosition.x, (int)randomPosition.y);
            if(floor != null)
            {
                floor.GetComponent<SpriteRenderer>().sprite = floor.GetComponent<Square>().inaccessibleSprite;               
            }
        }
    }


    public void SetupScene()
    {
        BoardSetup();
        InitialiseList();
        ObstaclesSetup();
    }

    public Transform getBoard()
    {
        return boardHolder;
    }

    public GameObject GetGameObject(int xDir, int yDir)
    {
        Vector3 pos = new Vector3(xDir, yDir);
        if (floorGameObjects.ContainsKey(pos))
        {
            return floorGameObjects[pos];
        }
        return null;
    }
}
