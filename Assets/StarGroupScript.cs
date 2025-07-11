using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGroupScript : MonoBehaviour
{
    int childCount;
    private void Update()
    {
        childCount = transform.childCount;

        if (childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
