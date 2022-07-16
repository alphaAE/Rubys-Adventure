using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {
    private int _damage = 1;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            RubyController rubyController = other.GetComponent<RubyController>();
            rubyController.Hurt(_damage);
        }
    }
}