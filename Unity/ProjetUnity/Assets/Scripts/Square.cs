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
    public Sprite alliesSprite;
    public RuntimeAnimatorController selectedSquareAnim;


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
                        if (character.GetComponent<Character>().naPasJouer.currentStat == 0)
                        {
                            if (selectedSquare == null)
                            {
                                gm.SetSelectedSquare(gameObject);
                                transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = selectedSquareAnim;
                                gm.AttackSquares((int)transform.position.x, (int)transform.position.y, character.movePoint.currentStat, character.minDistAttack.currentStat, character.maxDistAttack.currentStat);
                            }
                            else if (selectedSquare.GetComponent<Square>().GetCharacter().Equals(character))
                            {
                                gm.ClearMovingTiles();
                                transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                                gm.SetSelectedSquare(null);
                                gm.ResetStats();
                            }
                            else
                            {
                                if (selectedSquare.GetComponent<Square>().GetCharacter().name == "Medecin(Clone)" && selectedSquare.GetComponent<Square>().GetCharacter().GetComponent<Character>().naPasJouer.currentStat == 0) 
                                {
                                    int distance = gm.DistanceEntrePoint((int)character.transform.position.x, (int)character.transform.position.y, (int)selectedSquare.GetComponent<Square>().GetCharacter().transform.position.x, (int)selectedSquare.GetComponent<Square>().GetCharacter().transform.position.y);
                                    if (selectedSquare.GetComponent<Square>().GetCharacter().GetComponent<Character>().maxDistAttack.baseStat >= distance && selectedSquare.GetComponent<Square>().GetCharacter().GetComponent<Character>().minDistAttack.baseStat <= distance)
                                    {
                                        if (character.healthPoint.baseStat != character.healthPoint.currentStat)
                                        {
                                            Heal(character);
                                            selectedSquare.GetComponent<Square>().GetCharacter().GetComponent<Character>().naPasJouer.currentStat = 1;
                                            selectedSquare.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                                            gm.ClearMovingTiles();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (selectedSquare != null)
                        {
                            lastPos = new Vector3(selectedSquare.transform.position.x, selectedSquare.transform.position.y, 0);
                            lastCharacter = selectedSquare.GetComponent<Square>().GetCharacter();
                            int distance = gm.DistanceEntrePoint((int)lastPos.x, (int)lastPos.y, (int)transform.position.x, (int)transform.position.y);

                            if (gm.IsEnemy(character.gameObject))
                            {
                                if (lastCharacter.GetComponent<Character>().maxDistAttack.baseStat >= distance && lastCharacter.GetComponent<Character>().minDistAttack.baseStat <= distance)
                                {
                                    if (Attaque(character, lastCharacter))
                                        character = null;
                                    gm.ClearMovingTiles();
                                    selectedSquare.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                                    gm.SetSelectedSquare(null);
                                    gm.ResetStats();
                                    lastCharacter.GetComponent<Character>().naPasJouer.currentStat = 1;
                                }
                                else
                                {
                                    int porteMax = lastCharacter.GetComponent<Character>().movePoint.currentStat + lastCharacter.GetComponent<Character>().maxDistAttack.baseStat;
                                    if (distance <= porteMax && lastCharacter.GetComponent<Character>().movePoint.currentStat != 0)
                                    {
                                        gm.DeplacementAttaque((int)lastPos.x, (int)lastPos.y, (int)transform.position.x, (int)transform.position.y, lastCharacter.GetComponent<Character>().minDistAttack.baseStat, lastCharacter.GetComponent<Character>().maxDistAttack.baseStat);
                                        gm.SetCible(character);
                                        gm.SetUnite(lastCharacter);
                                        lastCharacter.GetComponent<Character>().naPasJouer.currentStat = 1;
                                    }

                                    gm.ChangeMove(character.movePoint.currentStat);
                                    gm.ChangeHealth(character.healthPoint.currentStat);
                                    gm.ChangeAttack(character.attackPoint.currentStat);
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
                        if (transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController == selectedSquareAnim)
                        {
                            gm.MoveCharacter(new Vector3(selectedSquare.transform.position.x, selectedSquare.transform.position.y, 0f), new Vector3(transform.position.x, transform.position.y, 0f));
                            Attaque(gm.GetCible(),gm.GetUnite());
                            for(int i = 0; i < gm.GetCibleCase().Count; i++)
                            {
                                gm.GetCibleCase()[i].gameObject.transform.Find("UnderFloor").GetComponent<Animator>().runtimeAnimatorController = null;
                            }
                            gm.ClearCibleCase();
                        }
                        else
                            gm.MoveCharacter(new Vector3(selectedSquare.transform.position.x, selectedSquare.transform.position.y,0f), new Vector3(transform.position.x, transform.position.y,0f));
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
            else
            {
                if(character != null)
                {
                    gm.ChangeMove(character.movePoint.currentStat);
                    gm.ChangeHealth(character.healthPoint.currentStat);
                    gm.ChangeAttack(character.attackPoint.currentStat);
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
                        gm.ResetStats();
                    }
                }
            }
            else
            {
                GameObject selectedSquare = gm.GetSelectedSquare();
                if (canMoveIn)
                {
                    if (selectedSquare != null)
                    {
                        character = selectedSquare.GetComponent<Square>().GetCharacter();
                        gm.MoveCharacter(character, new Vector3(transform.position.x, transform.position.y, transform.position.z));
                    }
                }
                else
                {
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
        return enemi.GetAttacked(charac.GetComponent<Character>().attackPoint.baseStat);
    }

    public void Heal(Character allie)
    {
        int pourcentage = allie.GetComponent<Character>().healthPoint.baseStat*25/100;
        allie.GetComponent<Character>().healthPoint.currentStat = allie.GetComponent<Character>().healthPoint.currentStat + pourcentage;
        if(allie.GetComponent<Character>().healthPoint.baseStat < allie.GetComponent<Character>().healthPoint.currentStat)
        {
            allie.GetComponent<Character>().healthPoint.currentStat = allie.GetComponent<Character>().healthPoint.baseStat;
        }
        allie.GetHealed(allie.GetComponent<Character>().healthPoint.currentStat);
    }

}
