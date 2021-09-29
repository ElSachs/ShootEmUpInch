using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widener : MonoBehaviour
{
    void Update()
    {
        transform.localScale = transform.localScale + new Vector3(2f, 2f, 2f) * Time.deltaTime;
        if (transform.localScale.x >= 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
