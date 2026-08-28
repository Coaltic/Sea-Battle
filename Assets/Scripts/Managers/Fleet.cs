using UnityEngine;
using System;
using System.Collections.Generic;

public class Fleet : MonoBehaviour
{
    public List<Boat> fleetBoats;
    public GameObject[] fleetBoatLocationObjects;
    public GameObject shipButtonsPanel;
    public GameObject[] shipButtons;
    public bool alreadyLaunched;

    public int currentNumOfBoats;
    public int maxAmountOfBoats = 3;

    public GameManager _gameManager;

    void Start()
    {
        fleetBoatLocationObjects = new GameObject[maxAmountOfBoats];
        fleetBoatLocationObjects[0] = this.gameObject.transform.GetChild(0).gameObject;
        fleetBoatLocationObjects[1] = this.gameObject.transform.GetChild(1).gameObject;
        fleetBoatLocationObjects[2] = this.gameObject.transform.GetChild(2).gameObject;

        shipButtonsPanel = GameObject.Find("UI Canvas/Bottom Panel");
        shipButtons = new GameObject[shipButtonsPanel.transform.childCount];

        for (int i = 0; i < shipButtons.Length; i++)
        {
            shipButtons[i] = shipButtonsPanel.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

            _gameManager.ClearFleetControl();
            _gameManager.activeFleetsList.Add(this);
            alreadyLaunched = true;
        }
    }
}
