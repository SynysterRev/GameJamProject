using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBullet : MonoBehaviour
{
    [SerializeField]
    private Text nbBullet = null;
    [SerializeField]
    private Image imgBullet = null;
    private PlayerController player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnFire += UpdateBullet;
        player.OnReloading += UpdateBullet;
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
}
