using UnityEngine;
using UnityEngine.UI;

public class TwoShipButton : ShipButton
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxNumOfPresses = 2;
        remainingPresses = maxNumOfPresses;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
