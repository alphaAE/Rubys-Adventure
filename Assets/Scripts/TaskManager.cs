using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    public static TaskManager Instance { get; private set; }
    public bool isStartTask;
    public bool isMissionCompleted;
    public AudioSource loopAudio;

    private int _enemyCount;
    private AudioSource _audioSource;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void DefeatEnemy() {
        _enemyCount++;
        if (_enemyCount >= 3) {
            isMissionCompleted = true;
            Invoke(nameof(MissionCompletedAudio), 1.3f);
        }
    }

    private void MissionCompletedAudio() {
        _audioSource.Play();
        loopAudio.Stop();
    }
}