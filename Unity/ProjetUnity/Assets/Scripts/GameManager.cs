using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;

    private List<GameObject> enemies;
    private List<GameObject> allies;
    private GameObject selectedSquare = null;
    private List<GameObject> movingTiles;
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
        InitGame();
    }

    void InitGame()
    {
        movingTiles.Clear();
        boardScript.SetupScene();
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
            tile.GetComponent<Square>().SetMovable(true);
            movingTiles.Add(tile);
        }
    }

    public void ClearMovingTiles()
    {
        foreach (GameObject obj in movingTiles)
        {
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<Square>().baseSprite;
            obj.GetComponent<Square>().SetMovable(false);
        }
        movingTiles.Clear();
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

    public void MovableSquares(int posX, int posY, int mouvement)
    {
        if (mouvement == 0) return;
        GameObject objet = GetGameObject(posX + 1, posY);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite || objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().moveSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX + 1, posY, mouvement - 1);
                }
            }
        }
        objet = GetGameObject(posX - 1, posY);
        if (objet != null && !selectedSquare.Equals(objet))
        {

            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite || objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().moveSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX - 1, posY, mouvement - 1);
                }
            }
        }
        objet = GetGameObject(posX, posY + 1);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite || objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().moveSprite)
            {

                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX, posY + 1, mouvement - 1);
                }
            }
        }
        objet = GetGameObject(posX, posY - 1);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite || objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().moveSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX, posY - 1, mouvement - 1);
                }
            }
        }
    }

    public void AttackSquares(int posX, int posY, int mouvement, int minDistAttack, int maxDistAttack)
    {
        if (maxDistAttack == 0) return;
        GameObject objet = GetGameObject(posX + 1, posY);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    if (mouvement != 0)
                    {
                        AttackSquares(posX + 1, posY, mouvement - 1,minDistAttack, maxDistAttack);
                    }
                    else
                    {
                        if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite && maxDistAttack == 1)
                        {
                            AddMovingAttack(objet);
                        }
                        AttackSquares(posX + 1, posY, mouvement,minDistAttack, maxDistAttack - 1);
                    }
                }
                else
                {
                    if(mouvement!=0)
                        AddMovingAttack(objet);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        int distance = (int)(Mathf.Abs(GetSelectedSquare().transform.position.x - enemies[i].transform.position.x) + (int)Mathf.Abs(GetSelectedSquare().transform.position.y - enemies[i].transform.position.y));
                        if (minDistAttack <= distance)
                            AddMovingAttack(objet);
                    }
                    if (maxDistAttack > 1)
                        AttackSquares(posX + 1, posY, 0, minDistAttack, maxDistAttack - 1);
                }
            }
            else
            {
                if (maxDistAttack > 1)
                    AttackSquares(posX + 1, posY, 0, minDistAttack, maxDistAttack - 1);
            }
        }
        objet = GetGameObject(posX - 1, posY);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    if (mouvement != 0)
                    {
                        AttackSquares(posX -1 , posY, mouvement - 1, minDistAttack, maxDistAttack);
                    }
                    else
                    {
                        if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite && maxDistAttack == 1)
                        {
                            AddMovingAttack(objet);
                        }
                        AttackSquares(posX -1 , posY, mouvement, minDistAttack, maxDistAttack - 1);
                    }
                }
                else
                {
                    if (mouvement != 0)
                        AddMovingAttack(objet);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        int distance = (int)(Mathf.Abs(GetSelectedSquare().transform.position.x - enemies[i].transform.position.x) + (int)Mathf.Abs(GetSelectedSquare().transform.position.y - enemies[i].transform.position.y));
                        if (minDistAttack <= distance)
                            AddMovingAttack(objet);
                    }
                    if (maxDistAttack > 1)
                        AttackSquares(posX - 1, posY, 0, minDistAttack, maxDistAttack - 1);
                }
            }
            else
            {
                if (maxDistAttack > 1)
                    AttackSquares(posX - 1, posY, 0, minDistAttack, maxDistAttack - 1);
            }
        }
        objet = GetGameObject(posX, posY + 1);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    if (mouvement != 0)
                    {
                        AttackSquares(posX , posY+1, mouvement - 1, minDistAttack, maxDistAttack);
                    }
                    else
                    {
                        if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite && maxDistAttack == 1)
                        {
                            AddMovingAttack(objet);
                        }
                        AttackSquares(posX, posY+1, mouvement, minDistAttack, maxDistAttack - 1);
                    }
                }
                else
                {
                    if (mouvement != 0)
                        AddMovingAttack(objet);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        int distance = (int)(Mathf.Abs(GetSelectedSquare().transform.position.x - enemies[i].transform.position.x) + (int)Mathf.Abs(GetSelectedSquare().transform.position.y - enemies[i].transform.position.y));
                        if (minDistAttack <= distance)
                            AddMovingAttack(objet);
                    }
                    if (maxDistAttack > 1)
                        AttackSquares(posX, posY+1, 0, minDistAttack, maxDistAttack - 1);
                }
            }
            else
            {
                if (maxDistAttack > 1)
                    AttackSquares(posX, posY + 1, 0, minDistAttack, maxDistAttack - 1);
            }
        }
        objet = GetGameObject(posX, posY - 1);
        if (objet != null && !selectedSquare.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    if (mouvement != 0)
                    {
                        AttackSquares(posX, posY-1, mouvement - 1, minDistAttack, maxDistAttack);
                    }
                    else
                    {
                        if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite && maxDistAttack == 1)
                        {
                            AddMovingAttack(objet);
                        }
                        AttackSquares(posX, posY-1, mouvement, minDistAttack, maxDistAttack - 1);
                    }
                }
                else
                {
                    if (mouvement != 0)
                        AddMovingAttack(objet);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        int distance = (int)(Mathf.Abs(GetSelectedSquare().transform.position.x - enemies[i].transform.position.x) + (int)Mathf.Abs(GetSelectedSquare().transform.position.y - enemies[i].transform.position.y));
                        if (minDistAttack<=distance)
                            AddMovingAttack(objet);
                    }
                    if (maxDistAttack > 1)
                        AttackSquares(posX, posY-1, 0, minDistAttack, maxDistAttack - 1);
                }
            }
            else
            {
                if (maxDistAttack > 1)
                    AttackSquares(posX, posY - 1, 0, minDistAttack, maxDistAttack - 1);
            }
        }
    }
}