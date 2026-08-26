using UnityEngine;

public class FleetMovement : MonoBehaviour
{
    public int speed;
    public Rigidbody2D rb2d;
    public GameObject[] boats;
    public bool setUp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (setUp) Movement();
    }

    public void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb2d.linearVelocity = new Vector2((moveHorizontal * speed) * Time.deltaTime, (moveVertical * speed) * Time.deltaTime);
    }

    public void InitializeFleet()
    {
        rb2d = this.GetComponent<Rigidbody2D>();

        boats = new GameObject[3];
        if (this.gameObject.transform.GetChild(0).GetChild(0).gameObject != null) boats[0] = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;

        if (this.gameObject.transform.GetChild(1).childCount == 0)
        {
            Debug.Log("No Second Boat");
        }
        else boats[1] = this.gameObject.transform.GetChild(1).GetChild(0).gameObject;

        if (this.gameObject.transform.GetChild(2).childCount == 0)
        {
            Debug.Log("No Third Boat");
        }
        else boats[2] = this.gameObject.transform.GetChild(2).GetChild(0).gameObject;

        Debug.Log($"Firt Boats Speed: {boats[0].GetComponent<Boat>().speed}");
        speed = boats[0].GetComponent<Boat>().speed;
        Debug.Log($"Set speed to: {speed}");

        foreach (GameObject fleetBoat in boats)
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

        Debug.Log($"Firt Boats Speed: {boats[0].GetComponent<Boat>().speed}");
        setUp = true;
    }
}
