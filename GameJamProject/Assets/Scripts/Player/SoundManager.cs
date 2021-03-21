
using UnityEngine;
using System.Collections;
using DG.Util;
using System;

public class SoundManager : Singleton<SoundManager>
{

    #region Public Fields


    #endregion



    #region Private Fields


    [SerializeField]
    private AudioSource MusicSource = null;

    [SerializeField]
    private int sizePool = 5;

    private AudioSource[] pool;

    [SerializeField]
    private AudioClip[] audios = null;

    public AudioClip[] Audios { get => audios; }
    #endregion


    #region Accessors



    #endregion


    #region MonoBehaviour Methods
    protected override void Awake()
    {
        base.Awake();
        InitPool(sizePool);
    }
    void Start()
    {

    }


    void Update()
    {

    }

    #endregion


    #region Private Methods

    private void InitPool(int _sizePool)
    {
        pool = new AudioSource[_sizePool];
        for (int i = 0; i < _sizePool; i++)
        {
            pool[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    #endregion


    #region Public Methods

    public void PlayMusic(AudioClip clip, float _volumeMultiplier = 1f, bool _loop = true)
    {
        MusicSource.clip = clip;
        MusicSource.volume = _volumeMultiplier;
        MusicSource.loop = _loop;
        MusicSource.Play();
    }

    public void PlaySoundClip(int _idClip)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].isPlaying)
            {
                pool[i].clip = audios[_idClip];
                pool[i].time = 0f;
                pool[i].Play();
                return;
            }
        }

    }
    #endregion


}
