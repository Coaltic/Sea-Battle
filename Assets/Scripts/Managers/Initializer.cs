using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class Initializer : MonoBehaviour
{
    [SerializeField] private int minPlayersToStart = 2;
    public GameObject gameManagerPrefab;
    Coroutine startRoutine = null;

    void Start()
    {
        startRoutine = StartCoroutine(CheckForGameStart());
    }

    IEnumerator CheckForGameStart()
    {
        while (true)
        {
            if (NetworkManager.Singleton.ConnectedClients.Count == minPlayersToStart)
            {
                if (NetworkManager.Singleton.IsHost)
                {
                    Debug.Log("You are Host");
                    Instantiate(gameManagerPrefab);
                }
                else
                {
                    Debug.Log("You are not Host");
                    Instantiate(gameManagerPrefab);
                }

                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
