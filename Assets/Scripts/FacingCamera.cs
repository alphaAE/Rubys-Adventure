using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour {
    private Transform[] _childs;
    private Camera _camera;

    void Start() {
        _childs = new Transform[transform.childCount];
        _camera = Camera.main;
        for (int i = 0; i < transform.childCount; i++) {
            _childs[i] = transform.GetChild(i);
        }
    }

    void Update() {
        foreach (var child in _childs) {
            if (child is null) return;
            child.rotation = _camera.transform.rotation;
        }
    }
}