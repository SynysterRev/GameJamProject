using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimDestroy : MonoBehaviour
{
    [SerializeField]
    GameObject prefabParticle = null;
    [SerializeField]
    GameObject prefabWave = null;
    private void Start()
    {
        if (prefabParticle)
            GameObject.Instantiate(prefabParticle, transform.position, Quaternion.identity);
        if (prefabWave)
            GameObject.Instantiate(prefabWave, transform.position, Quaternion.identity);
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
