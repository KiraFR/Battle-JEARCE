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

        if (character != null)
        {
            character.ChangeMove();
            GameObject unit = gm.GetSelectedSquare();
            if (unit == null)
            {
                gm.SetSelectedSquare(gameObject);
                // TOCHANGE
                // GameManager.instance.
                GameManager.instance.MovableSquares((int)transform.position.x, (int)transform.position.y, character.movePoint.currentStat);
                GameManager.instance.AttackSquares((int)transform.position.x, (int)transform.position.y, character.movePoint.currentStat, character.maxDistAttack.currentStat);
            }
            else if (unit.GetComponent<Square>().GetCharacter().Equals(character))
            {
                gm.ClearMovingTiles();
                gm.SetSelectedSquare(null);
            }
            else
            {
                lastPos = new Vector3(unit.transform.position.x, unit.transform.position.y, 0);
                lastCharacter = unit.GetComponent<Square>().GetCharacter();
                //Verifie ennemi -> gm.IsEnemy(unit) + verifie a cote 
                if (!unit.GetComponent<Square>().GetCharacter().Equals(character))
                {
                    if (lastCharacter.GetComponent<Character>().maxDistAttack.baseStat==2) {
                        if ((lastPos.x == transform.position.x + 2 && lastPos.y == transform.position.y) || 
                        (lastPos.x == transform.position.x - 2 && lastPos.y == transform.position.y) || 
                            (lastPos.x == transform.position.x && lastPos.y == transform.position.y + 2) || 
                        (lastPos.x == transform.position.x && lastPos.y == transform.position.y - 2) ||
                           (lastPos.x == transform.position.x+1 && lastPos.y == transform.position.y - 1)||
                        (lastPos.x == transform.position.x -1 && lastPos.y == transform.position.y - 1) ||
                           (lastPos.x == transform.position.x + 1 && lastPos.y == transform.position.y + 1) ||
                        (lastPos.x == transform.position.x - 1 && lastPos.y == transform.position.y + 1))
                        {
                            Debug.Log("Attaque archer");
                            Vector3 pos = unit.transform.position;
                            Debug.Log(lastPos.x + " " + lastPos.y + " " + transform.position.x + " " + transform.position.y);
                        }
                        }
                    else
                    {
                        if ((lastPos.x == transform.position.x + 1 && lastPos.y == transform.position.y) || (lastPos.x == transform.position.x - 1 && lastPos.y == transform.position.y) || (lastPos.x == transform.position.x && lastPos.y == transform.position.y + 1) || (lastPos.x == transform.position.x && lastPos.y == transform.position.y - 1))
                        {
                            Debug.Log("Attaque cac");
                            Vector3 pos = unit.transform.position;
                            Debug.Log(lastPos.x + " " + lastPos.y + " " + transform.position.x + " " + transform.position.y);
                        }
                    }


                }
            }
        }
        else
        {
            GameObject unit = gm.GetSelectedSquare();
            if (canMoveIn)
            {
                gm.ClearMovingTiles();
                character = unit.GetComponent<Square>().GetCharacter();
                unit.GetComponent<Square>().GetCharacter().GetComponent<Character>().Move(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                unit.GetComponent<Square>().SetCharacter(null);
                gm.SetSelectedSquare(null);

                Vector3 start = unit.transform.position;
                Vector3 finish = transform.position;
                Vector3 calcul = new Vector3(Mathf.Abs(finish.x - start.x), Mathf.Abs(finish.y - start.y), 0);
                int essai = Mathf.RoundToInt(calcul.x + calcul.y);
                character.Move(essai);

            }
            else
            {
                gm.ClearMovingTiles();
                gm.SetSelectedSquare(null);
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
        //Debug.Log(transform.position.x + ", " + transform.position.y + ", " + color);
        gameObject.GetComponent<SpriteRenderer>().sprite = color;
    }
}
