using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staynotes : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerStay2D(Collider2D other) {
        PlayerController ct = other.GetComponent<PlayerController>();
        if(ct!=null) canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        canvas.SetActive(false);
    }
}
