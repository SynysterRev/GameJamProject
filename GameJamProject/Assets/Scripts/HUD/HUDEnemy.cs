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
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemy.OnChangeType += UpdateArrow;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateArrow(BulletType type)
    {
        switch (type)
        {
            case BulletType.red:
                img.sprite = leftSprite;
                break;
            case BulletType.blue:
                img.sprite = rightSprite;
                break;
        }
    }
}
