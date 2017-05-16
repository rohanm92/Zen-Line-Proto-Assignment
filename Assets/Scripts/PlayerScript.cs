using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using CnControls;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour {
    public float movementSpeed = 3.5f;
    public int level = 0;
    public int score = 0;
    public bool game_over = false;
    public GameObject gameOverObject;

    private Vector2 move_pos;
    private ParticleSystem deathParticle;
    private Rigidbody2D _rigidbody2D;

    private void Awake () {
        // For initialization
        GetComponent<SpriteRenderer>().enabled = true;
        game_over = false;
        gameOverObject.SetActive(false);

        deathParticle = GetComponent<ParticleSystem>();
        deathParticle.Stop();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        // Initialization
        GetComponent<SpriteRenderer>().enabled = true;
        game_over = false;
        deathParticle.Stop();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void moveShip (Vector2 moveVector) {
        // If we have some move input give velocity to player
        if (moveVector.sqrMagnitude > 0.001f) {
            _rigidbody2D.velocity = moveVector * movementSpeed;
        } else {
            _rigidbody2D.velocity = moveVector * 0;
        }
    }

    void onHit () {
        // Show particle effect and get ready to call gameOver
        deathParticle.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        game_over = true;
        gameOverObject.SetActive(true);
        Invoke("gameOver", 3f);
    }

    void gameOver () {
        // Set Highscore and show menu scene
        int real_score = score/10;
        if(real_score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", real_score);
        }
        SceneManager.LoadScene("MenuScreen");
    }

    public void incrLevel () {
        // increase Level
        level++;
    }

    // Update is called once per frame
    void Update () {
        // If game isn't over move and increment score
        if (!game_over) {
            score++;
            var moveVector = new Vector2(CnInputManager.GetAxis("MoveX"), CnInputManager.GetAxis("MoveY"));
            moveShip(moveVector);
        }
    }

    void OnCollisionEnter2D(Collision2D coll_object) {
        // If hitting a red wall
        if(coll_object.gameObject.tag == "Wall")
        {
            onHit();
        }        
    }
}
