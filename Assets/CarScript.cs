using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public int minCarSpeed;
    public int maxCarSpeed;
    [HideInInspector] public int carSpeed;
    public Transform carContainer;

    GameManagerScript gameManagerScript;

    private void Start()
    {
        int childCount = carContainer.childCount;
        int randomIndex = Random.Range(0, childCount);
        Transform randomChild = carContainer.GetChild(randomIndex);

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        // making all sprites inactive
        for (int i=0; i<childCount; i++)
        {
            carContainer.GetChild(i).gameObject.SetActive(false);
        }

        // making the current sprite active
        randomChild.gameObject.SetActive(true);

        // randomizing the speed
        RandomizeSpeed();
    }

    private void Update()
    {
        transform.position = transform.position + (carSpeed * gameManagerScript.globalSpeed * Vector3.left) * Time.deltaTime;

        // destroying the car if it crosses the screen
        if (transform.position.x < -13)
        {
            Destroy(gameObject);
        }
    }

    public void RandomizeSpeed()
    {
        carSpeed = Random.Range(minCarSpeed, maxCarSpeed);
    }
}
