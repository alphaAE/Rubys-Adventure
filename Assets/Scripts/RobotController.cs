using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RobotController : MonoBehaviour {
    public ParticleSystem particleSys;
    public AudioSource walkAudio;
    public AudioSource triggerAudio;

    public AudioClip[] hitAudioClips;
    public AudioClip fixedAudioClip;

    private Rigidbody2D _rid;
    private Animator _anim;
    private Direction _direction;
    private Vector2 _directionVector;

    private float _speed = 2.0f;

    private float _directionTime = 1.0f;
    private float _currentDirectionTime;

    private bool _fixed = false;

    void Start() {
        _rid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _currentDirectionTime = _directionTime;
        RandomDirection();
    }

    public void Fix() {
        _fixed = true;
        _anim.SetTrigger("Fixed");
        _rid.simulated = false;
        particleSys.Stop();
        walkAudio.Stop();
        triggerAudio.PlayOneShot(hitAudioClips[Random.Range(0, hitAudioClips.Length)]);
        TaskManager.Instance.DefeatEnemy();
        Invoke(nameof(PlayFixedAudio), 0.5f);
    }

    private void PlayFixedAudio() {
        triggerAudio.PlayOneShot(fixedAudioClip);
    }

    private void FixedUpdate() {
        if (_fixed) return;
        _currentDirectionTime -= Time.deltaTime;
        if (_currentDirectionTime <= 0) {
            _currentDirectionTime = _directionTime;
            RandomDirection();
        }

        _anim.SetFloat("Speed", _speed);
        _rid.MovePosition(_rid.position + _directionVector * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.CompareTag("Player")) {
            RubyController rubyController = collisionGameObject.GetComponent<RubyController>();
            rubyController.Hurt(1);
        }
        else if (collisionGameObject.CompareTag("MapObject")) {
            RandomDirection();
        }
    }

    private void RandomDirection() {
        while (true) {
            var tempDir = (Direction)Random.Range(0, 4);
            if (tempDir != _direction) {
                _direction = tempDir;
                break;
            }
        }

        switch (_direction) {
            case Direction.Up:
                _directionVector = new Vector2(0, 1);
                _anim.SetFloat("Look X", 0);
                _anim.SetFloat("Look Y", 1);
                break;
            case Direction.Right:
                _directionVector = new Vector2(1, 0);
                _anim.SetFloat("Look X", 1);
                _anim.SetFloat("Look Y", 0);
                break;
            case Direction.Down:
                _directionVector = new Vector2(0, -1);
                _anim.SetFloat("Look X", 0);
                _anim.SetFloat("Look Y", -1);
                break;
            case Direction.Left:
                _directionVector = new Vector2(-1, 0);
                _anim.SetFloat("Look X", -1);
                _anim.SetFloat("Look Y", 0);
                break;
        }
    }
}