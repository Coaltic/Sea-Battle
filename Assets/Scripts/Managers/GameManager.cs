using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private int minPlayersToStart = 2;
    public GameObject fleetPrefab;
    public GameObject rightSpawnLocation;
    public GameObject leftSpawnLocation;

    public Fleet currentFleet;
    public List<Fleet> activeFleetsList;

    public bool inGame;

    void Start()
    {
        
    }

    public void InitializeGame()
    {
        if (rightSpawnLocation == null) rightSpawnLocation = GameObject.Find("Land/Spawn Locations/Right Spawn");
        if (leftSpawnLocation == null) rightSpawnLocation = GameObject.Find("Land/Spawn Locations/Left Spawn");

        SetUpNewFleet();
    }

    void Update()
    {
       if (inGame) CheckForControlChange();
    }

    public void SetUpNewFleet()
    {
        currentFleet = Instantiate(fleetPrefab).GetComponent<Fleet>();
        currentFleet._gameManager = this;
        currentFleet.gameObject.transform.SetParent(GameObject.Find("Land").transform, false);
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
