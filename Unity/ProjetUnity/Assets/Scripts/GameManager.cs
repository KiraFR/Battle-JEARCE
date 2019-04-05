using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;

    private Text healthText;
    private Text moveText;
    private Text attackText;

    private int[,] plateauDeJeux = new int[6, 9];

    private List<GameObject> enemies;
    private List<GameObject> allies;
    private GameObject selectedSquare = null;
    private List<GameObject> movingTiles;
    private bool playerTurn;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        movingTiles = new List<GameObject>();
        enemies = new List<GameObject>();
        allies = new List<GameObject>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        moveText = GameObject.Find("MoveText").GetComponent<Text>();
        attackText = GameObject.Find("AttackText").GetComponent<Text>();
        InitGame();


        playerTurn = true;
    }

    void InitGame()
    {
        movingTiles.Clear();
        boardScript.SetupScene();
        boardScript.InitPlacement();
    }


    public void StartGame()
    {

    }

    public GameObject GetGameObject(int xDir, int yDir)
    {
        return boardScript.GetGameObject(xDir, yDir);
    }

    public GameObject GetSelectedSquare()
    {
        return selectedSquare;
    }

    public void SetSelectedSquare(GameObject square)
    {
        selectedSquare = square;
    }

    public void AddMovingTiles(GameObject tile)
    {
        if (tile != null)
        {
            tile.GetComponent<SpriteRenderer>().sprite = tile.GetComponent<Square>().moveSprite;
            tile.GetComponent<Square>().SetMovable(true);
            movingTiles.Add(tile);
        }
    }

    public void AddMovingAttack(GameObject tile)
    {
        if (tile != null)
        {
            tile.GetComponent<SpriteRenderer>().sprite = tile.GetComponent<Square>().attackSprite;
            tile.GetComponent<Square>().SetMovable(false);
            movingTiles.Add(tile);
        }
    }

    public void ClearMovingTiles()
    {
        if (movingTiles.Count > 0)
        {
            selectedSquare.GetComponent<SpriteRenderer>().sprite = selectedSquare.GetComponent<Square>().baseSprite;
            foreach (GameObject obj in movingTiles)
            {
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<Square>().baseSprite;
                obj.GetComponent<Square>().SetMovable(false);
            }
            movingTiles.Clear();
        }
    }

    public void AddToEnemies(GameObject unit)
    {
        if (!enemies.Contains(unit))
        {
            enemies.Add(unit);
        }
    }

    public void AddToAllies(GameObject unit)
    {
        if (!allies.Contains(unit))
        {
            allies.Add(unit);
        }
    }

    public bool IsEnemy(GameObject unit)
    {
        return enemies.Contains(unit);
    }

    public bool IsAlly(GameObject unit)
    {
        return allies.Contains(unit);
    }

    public int DistanceEntrePoint(int x, int y, int x2, int y2)
    {
        return (int)(Mathf.Abs(x - x2) + (int)Mathf.Abs(y - y2));
    }

    public void AttackSquares(int posX, int posY, int mouvement, int minDistAttack, int maxDistAttack)
    {
        PlateauAZero();
        FonctionRecu(posX, posY, mouvement, minDistAttack, maxDistAttack);
    }

    public void FonctionRecu(int posX, int posY, int mouvement, int minDistAttack, int maxDistAttack)
    {
        plateauDeJeux[posX, posY] = 1;
        if (posX < 5)
            VerifCase(posX + 1, posY, mouvement, minDistAttack, maxDistAttack);
        if (posY > 0)
            VerifCase(posX, posY - 1, mouvement, minDistAttack, maxDistAttack);
        if (posX > 0)
            VerifCase(posX - 1, posY, mouvement, minDistAttack, maxDistAttack);
        if (posY < 8)
            VerifCase(posX, posY + 1, mouvement, minDistAttack, maxDistAttack);

    }

    public void VerifCase(int posX, int posY, int mouvement, int minDistAttack, int maxDistAttack)
    {
        if (maxDistAttack == 0)
            return;
        if(plateauDeJeux[posX, posY] == 1 && mouvement == 0)
            FonctionRecu(posX, posY, 0, minDistAttack, maxDistAttack-1);

        GameObject objet = GetGameObject(posX, posY);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    if (mouvement > 0)
                    {
                        AddMovingTiles(objet);
                        FonctionRecu(posX, posY, mouvement - 1, minDistAttack, maxDistAttack);
                    }
                    else
                    {
                        if (minDistAttack <= DistanceEntrePoint((int)GetSelectedSquare().transform.position.x, (int)GetSelectedSquare().transform.position.y, posX , posY))
                        {
                            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
                            {
                                AddMovingAttack(objet);
                            }
                        }
                        FonctionRecu(posX, posY, 0, minDistAttack, maxDistAttack - 1);
                    }
                }
                else
                {
                    if (mouvement > 0)
                        AddMovingAttack(objet);
                    else
                    {
                        if (DistanceEntrePoint(posX, posY, (int)GetSelectedSquare().transform.position.x, (int)GetSelectedSquare().transform.position.y) >= minDistAttack)
                            AddMovingAttack(objet);
                    }
                    if (maxDistAttack > 1)
                        FonctionRecu(posX, posY, 0, minDistAttack, maxDistAttack - 1);
                }
            }
            else
            {
                if (maxDistAttack > 1)
                {
                    FonctionRecu(posX, posY, 0, minDistAttack, maxDistAttack - 1);
                }
            }
        }
    }

    public void PlateauAZero()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                plateauDeJeux[i, j] = 0;
            }
        }
    }

    public void ChangeHealth(int healthPoint)
    {
        healthText.text = "HP : " + healthPoint;
    }

    public void ChangeMove(int movePoint)
    {
        moveText.text = "Mouvement : " + movePoint;
    }

    public void ChangeAttack(int attackPoint)
    {
        attackText.text = "Attaque : " + attackPoint;
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }


    public void EndTurn()
    {
        playerTurn = false;

        foreach (GameObject ally in allies)
        {
            ally.GetComponent<Character>().resetTurn();
        }

        playerTurn = true;
    }

}
