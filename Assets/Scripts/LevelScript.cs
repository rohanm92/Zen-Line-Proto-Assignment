using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {
    private Transform _transform;
    public float moveSpeed = 1f;

    void Awake () {
        _transform = gameObject.GetComponent<Transform>();
    }

    void OnEnable () {
        // Set level position and enable all children
        _transform.position = new Vector3(-2.8f, 42.15f, 3.5f);
        foreach(Transform child in _transform) {
            child.gameObject.SetActive(true);
        }
    }

    void Destroy () {
        // Disable object and all children
        gameObject.SetActive(false);
        foreach(Transform child in _transform) {
            child.gameObject.SetActive(false);
        }
    }

    void OnDisable () {
        CancelInvoke("Destroy");
    }
    
    void Update () {
        // If level is past a certain point deactivate for later use
        if (_transform.position.y < -10f) {
            gameObject.SetActive(false);                
        }

        // Move the level
        Vector3 move_by = new Vector3(0, moveSpeed * Time.deltaTime, 0);
        _transform.Translate(-move_by);
    }
}
