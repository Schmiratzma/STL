using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ToggleInformation : MonoBehaviour
{
    [SerializeField] Vector2 shownPosition;
    [SerializeField] Vector2 hiddenPosition;

    [SerializeField] float AnimationSpeedShow;
    [SerializeField] Ease EaseForShow;

    [SerializeField] float AnimationSpeedHide;
    [SerializeField] Ease EaseForHide;

    public bool isShown;

    [SerializeField] Button toggleButton;

    RectTransform rectTransform;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        toggleButton.onClick.AddListener(ToogleInformationDisplay);
    }

    public void ToogleInformationDisplay()
    {
        if(isShown)
        {
            rectTransform.DOAnchorPos(hiddenPosition, AnimationSpeedHide).SetEase(EaseForHide);
            //rectTransform.DOLocalMove(hiddenPosition, AnimationSpeedHide).SetEase(EaseForHide);
            //transform.DOMove(hiddenPosition,AnimationSpeedHide).SetEase(EaseForHide);
            isShown = false;
        }
        else
        {
            rectTransform.DOAnchorPos(shownPosition, AnimationSpeedShow).SetEase(EaseForShow);
            //rectTransform.DOLocalMove(shownPosition, AnimationSpeedShow).SetEase(EaseForShow);
            //transform.DOMove(shownPosition,AnimationSpeedShow).SetEase(EaseForShow);
            isShown = true;
        }
    }
}
