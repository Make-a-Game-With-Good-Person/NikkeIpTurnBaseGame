using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoverDirFinder
{
    public GameObject Player; // curControlUnit
    public GameObject Enemy; // selectedTarget

    //public List<Cover> Covers; // �� Ÿ�Ϻ��� ������ �յ��¿� ���󹰵�
    public Cover[] Covers = new Cover[4];
    Vector3 wallToP;
    float minDot = float.MaxValue;

    Cover minDisCover;

    // 4���� ���͸� �迭�� ����
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
            // �̰� �������� ��
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
        // minDisCover�� �����ؾ��� ����, �̰� null�̸� �׳� ���� ���� Ÿ���Ѵٴ� ����
        Debug.Log(minDisCover.name);
    }
*/

    /// <summary>
    /// �� �Լ��� ���� ���� �ٶ󺸸� ������ �� ���ֺ��� �ִ� ������ ã�� �� ����
    /// �����Ұ� Covers�� �ε��� ������ forward, back, right, left�ε� �� ������ ����� ������ �������� ��
    /// ���� ������ ��������� �տ� �ִ°� forward�� �ڿ� �ִ°� back��, transform ������ �ƴϱ⶧���� ���� �ٶ󺸴� ������� �ٸ�
    /// Ÿ���� Cover�� ����Ʈ�� ���� �ڷᱸ���� ������ �� �� �ε��� ������ ���Ѽ� �����ؾ� ����ϴµ� ������ ���� ��
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
