using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRubyHealthBar : MonoBehaviour {
    public static UIRubyHealthBar Instance { get; private set; }
    public Image maskImg;
    private float _originalWidth;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        _originalWidth = maskImg.rectTransform.rect.width;
    }

    public void SetValue(float fillPercent) {
        maskImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalWidth * fillPercent);
    }
}