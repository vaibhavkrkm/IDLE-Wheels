using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{   
    RoadScript roadScript;
    GameManagerScript gameManagerScript;
    Animator animator;
    float starSpeed;

    private void Start()
    {
        roadScript = GameObject.Find("Road").GetComponent<RoadScript>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        animator = GetComponent<Animator>();
        animator.speed = 0.5f;
    }

    private void Update()
    {
        starSpeed = roadScript.roadSpeed / 5.5f;
        transform.position = transform.position + (Vector3.left * starSpeed * gameManagerScript.globalSpeed) * Time.deltaTime;

        // destroying the star if it crosses the screen
        if (transform.position.x < -13)
        {
            DestroyStar();
        }
    }

    public void DestroyStar()
    {
        Destroy(gameObject);
    }
}
