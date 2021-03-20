using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private int indexTypeEnemy = 0;
    private BulletType[] orderDamage = null;
    private Rigidbody2D rb = null;
    private int currentOrderDmg = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    virtual protected void LoadData()
    {
        string textFile = Resources.Load<TextAsset>("DataEnemy/Data").ToString();
        string[] allData = textFile.Split('\n');
        orderDamage = new BulletType[allData[indexTypeEnemy].Length - 1];
        for (int i = 0; i < allData[indexTypeEnemy].Length - 1; ++i)
        {
            if (allData[indexTypeEnemy][i] == 'R')
            {
                orderDamage[i] = BulletType.blue;
            }
            else
            {
                orderDamage[i] = BulletType.red;
            }
        }

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullets bullet = collision.GetComponent<Bullets>();
        if (bullet)
        {
            if (bullet.TypeBullet == orderDamage[currentOrderDmg])
            {
                currentOrderDmg++;
                Debug.Log(currentOrderDmg + " " + orderDamage.Length);
                if (currentOrderDmg >= orderDamage.Length)
                {
                    //anim mort
                    rb.velocity = Vector2.zero;
                    Destroy(gameObject);
                }
            }
        }
    }
}
