using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected BattleManager owner;
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
    protected virtual void Awake()
    {
        owner = GetComponent<BattleManager>();
    }
    #endregion
}
