using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //public Items[] items;
    public Stats healthPoint;
    public Stats attackPoint;
    public Stats movePoint;

    public Equipement equipement;

    public Stats minDistAttack;
    public Stats maxDistAttack;

    public Text healthText;
    public Text moveText;
    private Rigidbody2D rb2D;
    public float moveTime = .1f;

    private float inverseMoveTime;

    protected virtual void Start()
    {
        inverseMoveTime = 1f / moveTime;
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void GetAttacked(int loss)
    {
        healthPoint.DecreaseCurrent(loss);

        ChangeHealth();
    }
    
    public int GethealthPointwithEquipement()
    {
        if (equipement!=null)
            return equipement.healthPoint.currentStat + healthPoint.currentStat;
        else
            return healthPoint.currentStat;
    }

    public int GetAttackPointwithEquipement()
    {
        if (equipement!=null)
            return equipement.attackPoint.currentStat + attackPoint.currentStat;
        else
            return attackPoint.currentStat;
    }
    public int GetMovePointwithEquipement()
    {
        if (equipement != null)
            return equipement.movePoint.currentStat + movePoint.currentStat;
        else
            return movePoint.currentStat;
    }



    public void ChangeHealth()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "HP : " + healthPoint.currentStat;
    }

    public void Move(int used)
    {
        movePoint.DecreaseCurrent(used);

        ChangeMove();
    }

    public void ChangeMove()
    {
        moveText = GameObject.Find("MoveText").GetComponent<Text>();
        moveText.text = "Mouvement : " + movePoint.currentStat;
    }

    public void RoundEnded()
    {
        movePoint.ResetCurrent();
    }

    public void Move(Vector3 end)
    {
        StartCoroutine(SmoothMovement(end));
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }
}
