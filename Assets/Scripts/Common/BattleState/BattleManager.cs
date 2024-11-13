using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    [HideInInspector] public StateMachine stateMachine;
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
    private void Awake()
    {
        stateMachine = gameObject.AddComponent<StateMachine>();
    }
    protected virtual void Start()
    {
        stateMachine.ChangeState<InitBattleState>();
    }
    #endregion
}
