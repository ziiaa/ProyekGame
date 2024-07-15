using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public static DragScript hurufSedangDrag;
    [SerializeField] private TextMeshProUGUI hurufDisplay;

    private bool petunjuk, terisi;
    private Transform parentAwal;
    private Vector3 posisiAwal;

    private string Huruf { get; set; }

    private void Awake()
    {
        if (hurufDisplay == null)
        {
            hurufDisplay = GetComponentInChildren<TextMeshProUGUI>();
            if (hurufDisplay == null)
            {
                Debug.LogError("TextMeshProUGUI component is not assigned or found in children.");
            }
        }
    }

    public void Inisialisasi(Transform parent, string huruf, bool petunjuk)
    {
        Huruf = huruf;
        transform.SetParent(parent);
        hurufDisplay.text = Huruf;
        this.petunjuk = petunjuk;
        GetComponent<CanvasGroup>().alpha = petunjuk ? 0.5f : 1f;
    }

    public void Cocok(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        petunjuk = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (petunjuk)
            return;

        posisiAwal = transform.position;
        parentAwal = transform.parent;
        hurufSedangDrag = this;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (petunjuk)
            return;

        transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (petunjuk && !terisi)
        {
            if (hurufSedangDrag.Huruf == Huruf)
            {
                ManagerKata.Instance.TambahPoin();
                hurufSedangDrag.Cocok(transform);
                terisi = true;
                GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (petunjuk)
            return;

        hurufSedangDrag = null;

        if (transform.parent == parentAwal)
        {
            transform.position = posisiAwal;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}