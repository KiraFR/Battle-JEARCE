using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameObject unit = gm.GetSelectedUnit();
            if (unit == null)
            {
                
                GameObject actionMenu = Instantiate(gm.canvasAction, new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                gm.SetInstantiatedCanvasAction(actionMenu);
                gm.SetSelectedUnit(gameObject);
            }
            else if (unit.GetComponent<Square>().GetCharacter().Equals(character.gameObject))
            {
                
                gm.SetSelectedUnit(null);
                gm.RemoveInstantiatedCanvasAction();
            }
            else if (unit != null)
            {
                
                Vector3 pos = unit.transform.position;
                if ((pos.x != transform.position.x && pos.y != transform.position.y) || (pos.x == transform.position.x && pos.y == transform.position.y))
                {
                    
                    gm.ClearMovingTiles();
                    gm.RemoveInstantiatedCanvasAction();
                    gm.SetSelectedUnit(null);
                }
            }
        }
        else
        {
            GameObject unit = gm.GetSelectedUnit();
            Debug.Log(unit);
            if(canMoveIn)
            {
                Debug.Log("move in " + transform.position.x + "," + transform.position.y);
                //TODO MOVE CHARACTER
                gm.ClearMovingTiles();
                character = unit.GetComponent<Square>().GetCharacter();
                unit.GetComponent<Square>().GetCharacter().GetComponent<Character>().Move(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                unit.GetComponent<Square>().SetCharacter(null);
            }
            else
            {
                if (GameManager.instance.GetinstantiatedCanvasAction() == null)
                {
                    gm.ClearMovingTiles();
                    gm.SetSelectedUnit(null);
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
        //Debug.Log(transform.position.x + ", " + transform.position.y + ", " + color);
        gameObject.GetComponent<SpriteRenderer>().sprite = color;
    }
    public void ChangeSquare(int xDir, int yDir)
    {
        GameObject right = GameManager.instance.GetGameObject((xDir - 1), yDir);
        if (right != null)
        {
            right.GetComponent<Square>().SetColor(moveSprite);
            right.GetComponent<Square>().SetMovable(true);
            GameManager.instance.AddMovingTiles(right);
        }
        GameObject left = GameManager.instance.GetGameObject(xDir + 1, yDir);
        if (left != null)
        {
            left.GetComponent<Square>().SetColor(moveSprite);
            left.GetComponent<Square>().SetMovable(true);
            GameManager.instance.AddMovingTiles(left);

        }
        GameObject down = GameManager.instance.GetGameObject(xDir, (yDir - 1));
        if (down != null)
        {
            down.GetComponent<Square>().SetColor(moveSprite);
            down.GetComponent<Square>().SetMovable(true);
            GameManager.instance.AddMovingTiles(down);
        }
        GameObject up = GameManager.instance.GetGameObject(xDir, (yDir + 1));
        if (up != null)
        {
            up.GetComponent<Square>().SetColor(moveSprite);
            up.GetComponent<Square>().SetMovable(true);
            GameManager.instance.AddMovingTiles(up);
        }
    }
}
