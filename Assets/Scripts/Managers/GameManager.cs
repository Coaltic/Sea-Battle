using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [System.Serializable]
    /*public struct SpawnLocations
    {
        public GameObject spawnLocationsObject;
        public GameObject[] locations;
    }*/

    public GameObject fleetPrefab;
    // public GameObject[] boatsCatalogue;
    public GameObject rightSpawnLocation;
    public GameObject leftSpawnLocation;

    public Fleet currentFleet;

    // public int shipsInFleet;
    // public int maxShipsInFleet = 3;

    void Start()
    {
        // rightSpawnLocation.locations = new GameObject[3];

        if (rightSpawnLocation == null) rightSpawnLocation = GameObject.Find("Land/Spawn Locations/Right Spawn");
        // rightSpawnLocation.locations[0] = rightSpawnLocation.spawnLocationsObject.transform.GetChild(0).gameObject;
        // rightSpawnLocation.locations[1] = rightSpawnLocation.spawnLocationsObject.transform.GetChild(1).gameObject;
        // rightSpawnLocation.locations[2] = rightSpawnLocation.spawnLocationsObject.transform.GetChild(2).gameObject;


        SetUpNewFleet();

        /*fleet.physicalObject = new GameObject();
        fleet.physicalObject.transform.SetParent(GameObject.Find("Land").transform);
        fleet.physicalObject.transform.position = spawnLocations[0].locations[0].transform.position;*/


        /*GameObject boat = Instantiate(boatsCatalogue[0]);
        boat.transform.SetParent(fleet.transform.GetChild(0), false);

        GameObject boat2 = Instantiate(boatsCatalogue[0]);
        boat2.transform.SetParent(fleet.transform.GetChild(1), false);

        fleet.GetComponent<FleetMovement>().InitializeFleet();*/

    }

    // Update is called once per frame
    void Update()
    {
        // if (currentFleet.GetComponent<FleetMovement>().setUp == true) SetUpNewFleet();
    }

    public void SetUpNewFleet()
    {
        currentFleet = Instantiate(fleetPrefab).GetComponent<Fleet>();
        currentFleet.gameObject.transform.SetParent(GameObject.Find("Land").transform, false);
        currentFleet.transform.position = rightSpawnLocation.transform.position;
    }

    public void AddToCurrentFleet(GameObject boat)
    {
        currentFleet.AddBoatToCurrentFleet(boat);
    }

    public void SetUpPlayer2()
    {

    }

}
