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
            return false; // 최대 깊이에 도달했으므로 실패 처리

        if (Physics.Raycast(origin, dir, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == target.gameObject)
            {
                // 타겟 유닛을 맞췄다면 성공
                return true;
            }

            if (hit.transform.gameObject.layer == completeLayerMask)
            {
                // 완전 엄폐물을 맞았을 때, 새로운 출발점으로 재귀 호출
                Vector3 newOrigin = hit.point + dir * 1f; // 약간 앞쪽으로 이동
                return RayRecursive(newOrigin, dir, count + 1, target);
            }
        }

        // 타겟을 맞추지 못한 경우
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
                    Debug.Log("타겟 유닛을 성공적으로 맞춤");
                }
                else
                {
                    Debug.Log("타겟팅 실패, 리스트에서 제거");
                    set.Remove(targetPos);
                }
            }
        }

        return set;
    }
}
