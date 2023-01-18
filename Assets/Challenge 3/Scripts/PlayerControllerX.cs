using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    //public float gravityModifier = 2f;
    public Rigidbody playerRb;

    public float upperLimit = 14;
    //public float lowerLimit = 2;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;

    public AudioClip music;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        //Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 4, ForceMode.Impulse);

        playerAudio.PlayOneShot(music, .01f);

    }

    // Update is called once per frame
    void Update() {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver) {
            playerRb.AddForce(Vector3.up * floatForce);
        }
        /*
        if (transform.position.y < lowerLimit && !gameOver) {
            transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
            playerAudio.PlayOneShot(bounceSound, 1f);
        }
        */
        if (transform.position.y > upperLimit) {
            transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, .2f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, .2f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.tag == "Ground")
            playerAudio.PlayOneShot(bounceSound, .6f);
    }

}
