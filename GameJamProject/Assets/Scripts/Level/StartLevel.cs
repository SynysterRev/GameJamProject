using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ps = null;
    [SerializeField]
    private bool startGame = false;

    private void OnParticleSystemStopped()
    {
        if (startGame)
        {
            LevelManager.Instance.StartLevel();
        }
        else
            ps.Play();
        Destroy(gameObject);
    }
}
