using UnityEngine;

public class MineLayer : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 20;
        health = 30;
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
