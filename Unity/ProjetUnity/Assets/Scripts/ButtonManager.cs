using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject confirmationCanvas;
    public Button yesButton;
    public Button noButton;

    public void Start()
    {
        yesButton.onClick.AddListener(EndTurn);
        noButton.onClick.AddListener(StayTurn);
    }

    public void EndTurn()
    {
        GameManager.instance.EndTurn();
        confirmationCanvas.SetActive(false);
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
