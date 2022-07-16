using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour {
    public GameObject collectibleHealthParticlePrefab;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            RubyController rubyController = other.GetComponent<RubyController>();
            if (!rubyController.IsMaxHealth()) {
                rubyController.ChangeHealth(1);
                _audioSource.Play();
                _spriteRenderer.enabled = false;
                _boxCollider2D.enabled = false;
                Instantiate(collectibleHealthParticlePrefab, transform.position, Quaternion.identity);
                // Invoke(nameof(DestroyThis), 1f);
            }
        }
    }

    private void DestroyThis() {
        Destroy(gameObject);
    }
}