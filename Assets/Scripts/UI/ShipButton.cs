using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipButton : MonoBehaviour
{
    public GameManager _gameManager;

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
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }
    public void OnLeftClick()
    {
        if (remainingPresses > 0 && _gameManager.currentFleet.fleetBoats.Count < _gameManager.currentFleet.maxAmountOfBoats)
        {
            _gameManager.AddToCurrentFleet(thisBoatPrefab);
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
        foreach (Boat boat in _gameManager.currentFleet.fleetBoats)
        {
            Debug.Log(thisBoatPrefab.name);
            Debug.Log(boat.name);

            if (boat.name == thisBoatPrefab.name)
            {
                Destroy(boat.gameObject);
                _gameManager.currentFleet.fleetBoats.Remove(boat);
                _gameManager.currentFleet.ReOrderFleet();
                remainingPresses++;

                break;
            }
        }

        UpdateSprite();
    }

    public void UpdateSprite()
    {
        buttonImage.sprite = spritesArray[remainingPresses];
        // if (remainingPresses <= 0 && settingUpFleet)
        // if (remainingPresses <= 0) this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void OnClick()
    {
        if (remainingPresses > 0)
        {
            _gameManager.AddToCurrentFleet(thisBoatPrefab);
            remainingPresses--;
        }

        UpdateSprite();
    }
}
