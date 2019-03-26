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
    public List<GameObject> Units;

    // Every Floor's gameObject from Vector3
    private Dictionary<Vector3,GameObject> floorGameObjects = new Dictionary<Vector3, GameObject>();

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
                // TODELETE --> DEBUG ALLY CHARACTER
                if (x == 0 && y == 0)
                {

                    GameObject ally = GameObject.Find("epeiste");
                    instance.GetComponent<Square>().SetCharacter(ally.GetComponent<Character>());
                    ally.GetComponent<Character>().SetState(true);
                    GameManager.instance.AddToAllies(ally);

                }

                // TODELETE --> DEBUG ENNEMY CHARACTER 
                if (x == 0 && y == 1)
                {
                    GameObject enemy = Instantiate(GameObject.Find("epeiste"), pos, Quaternion.identity) as GameObject;
                    instance.GetComponent<Square>().SetCharacter(enemy.GetComponent<Character>());
                    enemy.GetComponent<Character>().SetState(false);
                    GameManager.instance.AddToEnemies(enemy);
                }

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //Method that will first add obstacles randomly before checking is the board is blocked
    //If so then it'll call the method isBlocked() that will remove one obstacle to set the board playable
    void RandomObstaclesSetup()   
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


    // SETUP OF 4 BOARDS WITH 8 OBSTACLES PLACED SYMMETRICALLY

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup1()
    {
        GameObject obs1 = GetGameObject(0, 2);
        GameObject obs2 = GetGameObject(1, 3);
        GameObject obs3 = GetGameObject(2, 2);
        GameObject obs4 = GetGameObject(3, 2);

        GameObject obs5 = GetGameObject(columns - 1, rows - 3);
        GameObject obs6 = GetGameObject(columns - 2, rows - 4);
        GameObject obs7 = GetGameObject(columns - 3, rows - 3);
        GameObject obs8 = GetGameObject(columns - 4, rows - 3);

        PlaceObstacles(obs1, obs2, obs3, obs4, obs5, obs6, obs7, obs8);
    }

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup2()
    {
        GameObject obs1 = GetGameObject(0, 2);
        GameObject obs2 = GetGameObject(1, 3);
        GameObject obs3 = GetGameObject(1, 4);
        GameObject obs4 = GetGameObject(2, 5);

        GameObject obs5 = GetGameObject(columns - 1, rows - 3);
        GameObject obs6 = GetGameObject(columns - 2, rows - 4);
        GameObject obs7 = GetGameObject(columns - 2, rows - 5);
        GameObject obs8 = GetGameObject(columns - 3, rows - 6);

        PlaceObstacles(obs1, obs2, obs3, obs4, obs5, obs6, obs7, obs8);
    }

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup3()
    {
        GameObject obs1 = GetGameObject(0, 5);
        GameObject obs2 = GetGameObject(1, 2);
        GameObject obs3 = GetGameObject(2, 3);
        GameObject obs4 = GetGameObject(2, 4);

        GameObject obs5 = GetGameObject(columns - 1, rows - 6);
        GameObject obs6 = GetGameObject(columns - 2, rows - 3);
        GameObject obs7 = GetGameObject(columns - 3, rows - 4);
        GameObject obs8 = GetGameObject(columns - 3, rows - 5);

        PlaceObstacles(obs1, obs2, obs3, obs4, obs5, obs6, obs7, obs8);
    }

    //Creation of obstacles that are defined and not randomly picked
    void ObstaclesSetup4()
    {
        GameObject obs1 = GetGameObject(1, 3);
        GameObject obs2 = GetGameObject(1, 4);
        GameObject obs3 = GetGameObject(2, 4);
        GameObject obs4 = GetGameObject(2, 5);

        GameObject obs5 = GetGameObject(columns - 2, rows - 4);
        GameObject obs6 = GetGameObject(columns - 2, rows - 5);
        GameObject obs7 = GetGameObject(columns - 3, rows - 5);
        GameObject obs8 = GetGameObject(columns - 3, rows - 6);

        PlaceObstacles(obs1, obs2, obs3, obs4, obs5, obs6, obs7, obs8);
    }

    //Predefined obstacles are now replacing basic floors (only works for boards with 8 obstacles)
    public void PlaceObstacles(GameObject obs1, GameObject obs2, GameObject obs3, GameObject obs4,
        GameObject obs5, GameObject obs6, GameObject obs7, GameObject obs8)
    {
        obs1.GetComponent<SpriteRenderer>().sprite = obs1.GetComponent<Square>().inaccessibleSprite;
        obs2.GetComponent<SpriteRenderer>().sprite = obs2.GetComponent<Square>().inaccessibleSprite;
        obs3.GetComponent<SpriteRenderer>().sprite = obs3.GetComponent<Square>().inaccessibleSprite;
        obs4.GetComponent<SpriteRenderer>().sprite = obs4.GetComponent<Square>().inaccessibleSprite;
        obs5.GetComponent<SpriteRenderer>().sprite = obs5.GetComponent<Square>().inaccessibleSprite;
        obs6.GetComponent<SpriteRenderer>().sprite = obs6.GetComponent<Square>().inaccessibleSprite;
        obs7.GetComponent<SpriteRenderer>().sprite = obs7.GetComponent<Square>().inaccessibleSprite;
        obs8.GetComponent<SpriteRenderer>().sprite = obs8.GetComponent<Square>().inaccessibleSprite;
    }

    void ChooseSetup()
    {
        int i = UnityEngine.Random.Range(0, 4);
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
        InitialiseList();
        ChooseSetup();
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
