using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public GameObject fleetPrefab;
    public GameObject rightSpawnLocation;
    public GameObject leftSpawnLocation;

    public Fleet currentFleet;
    public List<Fleet> activeFleetsList;

    void Start()
    {
        if (rightSpawnLocation == null) rightSpawnLocation = GameObject.Find("Land/Spawn Locations/Right Spawn");
        if (leftSpawnLocation == null) rightSpawnLocation = GameObject.Find("Land/Spawn Locations/Left Spawn");

        SetUpNewFleet();

    }

    // Update is called once per frame
    void Update()
    {
        // if (activeFleetsList.Count > 0) 
            CheckForControlChange();
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
