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

    private GameObject ButtonEnd;

    private int[,] plateauDeJeux = new int[6, 9];
    private List<Vector3> chemin = null;

    public RuntimeAnimatorController selectedSquareAnim;

    private List<GameObject> enemies;
    private List<GameObject> allies;
    private GameObject selectedSquare = null;
    private List<GameObject> movingTiles;
    private bool playerTurn;
    private bool phase;

    private Character cible = null;
    private Character unite = null;

    private List<GameObject> caseCible ;




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
        caseCible = new List<GameObject>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        moveText = GameObject.Find("MoveText").GetComponent<Text>();
        attackText = GameObject.Find("AttackText").GetComponent<Text>();
        ButtonEnd = GameObject.Find("EndTurn");
        phase = true;
        ButtonEnd.GetComponentInChildren<Text>().text = "Ready";

        InitGame();
        
        playerTurn = false;
    }

    void InitGame()
    {
        movingTiles.Clear();
        boardScript.SetupScene();
        boardScript.InitPlacement();
    }


    public void StartGame()
    {
        ClearMovingTiles();
        phase = false;
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
            tile.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite = tile.GetComponent<Square>().moveSprite;
            tile.GetComponent<Square>().SetMovable(true);
            movingTiles.Add(tile);
        }
    }

    public void AddMovingAttack(GameObject tile)
    {
        if (tile != null)
        {
            tile.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite = tile.GetComponent<Square>().attackSprite;
            tile.GetComponent<Square>().SetMovable(false);
            movingTiles.Add(tile);
        }
    }

    public void ClearMovingTiles()
    {
        if (movingTiles.Count > 0)
        {
            if (selectedSquare != null)
            {
                selectedSquare.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite = selectedSquare.GetComponent<Square>().baseSprite;
            }
            foreach (GameObject obj in movingTiles)
            {
                obj.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite = obj.GetComponent<Square>().baseSprite;
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
            if (objet.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
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
                            if (objet.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
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

    public void ResetStats()
    {
        healthText.text = "";
        moveText.text = "";
        attackText.text = "";
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }

    public bool GetPhase()
    {
        return phase;
    }

    public void EndTurn()
    {
        playerTurn = false;

        foreach (GameObject ally in allies)
        {
            ally.GetComponent<Character>().resetTurn();
        }
        ResetStats();
        playerTurn = true;
    }

    public void Deplacement(int xArrive, int yArrive, int xDepart, int yDepart)
    {
        List<Vector3> cheminTest = new List<Vector3>();
        Vector3 objet = new Vector3(xArrive, yArrive, 0);
        cheminTest.Add(objet);
        objet = new Vector3(xDepart, yDepart, 0);
        cheminTest.Add(objet);


        if (DistanceEntrePoint(xArrive, yArrive, xDepart, yDepart) != 1)
        {
            RecuDeplacement(cheminTest, DistanceEntrePoint(xArrive, yArrive, xDepart, yDepart));
        }
        else
        {
            objet = new Vector3(xArrive, yArrive, 0);
            cheminTest.Add(objet);
            chemin = cheminTest;
        }
        chemin.RemoveAt(0);
        chemin.Reverse();
    }

    public bool VerifCaseDispo(int x, int y)
    {
        GameObject objet = GetGameObject(x, y);
        if (objet != null && !GetSelectedSquare().Equals(objet))
        {
            if (objet.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RecuDeplacement(List<Vector3> cheminTest, int mouvement)
    {
        if (mouvement > 0)
        {
            if (cheminTest[cheminTest.Count - 1].y < 8)
            {
                List<Vector3> nouveau = new List<Vector3>(cheminTest);
                VerifDeplacement(nouveau, (int)cheminTest[cheminTest.Count - 1].x, (int)cheminTest[cheminTest.Count - 1].y + 1, mouvement - 1);
            }
            if (cheminTest[cheminTest.Count - 1].y > 0)
            {
                List<Vector3> nouveau = new List<Vector3>(cheminTest);
                VerifDeplacement(nouveau, (int)cheminTest[cheminTest.Count - 1].x, (int)cheminTest[cheminTest.Count - 1].y - 1, mouvement - 1);
            }
            if (cheminTest[cheminTest.Count - 1].x < 5)
            {
                List<Vector3> nouveau = new List<Vector3>(cheminTest);
                VerifDeplacement(nouveau, (int)cheminTest[cheminTest.Count - 1].x + 1, (int)cheminTest[cheminTest.Count - 1].y, mouvement - 1);
            }
            if (cheminTest[cheminTest.Count - 1].x > 0)
            {
                List<Vector3> nouveau = new List<Vector3>(cheminTest);
                VerifDeplacement(nouveau, (int)cheminTest[cheminTest.Count - 1].x - 1, (int)cheminTest[cheminTest.Count - 1].y, mouvement - 1);
            }
        }
    }

    public void VerifDeplacement(List<Vector3> cheminTest, int x, int y, int mouvement)
    {
        if (VerifCaseDispo(x, y))
        {
            Vector3 vecteur = new Vector3(x, y, 0);
            cheminTest.Add(vecteur);
            if (cheminTest[0].x == cheminTest[cheminTest.Count - 1].x && cheminTest[0].y == cheminTest[cheminTest.Count - 1].y && cheminTest.Count == DistanceEntrePoint((int)cheminTest[0].x, (int)cheminTest[0].y, (int)cheminTest[1].x, (int)cheminTest[1].y) + 2 && mouvement == 0)
            {
                chemin = new List<Vector3>(cheminTest);
            }
            else
            {
                RecuDeplacement(cheminTest, mouvement);
            }

        }
    }

    public List<Vector3> GetChemin()
    {
        return chemin;
    }


    public void DeplacementAttaque(int posUniteX, int posUniteY, int posEnemiX, int posEnemiY, int minDisAttaque, int maxDisAttaque)
    {
        int porte = maxDisAttaque - minDisAttaque;
        if (porte == 0)
            porte = 1;
        RecuDeplacementAttaque(posEnemiX, posEnemiY,porte, maxDisAttaque);
    }

    public void RecuDeplacementAttaque(int posX, int posY, int porte , int mouvement)
    {
        if (mouvement > 0)
        {
            if (posX < 5)
                VerifDeplacementAttaque(posX + 1, posY, porte, mouvement);
            if (posY > 0)
                VerifDeplacementAttaque(posX, posY - 1, porte, mouvement);
            if (posX > 0)
                VerifDeplacementAttaque(posX - 1, posY, porte, mouvement);
            if (posY < 8)
                VerifDeplacementAttaque(posX, posY + 1, porte, mouvement);
        }
    }

    public void VerifDeplacementAttaque(int posX, int posY, int porte, int mouvement)
    {
        GameObject objet = GetGameObject(posX,posY);
        if (objet != null && !GetSelectedSquare().Equals(objet))
        {
            if (objet.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().moveSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if (character == null)
                {
                    if (porte >= mouvement)
                    {
                        AddCibleCase(objet);
                        objet.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = objet.GetComponent<Square>().selectedSquareAnim;
                    }
                }
            }
        }
        RecuDeplacementAttaque(posX, posY, porte ,mouvement - 1);
    }

    public void MoveCharacter(Vector3 startPos, Vector3 endPos)
    {   
        GameObject square = boardScript.GetGameObject((int)endPos.x, (int)endPos.y);
        Character character = null;

        if (selectedSquare != null)
        {
            character = selectedSquare.GetComponent<Square>().GetCharacter();
        }
        else
        {
            GameObject squareStart = boardScript.GetGameObject((int)startPos.x, (int)startPos.y);
            if(squareStart != null)
            {
                character = squareStart.GetComponent<Square>().GetCharacter();
            }
        }

        //Debug.Log(character);

        if (character != null)
        {
            square.GetComponent<Square>().SetCharacter(character);


            ClearMovingTiles();


            Deplacement((int)endPos.x, (int)endPos.y, (int)startPos.x, (int)startPos.y);
            character.Move(new List<Vector3>(GetChemin()));

            if (selectedSquare != null)
            {
                selectedSquare.GetComponent<Square>().SetCharacter(null);
                selectedSquare.gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                SetSelectedSquare(null);
            }
            Vector3 calcul = new Vector3(Mathf.Abs(endPos.x - startPos.x), Mathf.Abs(endPos.y - startPos.y), 0);
            int essai = Mathf.RoundToInt(calcul.x + calcul.y);
            character.Move(essai);
        }
    }
    public void MoveCharacter(Character character, Vector3 path)
    {
        if (phase)
        {
            character.Move(path);
            selectedSquare.GetComponent<Square>().SetCharacter(null);
            selectedSquare.gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
            SetSelectedSquare(null);
            ResetStats();
        }
    }


    public void SetCible(Character c)
    {
        cible = c;
    }

    public Character GetCible()
    {
        return cible;
    }

    public void SetUnite(Character c)
    {
        unite = c;
    }

    public Character GetUnite()
    {
        return unite;
    }

    public void AddCibleCase(GameObject o)
    {
        if (!caseCible.Contains(o))
        {
            caseCible.Add(o);
        }
    }

    public List<GameObject> GetCibleCase()
    {
        return caseCible;
    }

    public void ClearCibleCase()
    {
        caseCible.Clear();
    }
}
