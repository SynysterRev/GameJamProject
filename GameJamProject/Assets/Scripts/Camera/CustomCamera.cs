using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [Header("Shakiness Effect")]
    [SerializeField]
    protected float shakinessPos = 0.14f;
    [SerializeField]
    protected float shakinessRot = 0.14f;
    [SerializeField]
    protected float time = 0.15f;
    private Vector2 direction = Vector2.zero;
    private float rotationZ = 0.0f;
    private bool isShaking = false;

    public Vector2 Direction { get => direction; set => direction = value; }

    public void Shake(float _positionFactor, float _rotationFactor, float _time)
    {
        if (!isShaking)
            StartCoroutine(ShakeCoroutine(_positionFactor, _rotationFactor, _time));
    }

    public void Shake()
    {
        if (!isShaking)
            StartCoroutine(ShakeCoroutine(shakinessPos, shakinessRot, time));
    }

    private IEnumerator ShakeCoroutine(float _positionFactor, float _rotationFactor, float _time)
    {
        float time = 0.0f;
        isShaking = true;
        do
        {
            Vector3 newPos = transform.position;
            newPos.x += Random.Range(-_positionFactor, _positionFactor);
            newPos.y += Random.Range(-_positionFactor, _positionFactor);

            rotationZ += Random.Range(-_rotationFactor, _rotationFactor);
            //move with random position
            transform.position = newPos;

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        } while (time < _time);
        isShaking = false;
    }
}
