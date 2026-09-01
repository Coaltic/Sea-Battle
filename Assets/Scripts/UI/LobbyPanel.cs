using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyPanel : MonoBehaviour
{
    public TMP_Text lobbyNameText;
    public TMP_Text lobbyPlayerAmountText;
    public TMP_Text lobbyCreationLength;

    public RectTransform rectTransform;
    public float targetPositionBelow;
    public float height;
    // public ScrollRect scrollRect;

    void Awake()
    {
        lobbyNameText = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        lobbyPlayerAmountText = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        
        Canvas.ForceUpdateCanvases();
        Invoke("GetRectSize", 0.01f);
    }

    private void GetRectSize()
    {
        rectTransform = this.GetComponent<RectTransform>();
        height = rectTransform.rect.height;
        //targetPositionBelow = rectTransform.localPosition.y -  * 2;
        // scrollRect = this.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ScrollRect>();
        // Debug.Log($"Local Position Y: {rectTransform.localPosition.y}, Height: {height}");

        // LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
    }
}
