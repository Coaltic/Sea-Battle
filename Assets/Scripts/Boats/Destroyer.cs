using UnityEngine;

public class Destroyer : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 40;
        health = 15;
        weaponType = WeaponType.Gun;
        weaponStrength = 9;
        weaponRange = WeaponRange.Moderate;
        moveability = Moveability.Quick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
