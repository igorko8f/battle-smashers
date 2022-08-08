using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    private CanvasGroup _panel;

    private void Start()
    {
        _panel = GetComponent<CanvasGroup>();
    }

    public void SwitchPanel(float duration = 0.5f)
    {
        bool enablePanel = _panel.alpha > 0;

        _panel.blocksRaycasts = !enablePanel;
        _panel.interactable = !enablePanel;
        _panel.DOFade(enablePanel ? 0 : 1, duration);
    }
}
