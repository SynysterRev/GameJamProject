using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.02f);
    }

    public void StartAnimation()
    {
        transform.localScale = new Vector3(1.5f, 2.0f, 1.0f);
    }
}
