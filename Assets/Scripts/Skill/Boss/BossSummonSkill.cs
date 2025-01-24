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
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 몬스터 소환 스킬 발동!");
        //battleManager.curControlUnit.animator.SetTrigger("Summon");
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(SummonMonsters()); // 소환이 끝나는걸 기다린다.

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 소환 끝");
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
            if((tile.tileState & TileState.Placeable) > 0) // 해당 위치에 놓을 수 있다면
            {
                obj = SummonMonster(monsters[rnd], summonPoints[i].position, tile);
            }
            else // 해당 위치에 놓을수 없다면
            {
                Vector3 placablePos = FindPlacableCoordinate(summonPoints[i].position); // 주변 타일에서 놓을 수 있는 위치를 찾아
                if (placablePos == Vector3.zero) continue; // 만약 주변 8방향이 다 막혀있으면 그냥 소환하지마

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
        //여기에 owner에 유닛을 등록하는 코드 넣어야함
        tile.Place(monster);
        battleManager.EnemyUnits.Add(monster);
        return obj;
    }

    /// <summary>
    /// 만약 pos 위치에 무언가 있어 그 위치에 소환할 수 없다면 소환 가능한 주변 타일을 찾는 함수
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Vector3 FindPlacableCoordinate(Vector3 pos)
    {
        Vector3 placablePos = Vector3.zero;
        bool placable = false;

        // 주변 8방향 체크
        int[] dx ={ 1, 0, -1, 0, -1, -1, 1, 1 };
        int[] dz = { 0, 1, 0, -1, 1, -1, 1, -1 };

        for(int dir =0; dir < 8; dir++)
        {
            float nx = pos.x + dx[dir];
            float nz = pos.y + dz[dir];

            placablePos = placablePos + new Vector3(nx,0, nz);
            if ((battleManager.tileManager.GetTile(placablePos).tileState & TileState.Placeable) > 0) // 해당 위치에 놓을 수 있다면
            {
                placable = true;
                break;
            }
        }

        if (!placable) return Vector3.zero;
        else return placablePos;
    }
}
