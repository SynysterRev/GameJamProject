using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void DelegateFire(int nbBullet);
    public event DelegateFire OnFire;
    public delegate void DelegateReload(int nbBullet);
    public event DelegateReload OnReloading;

    [SerializeField]
    private Grid grid = null;
    [SerializeField]
    private float speed = 1.0f;
    [Header("Bullets")]
    [SerializeField]
    private GameObject prefabBullet = null;
    [SerializeField]
    private int numberMaxBullets = 7;
    [SerializeField]
    private float timerBeforeReload = 3.0f;
    [SerializeField]
    private float timerBetweenReload = 0.2f;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private GameObject dustPrefab = null;
    private float timerFireRate = 0.0f;
    private int numberBullets = 7;
    private float timerBullet = 0.0f;
    private bool startTimer = false;
    private bool isReloading = false;
    //movement
    private float lerp = 0.0f;
    private int currentGrid = 0;
    private int nextIndex = 0;
    private bool move = false;
    private Vector2[] posGrid = new Vector2[4];
    private Vector2 posBeforeMov = Vector2.zero;
    private ShakeEffect shakeEffect = null;
    private Number[] numbers = null;
    private Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        numbers = GameObject.FindObjectsOfType<Number>();
        shakeEffect = GameObject.FindObjectOfType<ShakeEffect>();
        posGrid[0] = transform.position;
        for (int i = 1; i < posGrid.Length; ++i)
        {
            posGrid[i] = posGrid[i - 1] + Vector2.right * grid.cellSize.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
        Move();
        InputFire();
        ReloadBullet();
    }
    private void InputMovement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentGrid != 0)
        {
            numbers[3].StartAnimation();
            posBeforeMov = transform.position;
            lerp = 0.0f;
            nextIndex = 0;
            move = true;
            ChangeAnim();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentGrid != 1)
        {
            numbers[2].StartAnimation();
            posBeforeMov = transform.position;
            lerp = 0.0f;
            nextIndex = 1;
            move = true;
            ChangeAnim();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentGrid != 2)
        {
            numbers[1].StartAnimation();
            posBeforeMov = transform.position;
            lerp = 0.0f;
            nextIndex = 2;
            move = true;
            ChangeAnim();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && currentGrid != 3)
        {
            numbers[0].StartAnimation();
            posBeforeMov = transform.position;
            lerp = 0.0f;
            nextIndex = 3;
            move = true;
            ChangeAnim();
        }
    }

    private void ChangeAnim()
    {
        if (currentGrid < nextIndex)
        {
            anim.SetTrigger("right");
            GameObject go = GameObject.Instantiate(dustPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            anim.SetTrigger("left");
            GameObject go = GameObject.Instantiate(dustPrefab, transform.position, Quaternion.identity);
            go.transform.localScale = new Vector2(-1.0f, 1.0f);
        }
    }
    private void Move()
    {
        if (!move) return;
        lerp = Mathf.Clamp(lerp + Time.deltaTime * speed, 0.0f, 1.0f);
        transform.position = Vector2.Lerp(posBeforeMov, posGrid[nextIndex], lerp);
        if (lerp == 1.0f)
        {
            currentGrid = nextIndex;
            move = false;
            anim.SetTrigger("idle");
        }
    }

    private void InputFire()
    {
        if (move || isReloading) return;
        if (timerFireRate > 0.0f)
            timerFireRate -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Fire(BulletType.blue);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Fire(BulletType.red);
        }
    }

    private void Fire(BulletType bulletType)
    {
        if (numberBullets > 0 && timerFireRate <= 0.0f)
        {
            shakeEffect.StartEffect();

            numberBullets--;
            if (OnFire != null)
                OnFire(numberBullets);
            timerBullet = 0.0f;
            startTimer = true;
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullets>().LaunchProjectile(bulletType);
            timerFireRate = fireRate;
        }
        else
        {
            //jouer un son
        }
    }
    private void ReloadBullet()
    {
        if (startTimer)
        {
            timerBullet += Time.deltaTime;
            if (timerBullet >= timerBeforeReload)
            {
                startTimer = false;
                StartCoroutine(Reload());
            }
        }
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        do
        {
            numberBullets++;
            if (OnReloading != null)
                OnReloading(numberBullets);
            yield return new WaitForSeconds(timerBetweenReload);
        } while (numberBullets < numberMaxBullets);
        isReloading = false;
    }
}
