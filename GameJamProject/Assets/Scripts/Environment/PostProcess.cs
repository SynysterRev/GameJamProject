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
    private bool theWorld = false;
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
        RenderTexture lastRt = rt0;
        material.SetTexture("_Distortion", EffectCam.targetTexture);

        Graphics.Blit(src, lastRt, material, 1);
        lastRt = rt0;

        Graphics.Blit(lastRt, rt1, material, 2);
        lastRt = rt1;

        if (theWorld)
        {
            Graphics.Blit(lastRt, rt0, material, 3);
            lastRt = rt0;
        }

        Graphics.Blit(lastRt, dest, material, 0);

        RenderTexture.ReleaseTemporary(rt0);
        RenderTexture.ReleaseTemporary(rt1);
    }

    public void StartEffect()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineEffect(1.0f));
    }

    public void StopEffect()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineEffect(0.0f));
    }
    private IEnumerator CoroutineEffect(float _finalValue)
    {
        do
        {
            theWorld = true;
            radius = Mathf.Lerp(radius, _finalValue, 0.01f);
            material.SetFloat("_Radius", radius);
            yield return null;

        } while (Mathf.Abs(_finalValue - radius) > 0.01f);

        if (_finalValue < 0.5f)
        {
            theWorld = false;
        }
    }
}
