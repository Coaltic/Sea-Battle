using UnityEngine;

public class Battleship : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 35;
        health = 30;
        weaponType = WeaponType.Gun;
        weaponStrength = 12;
        weaponRange = WeaponRange.Long;
        moveability = Moveability.Average;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
