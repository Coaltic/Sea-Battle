using UnityEngine;

public class StretchSprite : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        // Reset scale before measuring
        transform.localScale = Vector3.one;

        // Get the width and height of the sprite in world units
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // Calculate world space dimensions from the Main Camera
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // Calculate the scale factors
        float scaleX = worldScreenWidth / spriteWidth;
        float scaleY = worldScreenHeight / spriteHeight;

        // Apply the new scale to stretch perfectly across the screen
        transform.localScale = new Vector3(scaleX, scaleY, 1f);

        this.gameObject.AddComponent<PolygonCollider2D>();
    }
}
