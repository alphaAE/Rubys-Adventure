using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour {
    public GameObject imgDialog;
    public Text dialogText;

    void Start() {
        imgDialog.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other) {
        imgDialog.SetActive(false);
    }

    public void ShowDialog() {
        if (TaskManager.Instance.isMissionCompleted) {
            dialogText.text = "干的好 Ruby！";
        }
        imgDialog.SetActive(true);
    }
}