using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "team_template", menuName = "ScriptableObject/team_template", order = 4)]
public class Team_Scripts : ScriptableObject
{
    private static int max_player = 65; // kbo������ ������ ������ �ִ� ��� �ο��� 65������ �����ȴ�.
    [SerializeField]
    private int current_player = 59; // ���� ���ܿ� �����ϴ� ������ ��

    public void Waiver() // ��ġ�� ���ٰ� �򰡵Ǵ� ���� �ذ�. ������ �� ��ܿ��� ���� ��Ȱ��ȭ �� ��.
    {

    }



}
