using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour {
    public GameObject explodeParticlePrefab;
    private float _currentLife = 2f;
    private Rigidbody2D _rd2;

    private void Awake() {
        _rd2 = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (_currentLife <= 0) {
            Destroy(gameObject);
        }

        _currentLife -= Time.deltaTime;
    }

    public void Launcher(Vector2 direction, float force) {
        _rd2.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Destroy(gameObject);
        if (col.gameObject.CompareTag("Enemy")) {
            col.gameObject.GetComponent<RobotController>().Fix();
            Instantiate(explodeParticlePrefab, col.transform.position, Quaternion.identity);
        }
    }
}