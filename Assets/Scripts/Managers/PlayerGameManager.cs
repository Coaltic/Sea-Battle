using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class PlayerGameManager : NetworkBehaviour
{
    public GameManager _gameManager;
    public GameObject fleetPrefab;
    public GameObject mySpawnLocation;
    public GameObject land;

    public PlayerGameManager player1;
    public PlayerGameManager player2;

    public Fleet currentFleet;
    public List<Fleet> activeFleetsList;

    // Coroutine startRoutine = null;
    public bool inGame;

    void Start()
    {
        if (this.OwnerClientId == NetworkManager.Singleton.LocalClientId && NetworkManager.Singleton.IsHost)
        {
            this.gameObject.tag = "Player 1";
            player1 = this;
        }

        else if (this.OwnerClientId == NetworkManager.Singleton.LocalClientId && NetworkManager.Singleton.IsClient)
        {
            this.gameObject.tag = "Player 2";
            player2 = this;
        }

        else if (this.OwnerClientId != NetworkManager.Singleton.LocalClientId && NetworkManager.Singleton.IsClient)
        {
            this.gameObject.tag = "Player 1";
            player1 = this;
        }
        else if (this.OwnerClientId != NetworkManager.Singleton.LocalClientId && NetworkManager.Singleton.IsHost)
        {
            this.gameObject.tag = "Player 2";
            player2 = this;
        }

        if (_gameManager == null) _gameManager = GameObject.Find("Game Manager(Clone)").GetComponent<GameManager>();

        if (IsOwner) InitializeGame();
    }

    public void InitializeGame()
    {
        SetUpNewFleet();
    }

    void Update()
    {
       if (inGame) CheckForControlChange();
    }

    public void SetUpNewFleet()
    {
        // Debug.Log("Running PGM SetUpNewFleet");

        if (NetworkManager.Singleton.IsHost == true)
        {
            currentFleet = Instantiate(fleetPrefab.GetComponent<Fleet>(), land.transform, false);
            NetworkObject netObject = currentFleet.GetComponent<NetworkObject>();
            netObject.Spawn();

            currentFleet._playerGameManager = this;

            //currentFleet.gameObject.transform.SetParent(land.transform, false);
            currentFleet.transform.position = this.mySpawnLocation.transform.position;
            Debug.Log("Set up new fleet as host");
        }
        else
        {
            RequestSetUpNewFleetServerRpc();
        }

    }

    [ServerRpc]
    private void RequestSetUpNewFleetServerRpc()
    {
        /*currentFleet = Instantiate(fleetPrefab.GetComponent<Fleet>(), land.transform, false);
        currentFleet.GetComponent<NetworkObject>().Spawn();
        currentFleet._playerGameManager = this;
        currentFleet.transform.position = spawnLocation.transform.position;
        Debug.Log("Set up new fleet as client");*/

    }

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
