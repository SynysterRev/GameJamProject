using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimHitScript : MonoBehaviour
{
    [SerializeField]
    private HUDEnemy enemy;
    private void OnMiddleAnim()
    {
        enemy.ChangeSprite();
    }
}
