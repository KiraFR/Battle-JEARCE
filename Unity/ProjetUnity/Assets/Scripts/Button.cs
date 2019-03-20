using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void MoveAction()
    {
        GameObject unitSquare = GameManager.instance.GetSelectedUnit();
        if (unitSquare != null)
        {
            GameManager.instance.RemoveInstantiatedCanvasAction();
            Square square = unitSquare.GetComponent<Square>();
            GameManager.instance.MovableSquares((int)square.gameObject.transform.position.x, (int)square.gameObject.transform.position.y, square.GetCharacter().movePoint.currentStat);
            //Debug.Log("move");
        }
    }
    public void AttackAction()
    {
        Debug.Log("Attack");
    }
}
