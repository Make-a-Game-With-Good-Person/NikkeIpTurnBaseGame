using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUICamController : MonoBehaviour
{
    [Header("스쿼드 창에서 선택한 대상"), Space(0.5f)]
    public Transform target; // 카메라가 찍을 대상

    [Header("기본 촬영 위치, 센터"), Space(0.5f)]
    public Transform defaultTarget; // 카메라 대상 타겟의 숄더뷰 위치

    public Vector3 quarterViewoffset;
    public Vector3 quarterViewrotationOffset;  // 각도 offset (Pitch, Yaw, Roll)
    public Vector3 defaultViewrotationOffset;  // 각도 offset (Pitch, Yaw, Roll)

    public float transitionSpeed = 2f;
    public float rotationSpeed = 4f;

    IUICameraState currentState;
    IUICameraState squadSelectedViewState;
    IUICameraState defaultViewState;

    // Start is called before the first frame update
    void Start()
    {
        // 상태 객체를 한번만 생성해두고 재사용
        defaultViewState = new UIViewState();
        squadSelectedViewState = new UISelectedViewState();
        
        currentState = defaultViewState; // 초기 상태 설정
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void SetCamTarget(Transform _target)
    {
        this.target = _target;
    }

    public void SwitchToSelectedView(Transform target)
    {
        SetCamTarget(target);
        this.transform.parent = null;
        currentState = squadSelectedViewState;
    }

    public void SwitchToDefaultView()
    {
        this.transform.parent = null;
        currentState = defaultViewState;
    }
}
