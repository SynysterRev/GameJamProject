using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCanvas : MonoBehaviour
{
    [SerializeField]
    Button back = null;
    // Start is called before the first frame update
    void Start()
    {
        back.onClick.AddListener(delegate { TransitionLevel.Instance.StartTransition("Menu"); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
