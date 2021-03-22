using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorScreen : MonoBehaviour
{
    [SerializeField]
    private Color[] colors = null;
    private Button btn = null;
    private int currentId = 0;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeColor);
    }

    private void ChangeColor()
    {
        PostProcess pp = Camera.main.GetComponent<PostProcess>();
        currentId = (currentId + 1) % colors.Length;
        pp.ChangeColor(colors[currentId]);
    }
}
