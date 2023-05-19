using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnableWaveCanvasOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject waveCanvas;
    private Button button;
    private float hoverDelay = 0.1f;
    private bool isHovering = false;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void DisableCanvasOnClick()
    {
        StartCoroutine(DisableAfterClick());
    }
    private IEnumerator DisableAfterClick()
    {
        yield return new WaitForSeconds(.1f);

        waveCanvas.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable == true)
        {
            isHovering = true;
            Invoke("EnableObject", hoverDelay);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable == true)
        {
            isHovering = false;
            Invoke("DisableObject", hoverDelay);
        }
    }

    private void EnableObject()
    {
        if (isHovering)
        {
            waveCanvas.SetActive(true);
        }
    }

    private void DisableObject()
    {
        if (!isHovering)
        {
            waveCanvas.SetActive(false);
        }
    }
}
