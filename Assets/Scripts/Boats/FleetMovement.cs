using UnityEngine;
using System.Collections.Generic;

public class FleetMovement : MonoBehaviour
{
    public int speed;
    public Rigidbody2D rb2d;
    // public GameObject[] boats;
    public bool setUp;
    public bool inControl;

    public Fleet thisFleet;

    void Start()
    {
        thisFleet = this.GetComponent<Fleet>();

        setUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (setUp && inControl) Movement();
    }

    public void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // rb2d.linearVelocity = new Vector2((moveHorizontal * speed) * Time.deltaTime, (moveVertical * speed) * Time.deltaTime);
        rb2d.AddForce(new Vector2((moveHorizontal * speed) * Time.deltaTime, (moveVertical * speed) * Time.deltaTime));
    }

    public void InitializeFleet(List<Boat> boats)
    {
        rb2d = this.GetComponent<Rigidbody2D>();

        speed = boats[0].speed;
        Debug.Log($"Boat[0] Name: {boats[0].name}, Boat[0] Speed: {speed}");

        foreach (Boat fleetBoat in boats)
        {
            // Debug.Log(fleetBoat.name);
            if (fleetBoat == null)
            {
                Debug.Log("No Boat");
            }
            else if (fleetBoat.GetComponent<Boat>().speed < speed)
            {
                speed = fleetBoat.GetComponent<Boat>().speed;
                Debug.Log($"Set speed to: {speed}");
            }
        }
        setUp = true;
        inControl = true;
        thisFleet._gameManager.SetUpNewFleet();
    }
}
