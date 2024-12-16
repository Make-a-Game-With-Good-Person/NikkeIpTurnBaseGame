using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverDirFinder : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    public List<GameObject> Covers;
    Vector3 wallToP;
    float minDot = float.MaxValue;
    float minDistance = float.MaxValue;

    GameObject minDisCover;

    // Start is called before the first frame update
    void Start()
    {
        //CoverFind();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CoverFind();
        }
    }
    void CoverFind()
    {
        minDot = float.MaxValue;
        minDistance = float.MaxValue;

        Vector3 PToEDir = Enemy.transform.position - Player.transform.position;
        PToEDir.y = 0;
        PToEDir.Normalize();

        Vector3 wallToP = Vector3.zero;

        for (int i = 0; i < Covers.Count; i++)
        {
            Vector3 coverPos = Covers[i].transform.position;
            Vector3 playerPos = Player.transform.position;

            coverPos.y = 0;
            playerPos.y = 0;

            float distance = Vector3.Distance(playerPos, coverPos);

            // �Ÿ� �������� �켱 ����
            if (minDistance > distance)
            {
                wallToP = Covers[i].transform.forward;
                wallToP.Normalize();

                float dot = Vector3.Dot(PToEDir, wallToP);

                if (minDot > dot)
                {
                    minDot = dot;
                    minDistance = distance;
                    minDisCover = Covers[i];
                }
            }
            Debug.Log(Covers[i].gameObject.name + " , " + minDot + ", " + distance);

        }
        // minDisCover�� �����ؾ��� ����, �̰� null�̸� �׳� ���� ���� Ÿ���Ѵٴ� ����
        Debug.Log(minDisCover.name);
    }
}
