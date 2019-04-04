using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject turnCanvas;
    public GameObject confirmationCanvas;
    public Button yesButton;
    public Button noButton;

    public void Start()
    {
        yesButton.onClick.AddListener(EndTurn);
        noButton.onClick.AddListener(StayTurn);
        StartCoroutine("TurnStart");
    }

    IEnumerator TurnStart()
    {
        turnCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        turnCanvas.SetActive(false);
    }

    public void EndTurn()
    {
        GameManager.instance.EndTurn();
        confirmationCanvas.SetActive(false);
        StartCoroutine("TurnStart");
    }

    public void StayTurn()
    {
        confirmationCanvas.SetActive(false);
    }

    public void Confirmation()
    {
        confirmationCanvas.SetActive(true);
    }
}
