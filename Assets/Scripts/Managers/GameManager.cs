using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int minPlayersToStart = 2;
    public GameObject playerGameManagerPrefab;
    public PlayerGameManager player1;
    public PlayerGameManager player2;
    public GameObject fleetPrefab;
    public GameObject fleetMenuUICanvasPrefab;
    public GameObject rightSpawnLocation;
    public GameObject leftSpawnLocation;
    public GameObject land;

    public Fleet currentFleet;
    public List<Fleet> activeFleetsList;

    Coroutine startRoutine = null;
    public bool inGame;

    void Start()
    {
        InitializeGame();

    }

    public void InitializeGame()
    {
        Debug.Log($"Current connected players: {NetworkManager.Singleton.ConnectedClients.Count}");
        land = GameObject.Find("Land");
        if (rightSpawnLocation == null) rightSpawnLocation = land.transform.GetChild(0).GetChild(0).gameObject;
        if (leftSpawnLocation == null) leftSpawnLocation = land.transform.GetChild(0).GetChild(1).gameObject;

        if (NetworkManager.Singleton.IsHost == true)
        {
            player1 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
            player1.gameObject.tag = "Player 1";
            player1.mySpawnLocation = rightSpawnLocation;
            Debug.Log("You are Host");
        }
        else
        {
            player2 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
            player2.gameObject.tag = "Player 2";
            player2.mySpawnLocation = leftSpawnLocation;
            Debug.Log("You are not Host");
            Destroy(this.gameObject);
        }




        // GameObject fleetMenu = Instantiate(fleetMenuUICanvasPrefab);

        /*for (int i = 0; i < fleetMenu.transform.GetChild(0).childCount; i++)
        {
            fleetMenu.transform.GetChild(0).GetChild(i).GetComponent<ShipButton>()._playerGameManager = this;
        }*/

        // inGame = true;
        // SetUpNewFleet();
    }

    /*void Update()
    {
        if (inGame) CheckForControlChange();
    }*/

    /*public void SetUpNewFleet()
    {
        currentFleet = Instantiate(fleetPrefab).GetComponent<Fleet>();
        currentFleet._playerGameManager = this;
        NetworkObject netObject = currentFleet.GetComponent<NetworkObject>();
        netObject.Spawn();
        currentFleet.gameObject.transform.SetParent(land.transform, false);
        currentFleet.transform.position = mySpawnLocation.transform.position;
    }*/

    public void AddToCurrentFleet(GameObject boatPrefab)
    {
        currentFleet.AddBoatToCurrentFleet(boatPrefab);
    }

    public void CheckForControlChange()
    {
        if (Input.anyKeyDown)
        {
            string input = Input.inputString;

            if (!string.IsNullOrEmpty(input) && char.IsDigit(input[0]))
            {
                int.TryParse(input, out int result);
                if ((result - 1) < activeFleetsList.Count)
                {
                    ClearFleetControl();
                    activeFleetsList[result - 1].GetComponent<FleetMovement>().inControl = true;
                }
            }
        }
    }

    public void ClearFleetControl()
    {
        for (int i = 0; i < activeFleetsList.Count; i++)
        {
            activeFleetsList[i].GetComponent<FleetMovement>().inControl = false;
        }
    }
}
