using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour {
    private Transform _player;

    private bool _isRotating;

    private float _rotateTime = 0.2f;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update() {
        transform.position = _player.position;
        // Rotate();
    }

    void Rotate() {
        if (Input.GetKeyDown(KeyCode.Q) && !_isRotating) {
            StartCoroutine(RotateAround(-45, _rotateTime));
        }

        if (Input.GetKeyDown(KeyCode.E) && !_isRotating) {
            StartCoroutine(RotateAround(45, _rotateTime));
        }
    }

    IEnumerator RotateAround(float angel, float time) {
        float num = 60 * time;
        float nextAngel = angel / num;
        _isRotating = true;

        for (int i = 0; i < num; i++) {
            transform.Rotate(new Vector3(0, 0, nextAngel));
            yield return new WaitForFixedUpdate();
        }

        _isRotating = false;
    }
}