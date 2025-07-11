using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public float roadSpeed;

    private float offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += (roadSpeed * gameManager.globalSpeed * Time.deltaTime) / 100;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
