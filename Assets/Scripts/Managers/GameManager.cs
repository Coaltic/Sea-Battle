using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }


    public GameObject playerGameManagerPrefab;
    public PlayerGameManager player1;
    public PlayerGameManager player2;
    // public PlayerGameManager playerGameManager;
    public GameObject fleetPrefab;
    public GameObject fleetMenuUICanvasPrefab;
    public GameObject rightSpawnLocation;
    public GameObject leftSpawnLocation;
    public GameObject land;

    public Fleet currentFleet;
    public List<Fleet> activeFleetsList;

    public bool inGame;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (NetworkManager.Singleton.IsHost) InitializeGame();
    }

    public void InitializeGame()
    {
        

        land = GameObject.Find("Land");
        if (rightSpawnLocation == null) rightSpawnLocation = land.transform.GetChild(0).GetChild(0).gameObject;
        if (leftSpawnLocation == null) leftSpawnLocation = land.transform.GetChild(0).GetChild(1).gameObject;

        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.ClientId == 0) SetUpPlayer1Manager(client.ClientId, rightSpawnLocation);
            if (client.ClientId == 1) SetUpPlayer2Manager(client.ClientId, leftSpawnLocation);
        }

        // player1 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
        // NetworkObject netObj = player1.GetComponent<NetworkObject>();
        // netObj.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
        // Debug.Log($"Your client ID is: {NetworkManager.Singleton.LocalClientId} and the owner of playerGameManager is {playerGameManager.OwnerClientId}");

    }

    public void SetUpPlayer1Manager(ulong clientID, GameObject spawnLocation)
    {
        player1 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
        NetworkObject netObj = player1.GetComponent<NetworkObject>();
        
        player1._gameManager = Instance;
        player1.mySpawnLocation = spawnLocation;
        player1.land = land;
        netObj.SpawnAsPlayerObject(clientID);
    }
    public void SetUpPlayer2Manager(ulong clientID, GameObject spawnLocation)
    {
        player2 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
        NetworkObject netObj = player2.GetComponent<NetworkObject>();
        
        player2._gameManager = Instance;
        player2.mySpawnLocation = spawnLocation;
        player2.land = land;
        netObj.SpawnAsPlayerObject(clientID);
    }


    public void OldInitializeGame()
    {
        Debug.Log($"Current connected players: {NetworkManager.Singleton.ConnectedClients.Count}");
        land = GameObject.Find("Land");
        if (rightSpawnLocation == null) rightSpawnLocation = land.transform.GetChild(0).GetChild(0).gameObject;
        if (leftSpawnLocation == null) leftSpawnLocation = land.transform.GetChild(0).GetChild(1).gameObject;

        if (NetworkManager.Singleton.IsHost == true)
        {
            player1 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
            player1.gameObject.tag = "Player 1";
            // player1.mySpawnLocation = rightSpawnLocation;
            player1.land = land;
            player1._gameManager = this;
            // Debug.Log("You are Host");
            NetworkObject netObj = player1.GetComponent<NetworkObject>();
            ulong player1ClientID = NetworkManager.Singleton.LocalClientId;
            netObj.SpawnWithOwnership(player1ClientID);
            Debug.Log($"You are the Host and your ID is {player1ClientID}");
            Debug.Log($"The owner of player1 is {player1.OwnerClientId}");

            GameObject fleetMenu = Instantiate(fleetMenuUICanvasPrefab);

            for (int i = 0; i < fleetMenu.transform.GetChild(0).childCount; i++)
            {
                fleetMenu.transform.GetChild(0).GetChild(i).GetComponent<ShipButton>()._playerGameManager = player1;
            }

        }
        else
        {
            player2 = Instantiate(playerGameManagerPrefab).GetComponent<PlayerGameManager>();
            player2.gameObject.tag = "Player 2";
            // player2.mySpawnLocation = leftSpawnLocation;
            player2.land = land;
            player2._gameManager = this;
            NetworkObject netObj = player2.GetComponent<NetworkObject>();

            ulong player2ClientID = NetworkManager.Singleton.LocalClientId;
            netObj.SpawnWithOwnership(player2ClientID);

            Debug.Log($"You are not Host and your ID is {player2ClientID}");
            Debug.Log($"The owner of player2 is {player2.OwnerClientId}");

            GameObject fleetMenu = Instantiate(fleetMenuUICanvasPrefab);

            for (int i = 0; i < fleetMenu.transform.GetChild(0).childCount; i++)
            {
                fleetMenu.transform.GetChild(0).GetChild(i).GetComponent<ShipButton>()._playerGameManager = player2;
            }

            Destroy(this.gameObject);
        }
    }

    /*void Update()
    {
        if (inGame) CheckForControlChange();
    }*/

    /*public void SetUpNewFleet(PlayerGameManager player)
    {
        if (!IsHost) return;

        currentFleet = Instantiate(fleetPrefab).GetComponent<Fleet>();
        NetworkObject netObject = currentFleet.GetComponent<NetworkObject>();
        netObject.Spawn();

        currentFleet._playerGameManager = player;
        
        currentFleet.gameObject.transform.SetParent(land.transform, false);
        currentFleet.transform.position = player.mySpawnLocation.transform.position;
    }*/

    public void AddToCurrentFleet(GameObject boatPrefab)
    {
        // currentFleet.AddBoatToCurrentFleet(boatPrefab);
    }

    /*public void CheckForControlChange()
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
    }*/

    public void ClearFleetControl()
    {
        for (int i = 0; i < activeFleetsList.Count; i++)
        {
            activeFleetsList[i].GetComponent<FleetMovement>().inControl = false;
        }
    }
}
