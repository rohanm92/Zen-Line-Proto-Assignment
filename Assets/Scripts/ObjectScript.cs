using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {
    public Transform PointB = null;
    public float secondsForOneLength = 4f;

    private Vector3 from_pos;
    private Vector3 to_pos;

    private Vector3 from_scale;
    private Vector3 to_scale;

    private Quaternion from_rot;
    private Quaternion to_rot;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    // Use this for initialization

    void Start () {
        // Save Original Positions
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        originalScale = transform.localScale;

        // Set what to transform into if pointB is set
        if (PointB != null) {
            from_pos = transform.localPosition;
            to_pos = PointB.localPosition;

            from_rot = transform.localRotation;
            to_rot = PointB.localRotation;

            from_scale = transform.localScale;
            to_scale = PointB.localScale;
        }
    }
    
    void OnEnable () {
        // Save Original Positions on New level pool instance being called
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        originalScale = transform.localScale;

        // Set what to transform into if pointB is set
        if (PointB != null) {
            from_pos = transform.localPosition;
            to_pos = PointB.localPosition;

            from_rot = transform.localRotation;
            to_rot = PointB.localRotation;

            from_scale = transform.localScale;
            to_scale = PointB.localScale;
        }
    }
    
    void OnDisable () {
        // Reset Original Transform and set velocity as 0
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        transform.localScale = originalScale;

        if (GetComponent<Rigidbody2D>() != null) {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update () {
        // Ping Pong between transform and pointB details if PointB exists
        if (PointB != null) {
            transform.localPosition = Vector3.Lerp(from_pos, to_pos, 
                Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time/secondsForOneLength, 1f)));
            transform.localRotation = Quaternion.Lerp(from_rot, to_rot, 
                Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time/secondsForOneLength, 1f)));
            transform.localScale = Vector3.Lerp(from_scale, to_scale, 
                Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time/secondsForOneLength, 1f)));
        }
    }
}
