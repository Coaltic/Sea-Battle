using UnityEngine;
using UnityEngine.UI;

public class FleetUI : MonoBehaviour
{
    public Image panelImage;
    public RectTransform rectTransform;
    public float height;
    public float targetPosistionDown;
    public float targetPosistionUp;
    public bool openMenu;
    public bool menuIsOpen;
    public bool closeMenu;
    public bool menuIsClosed;


    void Start()
    {
        menuIsOpen = true;

        if (panelImage == null) panelImage = this.GetComponent<Image>();

        rectTransform = panelImage.rectTransform;
        height = rectTransform.rect.height;
        targetPosistionUp = rectTransform.localPosition.y;
        targetPosistionDown = rectTransform.localPosition.y - (height + 10);
        // Debug.Log(height);

        // panelImage.gameObject.transform.localPosition = new Vector2(0, targetPosistion);
        // Debug.Log(rectTransform.localPosition.y);

    }

    // Update is called once per frame
    void Update()
    {
        CheckForMenuButton();
        if (closeMenu) MoveMenuDown();
        if (openMenu) MoveMenuUp();
    }

    public void CheckForMenuButton()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (menuIsOpen)
            {
                closeMenu = true;
            }
            if (menuIsClosed)
            {
                openMenu = true;
            }
        }
    }

    public void MoveMenuDown()
    {
        if ((rectTransform.localPosition.y) > targetPosistionDown)
        {
            panelImage.gameObject.transform.localPosition = new Vector2(0, panelImage.gameObject.transform.localPosition.y - 10);

            Debug.Log($"Local Posistion: {rectTransform.localPosition.y}, Target Position: {targetPosistionDown}");
            Debug.Log(rectTransform.localPosition.y);
        }
        else if ((rectTransform.localPosition.y) <= targetPosistionDown)
        {
            closeMenu = false;
            menuIsClosed = true;
            menuIsOpen = false;
        }
    }

    public void MoveMenuUp()
    {
        if ((rectTransform.localPosition.y) < targetPosistionUp)
        {
            panelImage.gameObject.transform.localPosition = new Vector2(0, panelImage.gameObject.transform.localPosition.y + 10);

            Debug.Log($"Local Posistion: {rectTransform.localPosition.y}, Target Position: {targetPosistionDown}");
            Debug.Log(rectTransform.localPosition.y);
        }
        else if ((rectTransform.localPosition.y) >= targetPosistionUp)
        {
            openMenu = false;
            menuIsOpen = true;
            menuIsClosed = false;
        }
    }
}
