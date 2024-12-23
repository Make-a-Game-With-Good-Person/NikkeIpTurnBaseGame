using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoverDirFinder
{
    public GameObject Player; // curControlUnit
    public GameObject Enemy; // selectedTarget

    //public List<Cover> Covers; // 각 타일별로 저장할 앞뒤좌우 엄폐물들
    public Cover[] Covers = new Cover[4];
    Vector3 wallToP;
    float minDot = float.MaxValue;

    Cover minDisCover;

    // 4방향 벡터를 배열에 저장
    Vector3[] directions = new Vector3[] {
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left };

    /*void CoverFind()
    {
        minDot = float.MaxValue;

        Vector3 PToEDir = Enemy.transform.position - Player.transform.position;
        PToEDir.y = 0;
        PToEDir.Normalize();

        Vector3 wallToP = Vector3.zero;

        for (int i = 0; i < Covers.Count; i++)
        {
            // 이건 내적값만 비교
            wallToP = Covers[i].transform.forward;
            wallToP.Normalize();

            float dot = Vector3.Dot(PToEDir, wallToP);

            if (minDot > dot)
            {
                minDot = dot;
                minDisCover = Covers[i];
            }
            Debug.Log(Covers[i].gameObject.name + " , " + dot);
            

        }
        // minDisCover가 공격해야할 엄폐물, 이게 null이면 그냥 적을 직접 타격한다는 느낌
        Debug.Log(minDisCover.name);
    }
*/

    /// <summary>
    /// 이 함수를 통해 적을 바라보며 공격할 때 마주보고 있는 엄폐물을 찾을 수 있음
    /// 주의할건 Covers의 인덱스 순서는 forward, back, right, left인데 이 순서는 월드상 방향을 기준으로 함
    /// 따라서 적보다 월드상으로 앞에 있는게 forward고 뒤에 있는게 back임, transform 기준이 아니기때문에 적이 바라보는 방향과는 다름
    /// 타일이 Cover를 리스트던 뭐든 자료구조로 저장할 때 이 인덱스 순서를 지켜서 저장해야 사용하는데 문제가 없을 것
    /// </summary>

    public void SetFinder(GameObject player, GameObject enemy, Cover[] covers)
    {
        this.Player = player;
        this.Enemy = enemy;
        this.Covers = covers;
    }

    public GameObject FindCover()
    {
        minDot = float.MaxValue;

        Vector3 PToEDir = (Enemy.transform.position - Player.transform.position).normalized;

        for (int i = 0; i < directions.Length; i++)
        {
            float dot = Vector3.Dot(PToEDir, directions[i]);

            if (dot < minDot)
            {
                minDot = dot;
                minDisCover = Covers[i];
            }

            Debug.Log(i + ", " + dot);

        }
        Debug.Log(minDisCover.name);

        return minDisCover.gameObject;
    }
}
