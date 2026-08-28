using UnityEngine;

public class Submarine : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 35;
        health = 15;
        weaponType = WeaponType.Tordedo;
        weaponStrength = 20;
        weaponRange = WeaponRange.VeryLong;
        moveability = Moveability.Quick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
