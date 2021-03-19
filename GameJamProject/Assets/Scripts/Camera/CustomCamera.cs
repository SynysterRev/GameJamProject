using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float speed = 0.1f;

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
    private Vector3 startPos = Vector2.zero;
    public Vector2 Direction { get => direction; set => direction = value; }

    private void Start()
    {
        startPos = transform.position;
    }
    void FixedUpdate()
    {
        Follow(target);
    }

    public void Follow(Transform _Target)
    {
        if (_Target)
        {

            transform.position = Vector3.Lerp(transform.position, startPos, speed);
            rotationZ = Mathf.Lerp(rotationZ, 0.0f, speed);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationZ);
        }
    }

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
