using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick1 : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public Image JoyStickBG;
    public Image JoyStickIM;

    Image bgImage;
    Image JImage;
    Image Range;

    Vector3 InputVector;

	void Start ()
    {
        Range = GetComponent<Image>();
        bgImage = JoyStickBG.GetComponent<Image>();
        JImage = JoyStickIM.GetComponent<Image>();
        bgImage.enabled = false;
        JImage.enabled = false;
	}
	public virtual void OnDrag (PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = pos.x / (bgImage.rectTransform.sizeDelta.x / 2);
            pos.y = pos.y / (bgImage.rectTransform.sizeDelta.y / 2);

            InputVector.Set(pos.x, 0, pos.y);

            JImage.rectTransform.anchoredPosition = pos * 18;
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Range.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            bgImage.rectTransform.anchoredPosition = pos;
            bgImage.enabled = true;
            JImage.enabled = true;
        }
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputVector = Vector3.zero;
        bgImage.rectTransform.anchoredPosition = Vector3.zero;
        JImage.rectTransform.anchoredPosition = Vector3.zero;
        bgImage.enabled = false;
        JImage.enabled = false;
    }

    public float Horizontal()
    {
        return InputVector.x;
    }
    public float Vertical()
    {
        return InputVector.z;
    }
}
