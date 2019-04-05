using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private GameObject turnCanvas;
    private GameObject confirmationCanvas;
    private Button yesButton;
    private Button noButton;
    private Button EndTurnBtn;
    public void Start()
    {

        turnCanvas = GameObject.Find("CanvasTurn");
        confirmationCanvas = GameObject.Find("CanvasConfirm");
        EndTurnBtn = GameObject.Find("CanvasBottom").transform.Find("EndTurn").GetComponent<Button>();
        yesButton = confirmationCanvas.transform.Find("YesButton").GetComponent<Button>();
        noButton = confirmationCanvas.transform.Find("NoButton").GetComponent<Button>();

        yesButton.onClick.AddListener(EndTurn);
        noButton.onClick.AddListener(StayTurn);
        EndTurnBtn.onClick.AddListener(Ready);

        confirmationCanvas.SetActive(false);
        turnCanvas.SetActive(false);
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

    public void Ready()
    {
        GameManager.instance.StartGame();
        /*
         * 
         * Choses à faire entre les deux => Multijoueur
         * 
         * */

        StartCoroutine("TurnStart");

        GameObject.Find("EndTurn").GetComponent<Button>().onClick.AddListener(EndTurn);
        GameObject.Find("EndTurn").transform.Find("Text").GetComponent<Text>().text = "Fin de tour";
        GameManager.instance.ResetStats();
    }
}
