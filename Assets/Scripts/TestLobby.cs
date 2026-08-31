using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Matchmaker;
using Unity.Services.Multiplayer;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;


public class TestLobby : MonoBehaviour
{
    public string[] wordList = { "Apple", "Banana", "Cherry", "Dragonfruit" };

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Signed in: {AuthenticationService.Instance.PlayerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateLobby()
    {
        try
        {
            string lobbyName = GetRandomWord();
            int maxPlayers = Random.Range(1, 9);
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            Debug.Log($"Created Lobby: {lobbyName} with {maxPlayers} players");
        } catch (LobbyServiceException e)
        {
            Debug.Log($"CATCH: {e}");
        }
    }

    public async void ListLobbies()
    {
        try
        {
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();

            Debug.Log($"Lobbies Found: {queryResponse.Results.Count}");
            int i = 1;
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log($"Lobby #{i}: {lobby.Name}, Max Players: {lobby.MaxPlayers}");
                i++;
            }

        } catch (LobbyServiceException e)
        {
            Debug.Log($"CATCH: {e}");
        }
    }


    public string GetRandomWord()
    {
        if (wordList.Length == 0) return string.Empty;

        // Automatically handles picking a valid random index
        int randomIndex = Random.Range(0, wordList.Length);
        return wordList[randomIndex];
    }
}
