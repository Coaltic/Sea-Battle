using UnityEngine;
using System.Collections;

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
        // StartCoroutine(DebugText());

    }

    // Update is called once per frame
    void Update()
    {


        
    }

    IEnumerator DebugText()
    {
        
        while (true)
        {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Debug.Log($"Horizontal: {moveHorizontal}, Vertical: {moveVertical}");

            yield return new WaitForSeconds(0.25f);
        }
        
    }

    private void FixedUpdate()
    {

    }
}
