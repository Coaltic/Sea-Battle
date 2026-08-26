using UnityEngine;

public class AircraftCarrier : Boat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        speed = 30;
        health = 25;
        weaponType = WeaponType.Gun;
        weaponStrength = 10;
        weaponRange = WeaponRange.Long;
        moveability = Moveability.Slow;

        // rb2d = this.gameObject.GetComponentInChildren<Rigidbody2D>();

        // FleetMovement fleetMovement = this.gameObject.transform.parent.parent.GetComponent<FleetMovement>();
        // fleetMovement.InitializeFleet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Movement();
    }
}
