using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLight : MonoBehaviour
{
    PostProcess camPostProcess = null;
    // Start is called before the first frame update
    void Start()
    {
        camPostProcess = GameObject.FindObjectOfType<PostProcess>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
