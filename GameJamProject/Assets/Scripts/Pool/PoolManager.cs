using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Util;

public class PoolManager : Singleton<PoolManager>
{

    #region Public Fields

    #endregion


    #region Private Fields
    [SerializeField]
    private int maxSize = 100;
    private Stack<GameObject> pool = null;
    private Dictionary<GameObject, Stack<GameObject>> pools;
    #endregion


    #region Accessors


    #endregion


    #region MonoBehaviour Methods
    private new void Awake()
    {
        pools = new Dictionary<GameObject, Stack<GameObject>>();
    }
    #endregion


    #region Private Methods
    #endregion


    #region Public Methods
    public GameObject Get(GameObject _prefab, Vector3 _position, Quaternion _rotation, Transform parent = null)
    {
        GameObject go = null;
        if (pools.TryGetValue(_prefab, out pool))
        {
            if (pool.Count > 0)
            {
                go = pool.Pop();
                go.SetActive(true);
                go.transform.position = _position;
                go.transform.rotation = _rotation;
                go.transform.SetParent(parent);
            }
        }
        if (go == null)
        {
            go = GameObject.Instantiate(_prefab, _position, _rotation, parent);
            PoolingObject poolObject = go.AddComponent<PoolingObject>();
            poolObject.PrefabReference = _prefab;
        }
        return go;
    }

    public void Add(GameObject _gameObject)
    {
        GameObject prefab = _gameObject.GetComponent<PoolingObject>().PrefabReference;

        if (prefab)
        {
            if (pools.TryGetValue(prefab, out pool))
            {
                if (pool.Count < maxSize)
                {
                    _gameObject.SetActive(false);
                    pool.Push(_gameObject);
                }
                else Destroy(_gameObject);
            }
            else
            {
                Stack<GameObject> pool = new Stack<GameObject>();
                _gameObject.SetActive(false);
                pool.Push(_gameObject);
                pools.Add(prefab, pool);
            }
        }
    }
    #endregion


}
