using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEnemy : MonoBehaviour
{
    [SerializeField]
    private Sprite leftSprite = null;
    [SerializeField]
    private Sprite rightSprite = null;
    [SerializeField]
    private Image img = null;
    private Enemy enemy = null;
    private BulletType type;
    private Animator anim = null;
    // Start is called before the first frame update
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemy.OnChangeType += UpdateArrow;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateArrow(BulletType type)
    {
        this.type = type;
        anim.SetTrigger("Start");
    }

    public void ChangeSprite()
    {
        switch (type)
        {
            case BulletType.leftType:
                img.sprite = leftSprite;
                break;
            case BulletType.rightType:
                img.sprite = rightSprite;
                break;
        }
    }
}
