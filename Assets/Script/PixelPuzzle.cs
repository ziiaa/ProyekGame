using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PixelPuzzle : MonoBehaviour
{
    public List<Image> pieces; // Assign pieces in the Inspector
    public Transform puzzleArea; // Assign the puzzle area (parent transform) in the Inspector

    private Image selectedPiece;
    private Vector2 offset;
    private RectTransform canvasRectTransform;

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
        if (selectedPiece != null)
        {
            OnMouseDrag();
        }
    }

    void OnMouseDown()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            Image piece = result.gameObject.GetComponent<Image>();
            if (piece != null && pieces.Contains(piece))
            {
                selectedPiece = piece;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out offset);
                offset = selectedPiece.rectTransform.anchoredPosition - offset;
                break;
            }
        }
    }

    void OnMouseUp()
    {
        if (selectedPiece != null)
        {
            selectedPiece = null;
        }
    }

    void OnMouseDrag()
    {
        if (selectedPiece != null)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out localPoint);
            selectedPiece.rectTransform.anchoredPosition = localPoint + offset;
        }
    }
}
