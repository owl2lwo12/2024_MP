using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Scripts : MonoBehaviour
{
    private static int max_player = 65; // kbo규정에 따르면 구단의 최대 등록 인원은 65명으로 구성된다.
    [SerializeField]
    private int current_player = 59; // 현재 구단에 존재하는 선수의 수

    // 시즌 등수 계산 시에 사용할 스탯
    private int win = 0;
    private int lose = 0;
    private int draw = 0;
    // 선수 명단, 투수/  타자 --> 실제 게임에서는 타자 데이터만 사용될 확률이 높다.
    [SerializeField]
    private List<GameObject> batterrplayerlist;
    [SerializeField]
    private List<GameObject> pitcherplayerlist;

    public void LineUp()
    {
        // 먼저 포지션 별로 정렬, 이후 각 포지션 별 종합 overall를 비교해서 선수를 배정할 것.
        // 포지션 3,4,5에 먼저 power(장타,홈런)가치가 높은 선수를
        // 이후 1,2에 출루,컨택 가치가 높은 선수를
        // 6,7,8,9는 각각 6 -> 9 -> 7 -> 8 순서로 오버롤 높은 선수를 배치한다.


        // 투수는 체력(stamina)일정 이상의 선수를 선발로 분류해서 가치가 높은 선수부터 5명 선정
        // 나머지 투수는 stamina 제외 능력치를 분석해서 가치가 높은 순서대로 배정.
    }
    public void Waiver() // 가치가 없다고 평가되는 선수 해고. 선수는 삭제
    {
        int t = 0;
        if (batterrplayerlist.Count >= pitcherplayerlist.Count) { //일반적으로 투수의 가치가 높으므로 같은 수면 타자를 먼저 짜른다.
            for(int i = 1; i < batterrplayerlist.Count; i++)
            {
                if (batterrplayerlist[t].GetComponent<Batter_Stats>().getTotal() > batterrplayerlist[i].GetComponent<Batter_Stats>().getTotal())
                {
                    t = i;
                }
            }
        }
        else
        {
            for(int i =1;i<pitcherplayerlist.Count;i++) {
                if (pitcherplayerlist[t].GetComponent<Pitcher_Stats>().getTotal() > pitcherplayerlist[i].GetComponent<Pitcher_Stats>().getTotal())
                {
                    t = i;
                }
            }
        }
        //pitcherplayerlist.RemoveAt(t); //만약 gameobject로 만든다면, 비활성화하는 것으로 변경할 것
    }

}
