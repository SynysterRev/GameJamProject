using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBullet : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField]
    private Text nbBullet = null;
    [SerializeField]
    private Image imgBullet = null;
    [Header("Life")]
    [SerializeField]
    private Text nbLife = null;
    [SerializeField]
    private Image imgLife = null;
    private PlayerController player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnFire += UpdateBullet;
        player.OnReloading += UpdateBullet;
        player.OnHit += UpdateLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateBullet(int nbBullet)
    {
        this.nbBullet.text = nbBullet.ToString();
        imgBullet.fillAmount = nbBullet / 7.0f;
    }

    private void UpdateLife(int life, int maxLife)
    {
        nbLife.text = life.ToString() + " / " + maxLife.ToString();
        imgLife.fillAmount = life / (float)maxLife;
    }
}
