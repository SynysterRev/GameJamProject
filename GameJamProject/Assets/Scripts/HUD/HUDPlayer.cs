using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPlayer : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField]
    private Text nbBullet = null;
    [Header("Life")]
    [SerializeField]
    private Text nbLife = null;
    [SerializeField]
    private GameObject lifeGo = null;
    [SerializeField]
    private GameObject ammoGo = null;
    [Header("Score")]
    [SerializeField]
    private Text score = null;
    private PlayerController player = null;
    private GameManager gm = null;
    // Start is called before the first frame update
    void Awake()
    {
        gm = GameManager.Instance;
        gm.OnUpdateScore += UpdateScore;
        player = FindObjectOfType<PlayerController>();
        player.OnFire += UpdateBullet;
        player.OnReloading += UpdateBullet;
        player.OnHit += UpdateLife;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        gm.OnUpdateScore -= UpdateScore;
        player.OnFire -= UpdateBullet;
        player.OnReloading -= UpdateBullet;
        player.OnHit -= UpdateLife;
    }

    private void OnDestroy()
    {
        
    }

    private void UpdateBullet(int nbBullet, bool _reload)
    {
        this.nbBullet.text = nbBullet.ToString();
        if (nbBullet <= 7)
        {
            int childID = 7 - (nbBullet + ((_reload) ? 0 : 1));
            if (childID >= 0 && childID < ammoGo.transform.childCount)
            {
                Animator anim = ammoGo.transform.GetChild(childID).GetComponentInChildren<Animator>();
                if (_reload)
                    anim.Rebind();
                else
                    anim.SetTrigger("use");
            }
        }
    }

    private void UpdateLife(int life, int maxLife)
    {
        nbLife.text = life.ToString() + " / " + maxLife.ToString();
        if (life < maxLife)
        {
            int childID = maxLife - (life + 1);
            if (childID >= 0 && childID < lifeGo.transform.childCount)
            {
                Animator anim = lifeGo.transform.GetChild(childID).GetComponentInChildren<Animator>();
                anim.SetTrigger("hit");
            }
        }

    }

    private void UpdateScore(int newScore)
    {
        score.text = "Score\n" + newScore.ToString();
    }

    public void FireAnim()
    {

    }
}
