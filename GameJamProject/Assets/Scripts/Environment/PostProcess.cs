using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    [SerializeField]
    private Color screenColor = Color.white;
    [SerializeField]
    private Texture pattern = null;
    private Material material;
    private int MaxLight = 50;
    private void Start()
    {
        Shader shader = Resources.Load<Shader>("Shaders/Post Process Effects");
        material = new Material(shader);
        material.SetColor("_Color", screenColor);
        material.SetTexture("_Pattern", pattern);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture rt = RenderTexture.GetTemporary(src.width, src.height);
        Graphics.Blit(src, rt, material, 1);
        Graphics.Blit(rt, dest, material, 0);
        RenderTexture.ReleaseTemporary(rt);
    }
}
