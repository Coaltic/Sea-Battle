using UnityEngine;

public class Boat : MonoBehaviour
{
    public int speed;
    public int health;
    public int weaponStrength;
    public WeaponType weaponType;
    public WeaponRange weaponRange;
    public Moveability moveability;

    // public Rigidbody2D rb2d;

    void Start()
    {
        // rb2d = this.gameObject.GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Movement()
    {
        /*float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb2d.linearVelocity = new Vector2((moveHorizontal * speed) * Time.deltaTime, (moveVertical * speed) * Time.deltaTime);*/
    }

}

public enum WeaponType
{
    Gun,
    Tordedo
}

public enum WeaponRange
{
    VeryShort,
    Short,
    Moderate,
    Long,
    VeryLong
}

public enum Moveability
{
    Sluggish,
    Slow,
    Average,
    Quick,
    VeryQuick
}
