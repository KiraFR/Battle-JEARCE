using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveAction()
    {
        GameObject unitSquare = GameManager.instance.GetSelectedUnit();
        if (unitSquare != null)
        {
            GameManager.instance.RemoveInstantiatedCanvasAction();
            Square square = unitSquare.GetComponent<Square>();
            square.ChangeSquare((int)square.gameObject.transform.position.x, (int)square.gameObject.transform.position.y);
            Debug.Log("move");
        }
    }
    public void AttackAction()
    {
        Debug.Log("Attack");
    }
}
