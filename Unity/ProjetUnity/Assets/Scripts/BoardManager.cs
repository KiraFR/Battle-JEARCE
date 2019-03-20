using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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




    public void VisuDeplacement(int posX, int posY, int mouvement)
    {
        if (GetGameObject((posX+1),posY)!=null)
        {
            GameObject objet = GetGameObject((posX+1), posY);
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                // if (Enemy !=){
                VisuDeplacement(posX + 1, posY, mouvement - 1);
                //}
            }
        }

        if (GetGameObject((posX-1),posY) != null)
        {
            GameObject objet = GetGameObject((posX-1), posY);
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                // if (Enemy !=){
                VisuDeplacement(posX - 1, posY, mouvement - 1);
                //}
            }
        }

        if (GetGameObject(posX,(posY+1)) != null)
        {
            GameObject objet = GetGameObject(posX,(posY+1));
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                // if (Enemy !=){
                VisuDeplacement(posX, posY+1, mouvement - 1);
                //}
            }
        }

        if (GetGameObject(posX,(posY-1)) != null)
        {
            GameObject objet = GetGameObject(posX, (posY-1));
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                // if (Enemy !=){
                VisuDeplacement(posX, posY-1, mouvement - 1);
                //}
            }
        }
    }
}
