using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum BulletType
{
    leftType,
    rightType
}
public class Bullets : MonoBehaviour
{
    public delegate void DelegateDestroy();
    public event DelegateDestroy OnTouchEnemy;
    [SerializeField]
    private float speed = 1.0f;
    private BulletType bulletType = BulletType.rightType;

    public BulletType TypeBullet { get => bulletType; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaunchProjectile(BulletType bulletType)
    {
        this.bulletType = bulletType;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    public void CorrectBullet()
    {
        if (OnTouchEnemy != null)
            OnTouchEnemy();
    }
}
