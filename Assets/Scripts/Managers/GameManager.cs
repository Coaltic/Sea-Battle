using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnLocations
    {
        public GameObject[] locations;
    }

    public GameObject fleetPrefab;
    public GameObject[] boatsCatalogue;
    public SpawnLocations[] spawnLocations;

    void Start()
    {
        spawnLocations = new SpawnLocations[2];
        spawnLocations[0].locations = new GameObject[4];

        spawnLocations[0].locations[0] = GameObject.Find("Land/Spawn Locations/Right Spawn");
        spawnLocations[0].locations[1] = spawnLocations[0].locations[0].transform.GetChild(0).gameObject;
        spawnLocations[0].locations[2] = spawnLocations[0].locations[0].transform.GetChild(1).gameObject;
        spawnLocations[0].locations[3] = spawnLocations[0].locations[0].transform.GetChild(2).gameObject;

        GameObject fleet = Instantiate(fleetPrefab);
        fleet.transform.SetParent(GameObject.Find("Land").transform, false);
        fleet.transform.position = spawnLocations[0].locations[0].transform.position;

        /*fleet.physicalObject = new GameObject();
        fleet.physicalObject.transform.SetParent(GameObject.Find("Land").transform);
        fleet.physicalObject.transform.position = spawnLocations[0].locations[0].transform.position;*/


        GameObject boat = Instantiate(boatsCatalogue[0]);
        boat.transform.SetParent(fleet.transform.GetChild(0), false);

        GameObject boat2 = Instantiate(boatsCatalogue[0]);
        boat2.transform.SetParent(fleet.transform.GetChild(1), false);

        fleet.GetComponent<FleetMovement>().InitializeFleet();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
