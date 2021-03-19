using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    private BulletType[] orderDamage = null;
    private Rigidbody2D rb = null;
    private int currentOrderDmg = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    virtual protected void LoadData()
    {


        rb.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullets bullet = collision.gameObject.GetComponent<Bullets>();
        if (bullet)
        {
            if (bullet.TypeBullet == orderDamage[currentOrderDmg])
            {
                currentOrderDmg++;
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
