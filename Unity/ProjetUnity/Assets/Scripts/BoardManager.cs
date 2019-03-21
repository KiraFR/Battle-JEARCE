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
    private List<GameObject> obstacles = new List<GameObject>();    //Global list so we can use it in the isBlocked() method


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

    //Method that will first add obstacles randomly before checking is the board is blocked
    //If so then it'll call the method isBlocked() that will remove one obstacle to set the board playable
    void ObstaclesSetup()   
    {

        obstacles.Clear();

        for (int i = 0; i < nbObstacles; i++)
        {
            int x = UnityEngine.Random.Range(0, columns);   //Choosing a random x
            int y = UnityEngine.Random.Range(2, rows - 2);  //Choosing a random y letting some space for the spawn of characters

            Vector3 randomPosition = new Vector3(x, y, 0f);

            GameObject obstacle = GetGameObject((int)randomPosition.x, (int)randomPosition.y);
            obstacles.Add(obstacle);    //Adding our obstacle in the global obstacles list

            if(obstacle != null)
            {
                obstacle.GetComponent<SpriteRenderer>().sprite = obstacle.GetComponent<Square>().inaccessibleSprite;               
            }

           
            if((int)obstacle.transform.position.x == 1)  //Condition to verify to start the isBlocked() method
            {
                int increment = 1;  //increment that will count the number of obstacles that can block the board (blocked if increment = columns)
                isBlocked(obstacle, increment);
            }

        }
    }


    //Recursive method that will check is there is a line of obstacle blocking the way
    //If so the last blocking obstacle will be removed
    void isBlocked(GameObject obstacle, int increment) 
    {

        int xObs = (int)obstacle.transform.position.x;
        int yObs = (int)obstacle.transform.position.y;

        if (increment == columns) //Is the game is bocked the last blocking obstacle will be removed and replaced by a normal floor
        {
            obstacle.GetComponent<SpriteRenderer>().sprite = obstacle.GetComponent<Square>().baseSprite; 
        }

        else 
        {
            for(int i = 0; i < obstacles.Count; i++)
            {

                int xObsi = (int)obstacles[i].transform.position.x;
                int yObsi = (int)obstacles[i].transform.position.y;

                //If there is another potential blocking obstacle then
                if (xObsi == xObs + 1 && (yObsi == yObs -1 || yObsi == yObs || yObsi == yObs + 1))
                
                {
                    increment++;
                    isBlocked(obstacles[i], increment);
                }
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
