using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShipButtonSelectable : Selectable, IPointerClickHandler
{
    [Header("Click Events")]
    public UnityEvent OnLeftClick;
    public UnityEvent OnMiddleClick;
    public UnityEvent OnRightClick;

    private Coroutine _resetRoutine;

    protected override void Reset()
    {
        base.Reset();


        var imageComponent = GetComponent<Image>();
        if (imageComponent == null) imageComponent = gameObject.AddComponent<Image>();

        targetGraphic = imageComponent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DoStateTransition(SelectionState.Pressed, true);

        switch (eventData.button)
        {

            default:
            case PointerEventData.InputButton.Left:
                OnLeftClick?.Invoke();
                break;

            case PointerEventData.InputButton.Middle:
                OnMiddleClick?.Invoke();
                break;

            case PointerEventData.InputButton.Right:
                OnRightClick?.Invoke();
                break;
        }

        if (_resetRoutine != null) StopCoroutine(routine:OnFinishSubmit());

        _resetRoutine = StartCoroutine(routine:OnFinishSubmit());
    }


    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }




}
