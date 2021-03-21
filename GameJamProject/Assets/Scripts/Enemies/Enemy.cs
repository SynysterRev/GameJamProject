using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void DelegateDeath();
    public event DelegateDeath OnDeath;
    public delegate void DelegateTypeBullet(BulletType type);
    public event DelegateTypeBullet OnChangeType;

    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private int indexTypeEnemy = 0;
    [SerializeField]
    private GameObject firePrefab = null;
    [SerializeField]
    private GameObject explosionPrefab = null;
    private BulletType[] orderDamage = null;
    private Rigidbody2D rb = null;
    private int currentOrderDmg = 0;
    private GameManager gm = null;
    private int score = 0;

    public BulletType CorrectDmg { get => orderDamage[currentOrderDmg]; }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    virtual public void LoadData(int indexEnemy)
    {
        gm = GameManager.Instance;
        indexTypeEnemy = indexEnemy;
        TypeDamageScriptable combo = Resources.Load<TypeDamageScriptable>("DataEnemy/Combos");

        score = combo.listCombo[indexTypeEnemy].scoreValue;
        orderDamage = new BulletType[combo.listCombo[indexTypeEnemy].bulletTypes.Length];
        for (int i = 0; i < orderDamage.Length; ++i)
            orderDamage[i] = combo.listCombo[indexTypeEnemy].bulletTypes[i];
        if (OnChangeType != null)
            OnChangeType(orderDamage[currentOrderDmg]);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullets bullet = collision.GetComponent<Bullets>();
        PlayerController player = collision.GetComponent<PlayerController>();
        if (bullet)
        {
            GameObject go = GameObject.Instantiate(firePrefab, bullet.transform.position, Quaternion.identity);
            go.transform.localScale *= 0.5f;

            if (bullet.TypeBullet == orderDamage[currentOrderDmg])
            {

                bullet.CorrectBullet();
                currentOrderDmg++;



                if (currentOrderDmg >= orderDamage.Length)
                {

                    //anim mort
                    rb.velocity = Vector2.zero;
                    if (OnDeath != null)
                        OnDeath();
                    gm.UpdateScore(score);

                    GameObject.Instantiate(explosionPrefab, transform.GetChild(0).position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    if (OnChangeType != null)
                        OnChangeType(orderDamage[currentOrderDmg]);
                }
            }
        }
        else if (player)
        {
            //anim mort
            rb.velocity = Vector2.zero;
            if (OnDeath != null)
                OnDeath();
            Destroy(gameObject);
        }
    }
}
