using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.WSA;

public class RubyController : MonoBehaviour {
    public AudioSource runAudio;
    public AudioSource triggerAudio;

    public AudioClip playerRunAudioClip;
    public AudioClip playerHitAudioClip;
    public AudioClip throwCogAudioClip;

    public GameObject bulletPrefab;
    private Rigidbody2D _rd2;
    private Animator _anim;

    private Vector3 _spawnPoint;

    private Vector2 _directionV2 = new(0, -1);

    private const float MaxSpeed = 3.6f;
    private float _currentSpeed;

    private const int MaxHealth = 5;
    private int _currentHealth;

    private const float InvincibleTime = 1f;
    private float _currentInvincibleTime = 0;

    private void Start() {
        _rd2 = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _currentHealth = MaxHealth;
        runAudio.clip = playerRunAudioClip;
        _spawnPoint = transform.position;
    }

    private void Update() {
        if (_currentInvincibleTime > 0) _currentInvincibleTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {
            Launcher();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            RaycastHit2D raycastHit2D =
                Physics2D.Raycast(transform.position, _directionV2, 1.0f, LayerMask.GetMask("NPC"));
            if (raycastHit2D.collider) {
                NPCDialog npcDialog = raycastHit2D.collider.GetComponent<NPCDialog>();
                if (npcDialog) {
                    npcDialog.ShowDialog();
                    TaskManager.Instance.isStartTask = true;
                }
            }
        }
    }

    private void FixedUpdate() {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector2 move = (transform.right * inputX + transform.up * inputY).normalized;
        _currentSpeed = move.magnitude;
        _rd2.MovePosition(_rd2.position + move * (MaxSpeed * Time.deltaTime));

        if (!Mathf.Approximately(_currentSpeed, 0)) {
            _directionV2.Set(inputX, inputY);
        }

        _anim.SetFloat("Speed", _currentSpeed);
        _anim.SetFloat("Look X", _directionV2.x);
        _anim.SetFloat("Look Y", _directionV2.y);

        if (!Mathf.Approximately(_currentSpeed, 0)) {
            if (!runAudio.isPlaying) {
                runAudio.Play();
            }
        }
        else {
            runAudio.Stop();
        }
    }

    public void ChangeHealth(int amount) {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, MaxHealth);
        UIRubyHealthBar.Instance.SetValue(_currentHealth / (float)MaxHealth);
    }

    public void Hurt(int damage) {
        if (_currentInvincibleTime > 0) return;
        _currentInvincibleTime = InvincibleTime;
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, MaxHealth);
        _anim.SetTrigger("Hit");
        UIRubyHealthBar.Instance.SetValue(_currentHealth / (float)MaxHealth);
        triggerAudio.PlayOneShot(playerHitAudioClip);
        Debug.Log(_currentHealth);
        if (_currentHealth <= 0) {
            _currentHealth = MaxHealth;
            UIRubyHealthBar.Instance.SetValue(_currentHealth / (float)MaxHealth);
            transform.position = _spawnPoint;
            _directionV2 = new Vector2(0, -1);
        }
    }

    public bool IsMaxHealth() {
        return _currentHealth >= MaxHealth;
    }

    private void Launcher() {
        if (!TaskManager.Instance.isStartTask) return;
        _anim.SetTrigger("Launch");
        triggerAudio.PlayOneShot(throwCogAudioClip);
        GameObject obj = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 0.5f), Quaternion.identity);
        CogBullet cogBullet = obj.GetComponent<CogBullet>();
        cogBullet.Launcher(_directionV2, 800);
    }
}