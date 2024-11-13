using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
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
    protected virtual void AddListeners()
    {
    }

    protected virtual void RemoveListeners()
    {
    }
    #endregion
    #region Public
    public virtual void Enter()
    {
        Debug.Log($"{this.gameObject.name}, {this.GetType()}");
        AddListeners();
    }

    public virtual void Exit()
    {
        RemoveListeners();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }
    #endregion
}
