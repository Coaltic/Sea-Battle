using UnityEngine;

public class MineSweeper : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 30;
        health = 20;
        weaponType = WeaponType.Gun;
        weaponStrength = 6;
        weaponRange = WeaponRange.VeryShort;
        moveability = Moveability.Average;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
