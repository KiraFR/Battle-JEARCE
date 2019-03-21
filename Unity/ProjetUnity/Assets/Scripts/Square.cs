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
    private bool canMoveIn = false;

    public void OnMouseDown()
    {
        /*
         * Gros code en prévision
         */

        if (character != null)
        {
            GameObject unit = gm.GetSelectedSquare();
            if (unit == null)
            {
                gm.SetSelectedSquare(gameObject);
                // TOCHANGE
                // GameManager.instance.
                GameManager.instance.MovableSquares((int)transform.position.x, (int)transform.position.y, character.movePoint.currentStat);
                GameManager.instance.AttackSquares((int)transform.position.x, (int)transform.position.y, character.movePoint.currentStat,character.maxDistAttack.currentStat);
            }
            else if (unit.GetComponent<Square>().GetCharacter().Equals(character))
            {

                gm.SetSelectedSquare(null);
            }
            else if (unit != null)
            {

                Vector3 pos = unit.transform.position;
                if ((pos.x != transform.position.x && pos.y != transform.position.y) || (pos.x == transform.position.x && pos.y == transform.position.y))
                {

                    gm.ClearMovingTiles();
                    gm.SetSelectedSquare(null);
                }
            }
        }
        else
        {
            GameObject unit = gm.GetSelectedSquare();
            Debug.Log(unit);
            if (canMoveIn)
            {
                Debug.Log("move in " + transform.position.x + "," + transform.position.y);
                //TODO MOVE CHARACTER
                gm.ClearMovingTiles();
                character = unit.GetComponent<Square>().GetCharacter();
                Debug.Log(character + " à " + transform.position.x + "," + transform.position.y);
                unit.GetComponent<Square>().GetCharacter().GetComponent<Character>().Move(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                unit.GetComponent<Square>().SetCharacter(null);
                gm.SetSelectedSquare(null);
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
