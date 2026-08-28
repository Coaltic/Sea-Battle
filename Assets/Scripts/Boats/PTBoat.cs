using UnityEngine;

public class PTBoat : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 40;
        health = 5;
        weaponType = WeaponType.Tordedo;
        weaponStrength = 12;
        weaponRange = WeaponRange.Short;
        moveability = Moveability.VeryQuick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
