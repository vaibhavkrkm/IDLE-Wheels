using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerSpeed;
    public Rigidbody2D body;
    public float acceleration = 5f;

    public GameManagerScript gameManagerScript;

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");

        //body.velocity = new Vector2(body.velocity.x, (verticalInput*2) * playerSpeed);
        body.AddForce(Vector2.up * verticalInput * acceleration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            gameManagerScript.Gameover();
        }
        else if (collision.gameObject.CompareTag("Star"))
        {
            gameManagerScript.PlayStarSound();
            gameManagerScript.UpdateScore(10);

            // using LeanTween to add star collection animation
            LeanTween.move(collision.gameObject,
                gameManagerScript.scoreUITransform,
                3f/gameManagerScript.globalSpeed).setEaseInCubic().setOnComplete(collision.gameObject.GetComponent<StarScript>().DestroyStar);
            
            // For alpha tweening
            //LeanTween.alpha(collision.gameObject, 0f, 0.2f).setEaseInSine().setOnComplete(collision.gameObject.GetComponent<StarScript>().DestroyStar);
        }
    }
}
