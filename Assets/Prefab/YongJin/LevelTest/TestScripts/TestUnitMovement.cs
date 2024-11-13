using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitMovement : MonoBehaviour
{

    private Vector3 targetPositon;
    [SerializeField] private GridSystem gridSystem;
    
    private List<GridNode> path = new List<GridNode>();
    public event EventHandler OnArrive;
    private int pathPointindex;
    float moveSpeed = 4f;
    private void Start() 
    {
        gridSystem.OnSecondSelectedPoint += GridSystem_OnMovePosition;  
        pathPointindex = 0; 
        
    }
    private void Update() 
    {
        if(path.Count != 0) Move();
    }
    private void GridSystem_OnMovePosition(object sender, GridSystem.OnSecondSelectedPointArg e)
    {
        
        //Debug.Log(e.path[pathPointindex].position);
        path = e.path;
        targetPositon = path[pathPointindex].position;
    }
    private void Move()
    {
        //이동 :방향(dir), Time.deltatiem, speed
        Debug.Log(pathPointindex);
        Vector3 dir = targetPositon - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed);

        //도착판정
        float distance = Vector3.Distance(transform.position, targetPositon);
        if (distance < 0.2f)
        {
            SetNextTarget();
        }
    }
    private void SetNextTarget()
    {
        if (pathPointindex == path.Count - 1)
        {
            path.Clear();
            pathPointindex = 0; 
            OnArrive?.Invoke(this, EventArgs.Empty);
            return;
        }

        pathPointindex++;
        targetPositon = path[pathPointindex].position;
    }
}
