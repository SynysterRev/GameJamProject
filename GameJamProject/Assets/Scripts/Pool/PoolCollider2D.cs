using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class PoolCollider2D : MonoBehaviour
{

    #region Public Fields



    #endregion


    #region Private Fields



    #endregion


    #region Accessors



    #endregion


    #region MonoBehaviour Methods

    private void OnTriggerEnter2D(Collider2D other)
    {
        PoolingObject pObjt = other.transform.GetComponent<PoolingObject>();

        if (pObjt)
        {
            PoolManager.Instance.Add(other.transform.gameObject);
        }
    }

    #endregion


    #region Private Methods



    #endregion


    #region Public Methods



    #endregion


}
