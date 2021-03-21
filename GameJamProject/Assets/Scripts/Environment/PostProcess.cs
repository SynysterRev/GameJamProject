using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    [SerializeField]
    private Color screenColor = Color.white;
    [SerializeField]
    private Color bckColor = Color.black;
    [SerializeField]
    private Texture pattern = null;
    [SerializeField]
    private Camera EffectCam = null;
    private Material material;
    private float radius = 0.0f;
    private void Start()
    {
        Shader shader = Resources.Load<Shader>("Shaders/Post Process Effects");
        material = new Material(shader);
        material.SetColor("_Color", screenColor);
        material.SetColor("_ColorBck", bckColor);
        material.SetTexture("_Pattern", pattern);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture rt0 = RenderTexture.GetTemporary(src.width, src.height);
        RenderTexture rt1 = RenderTexture.GetTemporary(src.width, src.height);

        material.SetTexture("_Distortion", EffectCam.targetTexture);

        Graphics.Blit(src, rt0, material, 1);
        Graphics.Blit(rt0, rt1, material, 2);
        Graphics.Blit(rt1, rt0, material, 3);
        Graphics.Blit(rt0, dest, material, 0);

        RenderTexture.ReleaseTemporary(rt0);
        RenderTexture.ReleaseTemporary(rt1);
    }

    public void StartEffect()
    {
        StartCoroutine(CoroutineEffect(1.0f));
    }

    public void StopEffect()
    {
        StartCoroutine(CoroutineEffect(0.0f));
    }
    private IEnumerator CoroutineEffect(float _finalValue)
    {
        do
        {
            Debug.Log(radius);
            radius = Mathf.Lerp(radius, _finalValue, 0.1f);
            material.SetFloat("_Radius", radius);
            yield return null;

        } while (Mathf.Abs(_finalValue - radius) < 0.1f);
    }
}
