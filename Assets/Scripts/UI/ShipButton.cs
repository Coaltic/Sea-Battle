using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipButton : MonoBehaviour
{
    public PlayerGameManager _playerGameManager;

    public GameObject thisBoatPrefab;
    public Image buttonImage;
    public Sprite[] spritesArray;
    public int maxNumOfPresses;
    public int remainingPresses;

    // public bool settingUpFleet;

    void Awake()
    {
        maxNumOfPresses = spritesArray.Length - 1;
        remainingPresses = maxNumOfPresses;
        buttonImage = this.gameObject.GetComponent<Image>();
        // _playerGameManager = GameObject.Find("Game Manager").GetComponent<PlayerGameManager>();

    }
    public void OnLeftClick()
    {
        if (remainingPresses > 0 && _playerGameManager.currentFleet.fleetBoats.Count < _playerGameManager.currentFleet.maxAmountOfBoats)
        {
            _playerGameManager.AddToCurrentFleet(thisBoatPrefab);
            remainingPresses--;
        }

        UpdateSprite();
    }

    public void OnMiddleClick()
    {
        Debug.Log("Middle Button Clicked");
    }

    public void OnRightClick()
    {
        foreach (Boat boat in _playerGameManager.currentFleet.fleetBoats)
        {
            Debug.Log(thisBoatPrefab.name);
            Debug.Log(boat.name);

            if (boat.name == thisBoatPrefab.name)
            {
                Destroy(boat.gameObject);
                _playerGameManager.currentFleet.fleetBoats.Remove(boat);
                _playerGameManager.currentFleet.ReOrderFleet();
                remainingPresses++;

                break;
            }
        }

        UpdateSprite();
    }

    public void UpdateSprite()
    {
        buttonImage.sprite = spritesArray[remainingPresses];
    }

    public void OnClick()
    {
        if (remainingPresses > 0)
        {
            _playerGameManager.AddToCurrentFleet(thisBoatPrefab);
            remainingPresses--;
        }

        UpdateSprite();
    }
}
