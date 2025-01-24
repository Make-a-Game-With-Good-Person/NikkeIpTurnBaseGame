using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.CanvasScaler;

public class BossSummonSkill : UnitSkill
{
    public List<Transform> summonPoints;
    public List<Unit> monsters;
    [SerializeField] int coolTime;
    public int curCoolTime = 0;
    bool coolCheck = false;
    void CoolTimeCheck()
    {
        if (!coolCheck) return;
        curCoolTime++;
        if (curCoolTime >= coolTime)
        {
            coolCheck = false;
            curCoolTime = 0;
        }
    }

    protected override void Start()
    {
        base.Start();

        battleManager.RoundEndEvent.AddListener(CoolTimeCheck);
    }

    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }
    IEnumerator SkillAction()
    {
        coolCheck = true;
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� ���� ��ȯ ��ų �ߵ�!");
        //battleManager.curControlUnit.animator.SetTrigger("Summon");
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(SummonMonsters()); // ��ȯ�� �����°� ��ٸ���.

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� ��ȯ ��");
        changeStateWhenActEnd?.Invoke();
    }

    IEnumerator SummonMonsters()
    {
        yield return null;
        GameObject obj = null;

        for(int i = 0; i < summonPoints.Count; i++)
        {
            Tile tile = battleManager.tileManager.GetTile(summonPoints[i].position);
            int rnd = Random.Range(0, 3);
            if((tile.tileState & TileState.Placeable) > 0) // �ش� ��ġ�� ���� �� �ִٸ�
            {
                obj = SummonMonster(monsters[rnd], summonPoints[i].position, tile);
            }
            else // �ش� ��ġ�� ������ ���ٸ�
            {
                Vector3 placablePos = FindPlacableCoordinate(summonPoints[i].position); // �ֺ� Ÿ�Ͽ��� ���� �� �ִ� ��ġ�� ã��
                if (placablePos == Vector3.zero) continue; // ���� �ֺ� 8������ �� ���������� �׳� ��ȯ������

                tile = battleManager.tileManager.GetTile(placablePos);
                obj = SummonMonster(monsters[rnd], placablePos, tile);
            }

            battleManager.cameraStateController.SwitchToQuaterView(obj.transform);
            yield return new WaitForSeconds(0.3f);
        }
    }

    GameObject SummonMonster(Unit monster, Vector3 pos, Tile tile)
    {
        GameObject obj = Instantiate(monster.gameObject, pos, Quaternion.identity, null);

        monster.transform.position = tile.center;
        //���⿡ owner�� ������ ����ϴ� �ڵ� �־����
        tile.Place(monster);
        battleManager.EnemyUnits.Add(monster);
        return obj;
    }

    /// <summary>
    /// ���� pos ��ġ�� ���� �־� �� ��ġ�� ��ȯ�� �� ���ٸ� ��ȯ ������ �ֺ� Ÿ���� ã�� �Լ�
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Vector3 FindPlacableCoordinate(Vector3 pos)
    {
        Vector3 placablePos = Vector3.zero;
        bool placable = false;

        // �ֺ� 8���� üũ
        int[] dx ={ 1, 0, -1, 0, -1, -1, 1, 1 };
        int[] dz = { 0, 1, 0, -1, 1, -1, 1, -1 };

        for(int dir =0; dir < 8; dir++)
        {
            float nx = pos.x + dx[dir];
            float nz = pos.y + dz[dir];

            placablePos = placablePos + new Vector3(nx,0, nz);
            if ((battleManager.tileManager.GetTile(placablePos).tileState & TileState.Placeable) > 0) // �ش� ��ġ�� ���� �� �ִٸ�
            {
                placable = true;
                break;
            }
        }

        if (!placable) return Vector3.zero;
        else return placablePos;
    }
}
