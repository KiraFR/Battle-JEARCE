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
        EndTurnBtn.onClick.AddListener(ImReady);

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
        confirmationCanvas.SetActive(false);
        GameManager.instance.EndTurn();
        EndTurnBtn.transform.Find("Text").GetComponent<Text>().text = "Son Tour";
        EndTurnBtn.onClick.RemoveAllListeners();
        GameManager.instance.ResetStats();
    }


    public void YourTurn()
    {
        EndTurnBtn.GetComponent<Button>().onClick.AddListener(Confirmation);
        EndTurnBtn.transform.Find("Text").GetComponent<Text>().text = "Fin de tour";
        StartCoroutine("TurnStart");
        GameManager.instance.ResetStats();
    }

    public void StayTurn()
    {
        confirmationCanvas.SetActive(false);
    }

    public void Confirmation()
    {
        confirmationCanvas.SetActive(true);
    }

    private void ImReady()
    {
        GameManager.instance.network.SendString("ready", new List<object>());

        EndTurnBtn.onClick.RemoveAllListeners();
        EndTurnBtn.transform.Find("Text").GetComponent<Text>().text = "En attente";
        GameManager.instance.ResetStats();
        GameManager.instance.ClearMovingTiles();

    }

    public void Ready()
    {
        StartCoroutine("TurnStart");

        EndTurnBtn.onClick.RemoveAllListeners();
        EndTurnBtn.GetComponent<Button>().onClick.AddListener(Confirmation);
        EndTurnBtn.transform.Find("Text").GetComponent<Text>().text = "Fin de tour";
        GameManager.instance.ResetStats();
    }

    public void Surrended()
    {
        turnCanvas.SetActive(true);
        turnCanvas.GetComponentInChildren<Text>().text = "Votre adversaire a abandonné ou a quitté la partie.";
        StartCoroutine("WaitAnLoadScene");
    }

    public void Lost()
    {
        turnCanvas.SetActive(true);
        turnCanvas.GetComponentInChildren<Text>().text = "Vous avez perdu.";
        StartCoroutine("WaitAnLoadScene");
    }

    public void Won()
    {
        turnCanvas.SetActive(true);
        turnCanvas.GetComponentInChildren<Text>().text = "Vous avez gagné !";
        StartCoroutine("WaitAnLoadScene");
    }

    IEnumerator WaitAnLoadScene()
    {
        yield return new WaitForSeconds(2);
        turnCanvas.SetActive(false);
        GameManager.instance.LoadPrecedentScene();
    }
}
