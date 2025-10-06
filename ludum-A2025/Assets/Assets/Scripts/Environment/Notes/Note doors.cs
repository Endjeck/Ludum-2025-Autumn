using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notedoors : MonoBehaviour
{
    public Image Note;
    private bool noteActive = false;

    private void OnTriggerEnter(Collider other)
    {
        Note.gameObject.SetActive(true);
        noteActive = true;
    }

    private void Update()
    {
        if (noteActive && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Note.gameObject.SetActive(false);
            noteActive = false;
        }
    }
}