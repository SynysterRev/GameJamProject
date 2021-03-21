using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
    private void OnEndAnimation()
    {
        Destroy(gameObject);
    }
}
