using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitchChanger : MonoBehaviour {

    private AudioSource audioSource;
    
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    void FixedUpdate() {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }
	
}
