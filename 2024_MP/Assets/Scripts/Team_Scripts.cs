using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "team_template", menuName = "ScriptableObject/team_template", order = 4)]
public class Team_Scripts : ScriptableObject
{
    private static int max_player = 65; // kbo규정에 따르면 구단의 최대 등록 인원은 65명으로 추정된다.
    [SerializeField]
    private int current_player = 59; // 현재 구단에 존재하는 선수의 수

    public void Waiver() // 가치가 없다고 평가되는 선수 해고. 선수는 팀 명단에서 빼서 비활성화 할 것.
    {

    }



}
