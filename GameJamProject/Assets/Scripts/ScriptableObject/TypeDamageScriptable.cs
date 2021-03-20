using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combos", menuName = "Assets/Data/Combos")]
public class TypeDamageScriptable : ScriptableObject
{
    public List<Combo> listCombo; 

    [System.Serializable]
    public class Combo
    {
        public BulletType[] bulletTypes;
        public int scoreValue;
    }
}
