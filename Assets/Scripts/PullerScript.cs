using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullerScript : MonoBehaviour {
    public float pull_delay = 1f;
    public float pull_rate = 0.25f;
    public float current_radius = 1f;
    public float initial_radius = 5f;
    //Modify to create pull 10 to -ve value
    public float pull_force = 10;

    private bool imploded = false;
    private CircleCollider2D pull_radius;
    private ParticleSystem particleSystem1;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void OnEnable () {
        pull_radius = GetComponent<CircleCollider2D>();
        particleSystem1 = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Begin();
    }

    void Begin () {
        ParticleSystem.ShapeModule particle_shape = particleSystem1.shape;

		// Prepare for new pulling radius to form
        current_radius = initial_radius;
        imploded = false;
        particleSystem1.Play();
        pull_radius.enabled = true;
        pull_radius.radius = current_radius;
        particle_shape.radius = current_radius;
    }    
    // Update is called once per frame
    void Update () {
		// Modify pulled delay
        pull_delay -= Time.deltaTime;
        
        spriteRenderer.transform.Rotate(Vector3.forward*(Time.deltaTime*pull_delay));
        
        if(pull_delay<0) {
            imploded = true;
        }
    }

    void FixedUpdate() {
        ParticleSystem.ShapeModule particle_shape = particleSystem1.shape;

		// Keep reducing pull radius
        if(current_radius > 1f) {
            if(imploded == true) {
                current_radius -= pull_rate;            
            }
        } else {
			// Otherwise reset pull and call begin
            if(particleSystem1.isPlaying) {
                particleSystem1.Stop();
                pull_radius.enabled = false;
            }
            if(particleSystem1.particleCount == 0) {
                Invoke("Begin", pull_delay);
            }
        }
        pull_radius.radius = current_radius;
        particle_shape.radius = current_radius;
    }

    void OnTriggerStay2D(Collider2D coll) {
		// Pull object to puller
        if(coll.gameObject.GetComponent<Rigidbody2D>()!= null) {
            Vector2 target = coll.gameObject.transform.position;
            Vector2 puller_pos = gameObject.transform.position;

            coll.gameObject.transform.position = Vector2.Lerp(target, puller_pos, Time.deltaTime*2);
        }
    }
}
