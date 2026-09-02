using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class PlayerGameManager : MonoBehaviour
{
    [SerializeField] private int minPlayersToStart = 2;
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
        startRoutine = StartCoroutine(CheckForGameStart());

    }

    IEnumerator CheckForGameStart()
    {

        while (true)
        {
            if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
            {
                InitializeGame();
            }
            
            Debug.Log("Game has not started");
            
            yield return new WaitForSeconds(1f);
        }

    }

    public void InitializeGame()
    {
        StopCoroutine(startRoutine);
        land = GameObject.Find("Land");

        if (rightSpawnLocation == null) rightSpawnLocation = land.transform.GetChild(0).GetChild(0).gameObject;
        if (leftSpawnLocation == null) leftSpawnLocation = land.transform.GetChild(0).GetChild(1).gameObject;

        GameObject fleetMenu = Instantiate(fleetMenuUICanvasPrefab);

        for (int i = 0; i < fleetMenu.transform.GetChild(0).childCount; i++)
        {
            fleetMenu.transform.GetChild(0).GetChild(i).GetComponent<ShipButton>()._playerGameManager = this;
        }

        inGame = true;
        SetUpNewFleet();
    }

    void Update()
    {
       if (inGame) CheckForControlChange();
    }

    public void SetUpNewFleet()
    {
        currentFleet = Instantiate(fleetPrefab).GetComponent<Fleet>();
        currentFleet._playerGameManager = this;
        currentFleet.gameObject.transform.SetParent(land.transform, false);
        currentFleet.transform.position = rightSpawnLocation.transform.position;
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
