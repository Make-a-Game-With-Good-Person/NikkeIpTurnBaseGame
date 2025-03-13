using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUICamController : MonoBehaviour
{
    [Header("������ â���� ������ ���"), Space(0.5f)]
    public Transform target; // ī�޶� ���� ���

    [Header("�⺻ �Կ� ��ġ, ����"), Space(0.5f)]
    public Transform defaultTarget; // ī�޶� ��� Ÿ���� ����� ��ġ

    public Vector3 quarterViewoffset;
    public Vector3 quarterViewrotationOffset;  // ���� offset (Pitch, Yaw, Roll)
    public Vector3 defaultViewrotationOffset;  // ���� offset (Pitch, Yaw, Roll)

    public float transitionSpeed = 2f;
    public float rotationSpeed = 4f;

    IUICameraState currentState;
    IUICameraState squadSelectedViewState;
    IUICameraState defaultViewState;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ��ü�� �ѹ��� �����صΰ� ����
        defaultViewState = new UIViewState();
        squadSelectedViewState = new UISelectedViewState();
        
        currentState = defaultViewState; // �ʱ� ���� ����
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
