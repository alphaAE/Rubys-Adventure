using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPoints : MonoBehaviour {
    private float _timeMax = 0.5f;
    private float _courrentTime;
    private int _index = 0;

    private Text _textCom;

    private String[] _text = { ".", "..", "..." };

    private void Start() {
        _textCom = GetComponent<Text>();
        _courrentTime = _timeMax;
    }

    private void FixedUpdate() {
        if (TaskManager.Instance.isStartTask) {
            _textCom.text = "";
            return;
        }

        if (_courrentTime > 0) {
            _courrentTime -= Time.deltaTime;
        }
        else {
            if (_index >= _text.Length) {
                _index = 0;
            }

            _courrentTime = _timeMax;
            _textCom.text = _text[_index];
            _index++;
        }
    }
}