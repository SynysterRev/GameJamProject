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
    private Vector4[] lightPoints = null;
    private void Start()
    {
        lightPoints = new Vector4[30];

        Shader shader = Resources.Load<Shader>("Shaders/Post Process Effects");
        material = new Material(shader);
        material.SetColor("_Color", screenColor);
        material.SetTexture("_Pattern", pattern);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture rt0 = RenderTexture.GetTemporary(src.width, src.height);
        RenderTexture rt1 = RenderTexture.GetTemporary(src.width, src.height);
        material.SetVectorArray("__pointLight", lightPoints);
        Graphics.Blit(src, rt0, material, 1);
        Graphics.Blit(rt0, rt1, material, 2);
        Graphics.Blit(rt1, dest, material, 0);

        RenderTexture.ReleaseTemporary(rt0);
        RenderTexture.ReleaseTemporary(rt1);
    }

    // public void
}
