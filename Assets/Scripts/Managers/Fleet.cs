using UnityEngine;
using System;
using System.Collections.Generic;

public class Fleet : MonoBehaviour
{
    public List<Boat> fleetBoats;
    public GameObject[] fleetBoatObjects;

    public int currentNumOfBoats;
    public int maxAmountOfBoats = 3;

    void Start()
    {
        fleetBoatObjects = new GameObject[maxAmountOfBoats];
        fleetBoatObjects[0] = this.gameObject.transform.GetChild(0).gameObject;
        fleetBoatObjects[1] = this.gameObject.transform.GetChild(1).gameObject;
        fleetBoatObjects[2] = this.gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForLaunchButton();
    }

    public void AddBoatToCurrentFleet(GameObject boat)
    {
        if (fleetBoats.Count <= maxAmountOfBoats)
        {
            GameObject newBoat = Instantiate(boat);
            newBoat.transform.SetParent(fleetBoatObjects[currentNumOfBoats].transform, false);
            fleetBoats.Add(newBoat.GetComponent<Boat>());
            currentNumOfBoats++;
        }

    }

    public void CheckForLaunchButton()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            this.GetComponent<FleetMovement>().InitializeFleet(fleetBoats);
            
        }
    }
}
