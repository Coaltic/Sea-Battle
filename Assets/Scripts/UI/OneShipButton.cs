using UnityEngine;
using UnityEngine.UI;

public class OneShipButton : ShipButton
{

    void Start()
    {
        maxNumOfPresses = 1;
        remainingPresses = maxNumOfPresses;
    }
}
