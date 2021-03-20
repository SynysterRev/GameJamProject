using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : MonoBehaviour
{
    SpriteRenderer sprend = null;
    // Start is called before the first frame update
    void Start()
    {
        sprend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprend.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - transform.localScale.x);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.01f);
    }
}
