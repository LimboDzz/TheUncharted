using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public AudioClip audioPickup;

    private void OnTriggerStay2D(Collider2D other) {
        PlayerController ct = other.GetComponent<PlayerController>();
        if(Input.GetKeyDown(KeyCode.S) && ct!=null){
            ct.changeHP(1);
            AudioSource.PlayClipAtPoint(audioPickup, transform.position, 10f);
            Destroy(gameObject);
        }
    }
}
