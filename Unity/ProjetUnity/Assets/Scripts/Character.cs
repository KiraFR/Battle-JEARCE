using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject right = GameManager.instance.GetGameObject(((int)transform.position.x) - 1, (int)transform.position.y);
            if (right != null)
            {
                right.GetComponent<SpriteRenderer>().color = Color.green;
            }
            GameObject left = GameManager.instance.GetGameObject(((int)transform.position.x) + 1, (int)transform.position.y);
            if (left != null)
            {
                left.GetComponent<SpriteRenderer>().color = Color.green;
            }
            GameObject down = GameManager.instance.GetGameObject((int)transform.position.x, ((int) transform.position.y) - 1 );
            if (down != null)
            {
                down.GetComponent<SpriteRenderer>().color = Color.green;
            }
            GameObject up = GameManager.instance.GetGameObject((int)transform.position.x, ((int) transform.position.y) + 1);
            if (up != null)
            {
                up.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
            
    }

}
