using System.Collections;
using System.Collections.Generic;
using DG.Util;
using UnityEngine;
using UnityEngine.Events;

public class PostProcess : Singleton<PostProcess>
{
    [SerializeField]
    private Color screenColor = Color.white;
    [SerializeField]
    private Color bckColor = Color.black;
    /*  [SerializeField]
      private Texture pattern = null;*/
    [SerializeField]
    private Camera EffectCam = null;
    private Material material;
    private float radius = 0.0f;
    private bool theWorldEffect = false;
    private bool transEffect = false;
    private bool isHit = false;
    private Coroutine coroutineWorld = null;

    private void Start()
    {
        Shader shader = Resources.Load<Shader>("Shaders/Post Process Effects");
        material = new Material(shader);
        material.SetColor("_Color", screenColor);
        material.SetColor("_ColorBck", bckColor);
        //     material.SetTexture("_Pattern", pattern);
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

        if (theWorldEffect)
        {
            Graphics.Blit(lastRt, (lastRt == rt0) ? rt1 : rt0, material, 3);
            lastRt = (lastRt == rt0) ? rt1 : rt0;
        }

        if (transEffect)
        {
            Graphics.Blit(lastRt, (lastRt == rt0) ? rt1 : rt0, material, 4);
            lastRt = (lastRt == rt0) ? rt1 : rt0;
        }

        if (isHit)
        {
            Graphics.Blit(lastRt, (lastRt == rt0) ? rt1 : rt0, material, 5);
            lastRt = (lastRt == rt0) ? rt1 : rt0;
        }

        Graphics.Blit(lastRt, dest, material, 0);

        RenderTexture.ReleaseTemporary(rt0);
        RenderTexture.ReleaseTemporary(rt1);
    }

    public void StartWorldEffect()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineWorldEffect(1.0f));
    }

    public void StopWorldEffect()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineWorldEffect(0.0f));
    }

    public void StartTransitionEffect(UnityAction _action)
    {
        if (coroutineWorld != null)
            StopCoroutine(coroutineWorld);
        coroutineWorld = StartCoroutine(CoroutineEffectTrans(_action));
    }

    public void StartHitEffect()
    {
        StartCoroutine(CoroutineHit());
    }

    private IEnumerator CoroutineWorldEffect(float _finalValue)
    {
        do
        {
            theWorldEffect = true;
            radius = Mathf.Lerp(radius, _finalValue, 0.01f);
            material.SetFloat("_Radius", radius);
            yield return null;

        } while (Mathf.Abs(_finalValue - radius) > 0.01f);

        if (_finalValue < 0.5f)
        {
            theWorldEffect = false;
        }
    }

    private IEnumerator CoroutineEffectTrans(UnityAction _action)
    {
        float trans = 1.0f;
        material.SetFloat("_Trans", trans);
        transEffect = true;

        do
        {
            trans = Mathf.Lerp(trans, 0.0f, 0.05f);
            material.SetFloat("_Trans", trans);
            yield return null;

        } while (trans > 0.01f);
        material.SetFloat("_Trans", 0.0f);

        if (_action != null)
        {
            _action.Invoke();
        }
        yield return new WaitForSeconds(0.2f);

        do
        {
            trans = Mathf.Lerp(trans, 1.0f, 0.05f);
            material.SetFloat("_Trans", trans);
            yield return null;

        } while (Mathf.Abs(1.0f - trans) > 0.1f);

        material.SetFloat("_Trans", 1.0f);
        transEffect = false;
    }

    private IEnumerator CoroutineHit()
    {
        isHit = true;
        yield return new WaitForSeconds(0.2f);
        isHit = false;
    }
}
