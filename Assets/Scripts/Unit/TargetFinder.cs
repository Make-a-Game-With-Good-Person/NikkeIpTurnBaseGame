using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder
{
    BattleManager owner;
    Vector2Int targetPos;
    LayerMask completeLayerMask = 10;
    int MAX_COUNT = 2;

    public TargetFinder(BattleManager owner)
    {
        this.owner = owner;
    }

    bool RayRecursive(Vector3 origin, Vector3 dir, int count, Unit target)
    {
        if (count > MAX_COUNT)
            return false; // �ִ� ���̿� ���������Ƿ� ���� ó��

        if (Physics.Raycast(origin, dir, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == target.gameObject)
            {
                // Ÿ�� ������ ����ٸ� ����
                return true;
            }

            if (hit.transform.gameObject.layer == completeLayerMask)
            {
                // ���� ������ �¾��� ��, ���ο� ��������� ��� ȣ��
                Vector3 newOrigin = hit.point + dir * 1f; // �ణ �������� �̵�
                return RayRecursive(newOrigin, dir, count + 1, target);
            }
        }

        // Ÿ���� ������ ���� ���
        return false;
    }


    public HashSet<Vector2Int> FindTarget(HashSet<Vector2Int> set)
    {
        foreach (Unit unit in owner.Units)
        {
            targetPos = owner.tileManager.GetTile(unit.transform.position).coordinate;
            if (!set.Contains(targetPos)) continue;

            if (unit.gameObject.layer == 8)
            {
                Vector3 dir = unit.rayEnter.transform.position - owner.curControlUnit.rayPointer.transform.position;
                dir.Normalize();

                if (RayRecursive(owner.curControlUnit.rayPointer.position, dir, 0, unit))
                {
                    Debug.Log("Ÿ�� ������ ���������� ����");
                }
                else
                {
                    Debug.Log("Ÿ���� ����, ����Ʈ���� ����");
                    set.Remove(targetPos);
                }
            }
        }

        return set;
    }
}
