using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    [Header("ī�޶� ���� ���"), Space(0.5f)]
    public Transform target; // ī�޶� ���� ���

    [Header("ī�޶� ��� Ÿ���� ����� ��ġ"), Space(0.5f)]
    public Transform targetShoulder; // ī�޶� ��� Ÿ���� ����� ��ġ

    [Header("Ÿ���� �ٶ󺸰� �ִ� ���"), Space(0.5f)]
    public Transform lookTarget; // ���� Ÿ���� �ٶ󺸰� �ִ� Ÿ��

    public Vector3 quarterViewoffset = new Vector3(3, 5, -5);
    public Vector3 quarterViewrotationOffset = new Vector3(45, -30, 0);  // ���� offset (Pitch, Yaw, Roll)

    //public Vector3 shoulderViewOffset = new Vector3(1, 1, 5);
    public Vector3 shoulderViewrotationOffset = new Vector3(0, 0, 0);  // ���� offset (Pitch, Yaw, Roll)

    public float transitionSpeed = 2f;
    public float dragSpeed = 15f;
    public float rotationSpeed = 4f;

    public bool isDragging;

    public LayerMask layerMask;

    // ���� ��ü ĳ��, ������ �߰��� �� ���⿡ State�� �����ϰ� �Ʒ����� new�� ������ �� Switch�Լ��� �߰��� ���� ��, 
    // �������� ī�޶� ���� ��ȯ ������ �Ʒ� State��� ���� �߰��ϸ� �ȴ�. 
    ICameraState quarterViewState;
    ICameraState shoulderViewState;
    ICameraState mapViewState;
    ICameraState currentState;

    BattleManager owner;
    

    // Start is called before the first frame update
    void Start()
    {
        // ���� ��ü�� �ѹ��� �����صΰ� ����
        quarterViewState = new QuarterViewState();
        shoulderViewState = new ShoulderViewState();
        mapViewState = new MapViewState();
        owner = FindObjectOfType<BattleManager>();
        currentState = quarterViewState; // �ʱ� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (owner.curState == BATTLESTATE.NONE || owner.curState == BATTLESTATE.INIT) return;
        currentState?.UpdateState(this);

        // �� �Ʒ� �ڵ�� �� ���� ����
        //if(owner.curState == BATTLESTATE.UNITSELECT)
        // �� �ѷ����� ���� ��ȯ
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ��(��ġ) �̺�Ʈ
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (((1 << hit.collider.gameObject.layer) & layerMask) != 0)
                {
                    /*isDragging = false;
                    //SetCamTarget(hit.collider.transform);
                    //owner.selectedTarget = this.target;
                    SwitchToQuaterView(hit.collider.transform);*/
                }
                else
                {
                    SwitchToMapView();
                }
            }
        }
    }

    /*public void SetLockOnTarget(Transform _lockOnTarget)
    {
        if (_lockOnTarget == null) return;
        shoulderViewrotationOffset = Vector3.zero;
        else enemyShoulderViewrotationOffset.y = 180f;

        Vector3 lockOnVec = _lockOnTarget.position - target.position;
        lookRot = Vector3.Dot(lockOnVec, target.transform.forward);
        if (playerPhase) shoulderViewrotationOffset.y -= lookRot;
        else enemyShoulderViewrotationOffset.y -= lookRot;

        Vector3 rightVec = Vector3.Cross(target.forward, Vector3.up); // ����
        float dotRight = Vector3.Dot(rightVec, lockOnVec); // ����

        if (dotRight > 0)
        {
            // �����ʿ� ���� ��
            shoulderViewrotationOffset.y -= Mathf.Abs(dotRight);

        }
        else
        {
            // ���ʿ� ���� ��
            shoulderViewrotationOffset.y += Mathf.Abs(dotRight);
        }

        Debug.Log(dotRight);

    }*/

    public void SetCamTarget(Transform _target)
    {
        this.target = _target;
    }

    void SetLookTarget(Transform _target)
    {
        this.lookTarget = _target;
    }

    public void SwitchToShoulderView(Transform shoulder, Transform _target)
    {
        SetLookTarget(_target);
        this.transform.parent = shoulder;
        this.transform.localPosition= Vector3.zero;
        currentState = shoulderViewState;
    }

    public void SwitchToMapView()
    {
        this.transform.parent = null;
        currentState = mapViewState;
    }

    public void SwitchToQuaterView(Transform target)
    {
        SetCamTarget(target);
        this.transform.parent = null;
        currentState = quarterViewState;
    }

    
}
