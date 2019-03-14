using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Color baseColor;
    public Color moveColor;
    public Color attackColor;
    public Color inaccessibleColor;

    private Character character = null;
    public void OnMouseDown()
    {
        /*
         * Gros code en prévision
         */
        
        if (character != null)
        {

            if(GameManager.instance.GetSelectedUnit() != null)
            {
                GameManager.instance.SetSelectedUnit(character.gameObject);
                ChangeSquare((int)transform.position.x, (int)transform.position.y);
            }
            else
            {
                GameObject unit = GameManager.instance.GetGameObject((int)transform.position.x, (int)transform.position.y);
                if(unit != null)
                {
                    character = unit.GetComponent<Character>();
                    GameManager.instance.ClearMovingTiles();
                    ChangeSquare((int)transform.position.x, (int)transform.position.y);
                }
                else
                {
                    GameManager.instance.ClearMovingTiles();
                }
            }

            
        }

    }

    public void ChangeSquare(int xDir, int yDir)
    {
        GameObject right = GameManager.instance.GetGameObject((xDir - 1), yDir);
        if (right != null)
        {
            right.GetComponent<Square>().SetColor(moveColor);
            GameManager.instance.AddMovingTiles(right);
        }
        GameObject left = GameManager.instance.GetGameObject(xDir + 1, yDir);
        if (left != null)
        {
            left.GetComponent<Square>().SetColor(moveColor);
            GameManager.instance.AddMovingTiles(left);
        }
        GameObject down = GameManager.instance.GetGameObject(xDir, (yDir - 1));
        if (down != null)
        {
            down.GetComponent<Square>().SetColor(moveColor);
            GameManager.instance.AddMovingTiles(down);
        }
        GameObject up = GameManager.instance.GetGameObject(xDir, (yDir + 1));
        if (up != null)
        {
            up.GetComponent<SpriteRenderer>().color = moveColor;
            GameManager.instance.AddMovingTiles(up);
        }
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

}
