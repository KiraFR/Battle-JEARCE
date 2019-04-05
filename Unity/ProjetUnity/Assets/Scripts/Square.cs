using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Sprite baseSprite;
    public Sprite moveSprite;
    public Sprite attackSprite;
    public Sprite inaccessibleSprite;
    public RuntimeAnimatorController selectedSquareAnim;

    private List<Vector3> chemin = null;

    private GameManager gm = GameManager.instance;
    private Character character = null;
    private Character lastCharacter = null;
    private bool canMoveIn = false;
    private Vector3 lastPos = new Vector3();

    public void OnMouseDown()
    {
        /*
         * Gros code en prévision
         */
        if (!gm.GetPhase())
        {
            if (gm.GetPlayerTurn())
            {
                if (character != null)
                {
                    gm.ChangeMove(character.movePoint.currentStat);
                    gm.ChangeHealth(character.healthPoint.currentStat);
                    gm.ChangeAttack(character.attackPoint.currentStat);
                    GameObject selectedSquare = gm.GetSelectedSquare();
                    if (gm.IsAlly(character.gameObject))
                    {
                        if (selectedSquare == null)
                        {
                            gm.SetSelectedSquare(gameObject);
                            transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = selectedSquareAnim;
                            // TO CHANGE

                            //A changer nom de fonction
                            GameManager.instance.AttackSquares((int)transform.position.x, (int)transform.position.y, character.movePoint.currentStat, character.minDistAttack.currentStat, character.maxDistAttack.currentStat);
                        }
                        else if (selectedSquare.GetComponent<Square>().GetCharacter().Equals(character))
                        {
                            gm.ClearMovingTiles();
                            transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                            gm.SetSelectedSquare(null);
                        }
                    }
                    else
                    {
                        if (selectedSquare != null)
                        {
                            lastPos = new Vector3(selectedSquare.transform.position.x, selectedSquare.transform.position.y, 0);
                            lastCharacter = selectedSquare.GetComponent<Square>().GetCharacter();
                            int distance = (int)Mathf.Abs(lastPos.x - transform.position.x) + (int)Mathf.Abs(lastPos.y - transform.position.y);

                            //Verifie ennemi -> gm.IsEnemy(unit) + verifie a cote 
                            if (gm.IsEnemy(character.gameObject))
                            {
                                if (lastCharacter.GetComponent<Character>().maxDistAttack.baseStat >= distance && lastCharacter.GetComponent<Character>().minDistAttack.baseStat <= distance)
                                {
                                    if (Attaque(character, lastCharacter))
                                        character = null;
                                    gm.ClearMovingTiles();
                                    selectedSquare.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                                    gm.SetSelectedSquare(null);
                                }
                            }
                        }
                    }
                }
                else
                {

                    GameObject selectedSquare = gm.GetSelectedSquare();
                    if (canMoveIn)
                    {
                        gm.ClearMovingTiles();
                        character = selectedSquare.GetComponent<Square>().GetCharacter();

                        character = selectedSquare.GetComponent<Square>().GetCharacter();

                        Deplacement((int)transform.position.x, (int)transform.position.y, (int)character.transform.position.x, (int)character.transform.position.y);
                        selectedSquare.GetComponent<Square>().GetCharacter().GetComponent<Character>().Move(chemin);
                        selectedSquare.GetComponent<Square>().SetCharacter(null);
                        selectedSquare.gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                        gm.SetSelectedSquare(null);

                        Vector3 start = selectedSquare.transform.position;
                        Vector3 finish = transform.position;
                        Vector3 calcul = new Vector3(Mathf.Abs(finish.x - start.x), Mathf.Abs(finish.y - start.y), 0);
                        int essai = Mathf.RoundToInt(calcul.x + calcul.y);
                        character.Move(essai);
                    }
                    else
                    {
                        gm.ClearMovingTiles();
                        if (selectedSquare != null)
                        {
                            selectedSquare.gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                        }
                        gm.SetSelectedSquare(null);
                        gm.ResetStats();
                    }
                }
            }
        }
        else
        {

            if (character != null)
            {
                GameObject selectedSquare = gm.GetSelectedSquare();
                if (gm.IsAlly(character.gameObject))
                {
                    if (selectedSquare == null)
                    {
                        gm.SetSelectedSquare(gameObject);
                        
                        transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = selectedSquareAnim;

                    }
                    else if (selectedSquare.GetComponent<Square>().GetCharacter().Equals(character))
                    {
                        transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                        gm.SetSelectedSquare(null);
                    }
                }
            }
            else
            {
                GameObject selectedSquare = gm.GetSelectedSquare();
                if (canMoveIn)
                {
                    selectedSquare.GetComponent<Square>().GetCharacter().transform.position =  new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    selectedSquare.GetComponent<Square>().SetCharacter(null);
                    character = selectedSquare.GetComponent<Square>().GetCharacter();
                    selectedSquare.gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                    gm.SetSelectedSquare(null);

                }
                else
                {
                    if (selectedSquare != null)
                    {
                        selectedSquare.gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                    }
                    gm.SetSelectedSquare(null);
                }
            }
        }
    }
    

    public Character GetCharacter()
    {
        return character;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public void SetMovable(bool move)
    {
        canMoveIn = move;
    }

    public bool CanMoveIn()
    {
        return canMoveIn;
    }

    public void SetColor(Sprite color)
    {
        gameObject.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite = color;
    }

    public bool Attaque(Character enemi, Character charac)
    {
        enemi.GetComponent<Character>().healthPoint.currentStat = enemi.GetComponent<Character>().healthPoint.currentStat - charac.GetComponent<Character>().attackPoint.baseStat;
        if (enemi.GetComponent<Character>().healthPoint.currentStat <= 0)
        {
            Destroy(enemi.gameObject);
            return true;
        }
        return false;
    }

    public void Deplacement(int xArrive, int yArrive, int xDepart, int yDepart)
    {
        List<Vector3> cheminTest = new List<Vector3>();
        Vector3 objet = new Vector3(xArrive, yArrive, 0);
        cheminTest.Add(objet);
        objet = new Vector3(xDepart, yDepart, 0);
        cheminTest.Add(objet);

        //Debug.Log(xDepart + " " + yDepart + " et " + xArrive + " " + yArrive+" distance = "+ gm.DistanceEntrePoint(xArrive, yArrive, xDepart, yDepart));

        if (gm.DistanceEntrePoint(xArrive, yArrive, xDepart, yDepart) != 1)
        {
            RecuDeplacement(cheminTest, gm.DistanceEntrePoint(xArrive, yArrive, xDepart, yDepart));
        }
        else
        {
            objet = new Vector3(xArrive, yArrive, 0);
            cheminTest.Add(objet);
            chemin = cheminTest;
        }

        for (int i = 0; i < chemin.Count; i++)
        {
            Debug.Log(chemin[i].x + " " + chemin[i].y);
        }
    }

    public bool VerifCaseDispo(int x, int y)
    {
        GameObject objet = gm.GetGameObject(x, y);
        if (objet != null && !gm.GetSelectedSquare().Equals(objet))
        {
            if (objet.transform.Find("FloorBase").GetComponent<SpriteRenderer>().sprite != objet.GetComponent<Square>().inaccessibleSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !gm.IsEnemy(character.gameObject)) || character == null)
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
                VerifDeplacement(nouveau, (int)cheminTest[cheminTest.Count - 1].x + 1,(int) cheminTest[cheminTest.Count - 1].y,mouvement-1);
            }
            if (cheminTest[cheminTest.Count - 1].x > 0)
            {
                List<Vector3> nouveau = new List<Vector3>(cheminTest);
                VerifDeplacement(nouveau, (int)cheminTest[cheminTest.Count - 1].x - 1, (int)cheminTest[cheminTest.Count - 1].y,mouvement-1);
            }
        }
    }

    public void VerifDeplacement(List<Vector3> cheminTest, int x, int y, int mouvement)
    {
        if (VerifCaseDispo(x, y))
        {
            Vector3 vecteur = new Vector3(x, y, 0);
            cheminTest.Add(vecteur);
            if (cheminTest[0].x == cheminTest[cheminTest.Count - 1].x && cheminTest[0].y == cheminTest[cheminTest.Count - 1].y && cheminTest.Count == gm.DistanceEntrePoint((int)cheminTest[0].x, (int)cheminTest[0].y, (int)cheminTest[1].x, (int)cheminTest[1].y) + 2 && mouvement==0)
            {
                chemin = new List<Vector3>(cheminTest);
            }
            else
            {
                RecuDeplacement(cheminTest, mouvement);
            }

        }
    }
}
