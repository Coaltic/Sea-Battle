using UnityEngine;
using Unity.Netcode;
using System;
using System.Collections.Generic;

public class Fleet : MonoBehaviour
{
    public List<Boat> fleetBoats;
    public GameObject[] fleetBoatLocationObjects;
    // public GameObject shipButtonsPanel;
    public GameObject[] shipButtons;
    public bool alreadyLaunched;

    public int currentNumOfBoats;
    public int maxAmountOfBoats = 3;

    public PlayerGameManager _playerGameManager;

    void Awake()
    {
        fleetBoatLocationObjects = new GameObject[maxAmountOfBoats];
        fleetBoatLocationObjects[0] = this.gameObject.transform.GetChild(0).gameObject;
        fleetBoatLocationObjects[1] = this.gameObject.transform.GetChild(1).gameObject;
        fleetBoatLocationObjects[2] = this.gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!IsOwner) return;
        currentNumOfBoats = fleetBoats.Count;
        if (!alreadyLaunched) CheckForLaunchButton();
    }

    public void AddBoatToCurrentFleet(GameObject Prefab)
    {
        if (fleetBoats.Count < maxAmountOfBoats)
        {
            GameObject newBoat = Instantiate(Prefab);
            newBoat.name = newBoat.name.Replace("(Clone)", "");
            newBoat.transform.SetParent(fleetBoatLocationObjects[currentNumOfBoats].transform, false);
            fleetBoats.Add(newBoat.GetComponent<Boat>());
        }

    }

    public void ReOrderFleet()
    {
        foreach (Boat boat in fleetBoats)
        {
            boat.transform.SetParent(fleetBoatLocationObjects[fleetBoats.IndexOf(boat)].transform);
            boat.transform.localPosition = Vector2.zero;
        }
    }

    public void CheckForLaunchButton()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            this.GetComponent<FleetMovement>().InitializeFleet(fleetBoats);

            foreach (GameObject button in shipButtons)
            {
                if (button.GetComponent<ShipButton>().remainingPresses <= 0) button.GetComponent<ShipButtonSelectable>().interactable = false;
            }

            _playerGameManager.ClearFleetControl();
            _playerGameManager.activeFleetsList.Add(this);
            alreadyLaunched = true;
        }
    }
}
