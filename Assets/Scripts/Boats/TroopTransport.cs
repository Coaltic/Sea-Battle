using UnityEngine;

public class TroopTransport : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 30;
        health = 25;
        weaponType = WeaponType.Gun;
        weaponStrength = 7;
        weaponRange = WeaponRange.VeryShort;
        moveability = Moveability.Sluggish;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
