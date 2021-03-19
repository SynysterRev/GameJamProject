using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    red,
    blue
}
public class Bullets : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    private BulletType bulletType = BulletType.blue;
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
}
