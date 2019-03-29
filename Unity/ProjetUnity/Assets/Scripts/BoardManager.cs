using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour
{

    public int columns = 8;
    public int rows = 8;
    public int nbObstacles;
    public GameObject floorTiles;
    private List<GameObject> units;

    // Every Floor's gameObject from Vector3
    private Dictionary<Vector3,GameObject> floorGameObjects = new Dictionary<Vector3, GameObject>();

    private Dictionary<Vector3, GameObject> enemies = new Dictionary<Vector3, GameObject>();
    private Dictionary<Vector3, GameObject> allies = new Dictionary<Vector3, GameObject>();

    private List<Vector3> gridPositions = new List<Vector3>();
    private Transform boardHolder;
    private List<GameObject> obstacles = new List<GameObject>();    //Global list so we can use it in the isBlocked() method


    void Awake()
    {
        units = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs"));
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
                // TODELETE --> DEBUG ALLY CHARACTER
                if (x == 0 && y == 0)
                {

                    GameObject ally = Instantiate(GetUnit("epeiste"), pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<Square>().SetCharacter(ally.GetComponent<Character>());
                    ally.GetComponent<Character>().SetState(true);
                    GameManager.instance.AddToAllies(ally);

                }

                // TODELETE --> DEBUG ENNEMY CHARACTER
                if (x == 0 && y == 1)
                {
                    GameObject enemy = Instantiate(GetUnit("epeiste"), pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<Square>().SetCharacter(enemy.GetComponent<Character>());
                    enemy.GetComponent<Character>().SetState(false);
                    GameManager.instance.AddToEnemies(enemy);
                }

                instance.transform.SetParent(boardHolder);
            }
        }
    }


    GameObject GetUnit(string name)
    {
        foreach(GameObject unit in units)
        {
            //Debug.Log(unit.name);
            if (unit.name.Equals(name))
            {
                return unit;
            }
        }
        return null;
    }

    //Method that will first add obstacles randomly before checking is the board is blocked
    //If so then it'll call the method isBlocked() that will remove one obstacle to set the board playable
    void RandomObstaclesSetup()
    {

        obstacles.Clear();

        for (int i = 0; i < nbObstacles; i++)
        {
            int x = Random.Range(0, columns);   //Choosing a random x
            int y = Random.Range(2, rows - 2);  //Choosing a random y letting some space for the spawn of characters

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

    /*
    void SpawnEnemies()
    {
        enemies.Add(new Vector3(2f, 7f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        enemies.Add(new Vector3(2f, 6f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        enemies.Add(new Vector3(3f, 7f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        enemies.Add(new Vector3(3f, 6f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        foreach (KeyValuePair<Vector3, GameObject> unit in enemies)
        {
            GameObject enemy = Instantiate(unit.Value, unit.Key, Quaternion.identity) as GameObject;
            GetGameObject((int)unit.Key.x, (int)unit.Key.y).GetComponent<Square>().SetCharacter(enemy.GetComponent<Character>());
            enemy.GetComponent<Character>().SetState(false);
            GameManager.instance.AddToEnemies(enemy);
        }
    }


    void SpawnAllies()
    {
        allies.Add(new Vector3(2f, 1f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        allies.Add(new Vector3(2f, 0f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        allies.Add(new Vector3(3f, 1f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        allies.Add(new Vector3(3f, 0f, 0f), Units[Random.Range(0, Units.Count - 1)]);
        foreach (KeyValuePair<Vector3, GameObject> unit in allies)
        {
            GameObject ally = Instantiate(unit.Value, unit.Key, Quaternion.identity) as GameObject;
            GameObject square = GetGameObject((int)unit.Key.x, (int)unit.Key.y);
            Debug.Log("(" + square.transform.position.x + "," + square.transform.position.y + "), " + ally);
            square.GetComponent<Square>().SetCharacter(ally.GetComponent<Character>());
            ally.GetComponent<Character>().SetState(true);
            GameManager.instance.AddToAllies(ally);
        }
    }
    */

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


    // SETUP OF 4 BOARDS WITH 8 OBSTACLES PLACED SYMMETRICALLY

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup1()
    {
        obstacles.Clear();

        obstacles.Add(GetGameObject(0, 2));
        obstacles.Add(GetGameObject(1, 3));
        obstacles.Add(GetGameObject(2, 2));
        obstacles.Add(GetGameObject(3, 2));

        obstacles.Add(GetGameObject(columns - 1, rows - 3));
        obstacles.Add(GetGameObject(columns - 2, rows - 4));
        obstacles.Add(GetGameObject(columns - 3, rows - 3));
        obstacles.Add(GetGameObject(columns - 4, rows - 3));

        PlaceObstacles(obstacles);
    }

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup2()
    {
        obstacles.Clear();

        obstacles.Add(GetGameObject(0, 2));
        obstacles.Add(GetGameObject(1, 3));
        obstacles.Add(GetGameObject(1, 4));
        obstacles.Add(GetGameObject(2, 5));

        obstacles.Add(GetGameObject(columns - 1, rows - 3));
        obstacles.Add(GetGameObject(columns - 2, rows - 4));
        obstacles.Add(GetGameObject(columns - 2, rows - 5));
        obstacles.Add(GetGameObject(columns - 3, rows - 6));

        PlaceObstacles(obstacles);
    }

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup3()
    {
        obstacles.Clear();

        obstacles.Add(GetGameObject(0, 5));
        obstacles.Add(GetGameObject(1, 2));
        obstacles.Add(GetGameObject(2, 3));
        obstacles.Add(GetGameObject(2, 4));

        obstacles.Add(GetGameObject(columns - 1, rows - 6));
        obstacles.Add(GetGameObject(columns - 2, rows - 3));
        obstacles.Add(GetGameObject(columns - 3, rows - 4));
        obstacles.Add(GetGameObject(columns - 3, rows - 5));

        PlaceObstacles(obstacles);
    }

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup4()
    {
        obstacles.Clear();

        obstacles.Add(GetGameObject(1, 3));
        obstacles.Add(GetGameObject(1, 4));
        obstacles.Add(GetGameObject(2, 4));
        obstacles.Add(GetGameObject(2, 5));

        obstacles.Add(GetGameObject(columns - 2, rows - 4));
        obstacles.Add(GetGameObject(columns - 2, rows - 5));
        obstacles.Add(GetGameObject(columns - 3, rows - 5));
        obstacles.Add(GetGameObject(columns - 3, rows - 6));

        PlaceObstacles(obstacles);
    }

    //Predefined obstacles are now replacing basic floors
    public void PlaceObstacles(List<GameObject> obtacles)
    {
        for(int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].GetComponent<SpriteRenderer>().sprite = obstacles[i].GetComponent<Square>().inaccessibleSprite;
        }
    }

    void ChooseSetup()
    {
        int i = Random.Range(0, 4);
        if (i == 0)
            RandomObstaclesSetup();
        else if (i == 1)
            ObstaclesSetup1();
        else if (i == 2)
            ObstaclesSetup2();
        else if (i == 3)
            ObstaclesSetup3();
        else
            ObstaclesSetup4();
    }

    public void SetupScene()
    {
        BoardSetup();
        ChooseSetup();
        //SpawnEnemies();
        //SpawnAllies();
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
