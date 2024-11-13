using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Stat : MonoBehaviour
{
    #region Properties
    #region Private
    [SerializeField]private float[] _stat = new float[(int)EStatType.Count];
    #endregion
    #region Protected
    #endregion
    #region public
    public float this[EStatType i]
    {
        get { return _stat[(int)i]; }
        set { _stat[(int)i] = value; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
