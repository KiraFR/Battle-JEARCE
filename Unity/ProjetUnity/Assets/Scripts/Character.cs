﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //public Items[] items;
    public Stats healthPoint;
    public Stats attackPoint;
    public Stats movePoint;
    public Stats minDistAttack;
    public Stats maxDistAttack;

    public Equipement equipement;
    public GameObject canvas;
    public Sprite ally;
    public Sprite enemy;

    public float moveTime = .1f;

    private float inverseMoveTime;
    private Rigidbody2D rb2D;
    private Image image;

    void Start()
    {
        inverseMoveTime = 0.8f / moveTime;
        rb2D = GetComponent<Rigidbody2D>();
        
    }
    void Awake()
    {
        GameObject canvasGO = Instantiate(canvas, transform.position, Quaternion.identity);
        canvasGO.transform.SetParent(transform);
        image = canvasGO.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

public void GetAttacked(int loss)
    {
        healthPoint.DecreaseCurrent(loss);

        GameManager.instance.ChangeHealth(healthPoint.currentStat);
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

    public void Move(int used)
    {
        movePoint.DecreaseCurrent(used);

        GameManager.instance.ChangeMove(movePoint.currentStat);
    }

    public void RoundEnded()
    {
        movePoint.ResetCurrent();
    }

    public void Move(List<Vector3> follow)
    {
        foreach (Vector3 pos in follow)
        {
            StartCoroutine(SmoothMovement(pos));
        }
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

    }


    public void SetState (bool isAlly)
    {
        if (isAlly)
        {
            image.sprite = ally;
        }
        else
        {
            image.sprite = enemy;
        }
    }


    public void resetTurn()
    {
        movePoint.ResetCurrent();
    }

}
