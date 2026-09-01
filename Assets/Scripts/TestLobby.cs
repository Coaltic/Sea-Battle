using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Matchmaker;
using Unity.Services.Multiplayer;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using System;


public class TestLobby : MonoBehaviour
{
    private string[] wordList = { "Apple", "Banana", "Cherry", "Dragonfruit", "Pear", "Kiwi", "Orange", "Strawberry", "Grape", "Mango", "Lemon", "Lime", "Peach"};
    public GameObject mainMenuComponents;
    public GameObject lobbiesMenu;
    public GameObject lobbyPanelPrefab;
    public GameObject lobbyPanelContainer;
    public GameObject inLobbyMenu;

    private Lobby hostLobby;
    private float heartbeatTimer;
    public float lobbyPanelPositionOffset;

    public bool firstLoadDone;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Signed in: {AuthenticationService.Instance.PlayerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
    }

    private async void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f)
            {
                float heartbeatTimerMax = 15f;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }

    }

    public async void CreateLobby()
    {
        try
        {
            string lobbyName = GetRandomWord();
            int maxPlayers = UnityEngine.Random.Range(2, 9);
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            Debug.Log($"Created Lobby: {lobbyName} with {maxPlayers} players");

            hostLobby = lobby;

        } catch (LobbyServiceException e)
        {
            Debug.Log($"CATCH: {e}");
        }
    }

    public async void ListLobbies()
    {
        try
        {
            mainMenuComponents.SetActive(false);
            lobbiesMenu.SetActive(true);

            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions {
                Count = 25,
                Filters = new List<QueryFilter> {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder> {
                    new QueryOrder(true, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync(queryLobbiesOptions);
            LoadLobbiesInMenu(queryResponse);


            Canvas.ForceUpdateCanvases();

        } catch (LobbyServiceException e)
        {
            Debug.Log($"CATCH: {e}");
        }
    }

    public async void JoinLobby(LobbyPanel lobbyP)
    {
        try
        {
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();

            await LobbyService.Instance.JoinLobbyByIdAsync(lobbyP.lobbyIDNumber);


        }
        catch (LobbyServiceException e)
        {
            Debug.Log($"CATCH: {e}");
        }
    }

    public void LoadLobbiesInMenu(QueryResponse queryResponse)
    {
        int i = 0;
        lobbyPanelPositionOffset = 0f;

        foreach (Lobby lobby in queryResponse.Results)
        {
            LobbyPanel lobbyPanel = Instantiate(lobbyPanelPrefab).GetComponent<LobbyPanel>();
            lobbyPanel.testLobby = this;
            // lobbyPanel.rectTransform = lobbyPanel.gameObject.GetComponent<RectTransform>();
            lobbyPanelContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(lobbyPanelContainer.GetComponent<RectTransform>().sizeDelta.x, lobbyPanelContainer.GetComponent<RectTransform>().sizeDelta.y + 150);
            lobbyPanel.gameObject.transform.SetParent(lobbyPanelContainer.transform, false);
            lobbyPanel.rectTransform = lobbyPanel.gameObject.GetComponent<RectTransform>();

            Vector2 targetPosition = new Vector2(lobbyPanel.gameObject.transform.localPosition.x, lobbyPanel.gameObject.transform.localPosition.y - lobbyPanelPositionOffset);
            lobbyPanel.gameObject.transform.localPosition = targetPosition;
            lobbyPanel.lobbyIDNumber = lobby.Id;
            lobbyPanel.lobbyNameText.text = lobby.Name;
            lobbyPanel.lobbyPlayerAmountText.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}";
            string timeText = (DateTime.UtcNow - lobby.Created).ToString(@"mm\:ss");
            lobbyPanel.lobbyCreationLengthText.text = $"Open for: {timeText}";
            lobbyPanelPositionOffset += lobbyPanel.rectTransform.rect.height;

            i++;
        }
    }

    public void OnClickBack()
    {
        for (int i = lobbyPanelContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(lobbyPanelContainer.transform.GetChild(i).gameObject);
        }

        lobbyPanelContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(lobbyPanelContainer.GetComponent<RectTransform>().sizeDelta.x, 0);
        lobbiesMenu.SetActive(false);
        mainMenuComponents.SetActive(true);
    }

    public string GetRandomWord()
    {
        if (wordList.Length == 0) return string.Empty;

        // Automatically handles picking a valid random index
        int randomIndex = UnityEngine.Random.Range(0, wordList.Length);
        return wordList[randomIndex];
    }

}
