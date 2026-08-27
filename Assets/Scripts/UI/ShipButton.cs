using UnityEngine;
using UnityEngine.UI;

public class ShipButton : MonoBehaviour
{
    public GameManager _gameManager;

    public GameObject thisBoatPrefab;
    public Image buttonImage;
    public Sprite[] spritesArray;
    public int maxNumOfPresses;
    public int remainingPresses;

    public bool settingUpFleet;

    void Awake()
    {
        settingUpFleet = false;
        remainingPresses = maxNumOfPresses;
        buttonImage = this.gameObject.GetComponent<Image>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSprite()
    {
        buttonImage.sprite = spritesArray[remainingPresses];
        // if (remainingPresses <= 0 && settingUpFleet)
        if (remainingPresses <= 0) this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void OnClick()
    {
        if (remainingPresses > 0)
        {
            _gameManager.AddToCurrentFleet(thisBoatPrefab);
            remainingPresses--;
        }

        UpdateSprite();

        /*if (remainingPresses <= 0 && settingUpFleet)
        {
            remainingPresses++;
        }

        UpdateSprite();*/
    }
}
