using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선수 데이터가 가져야할 것
/// 1. 선수들의 stats
/// 2. 선수들의 records
/// </summary>

// 수비 포지션 알아보기 쉽게, 각 1루 2루 유격 3루 포수 좌익 중견 우익 순
public enum def_position { B1, B2, SS, B3, C, LF, CF, RF};

[CreateAssetMenu(fileName = "batter_stats",menuName = "ScriptableObject/batter_stats",order = 3)]
public class Batter_Stats : ScriptableObject
{
    //stats
    //normal naming : current stats | p_ : potential of stats
    [SerializeField]
    private int power = 100; // 파워 : 장타확률
    [SerializeField]
    private int p_power = 100;
    [SerializeField]
    private int speed = 100; // 스피트 : 주루, 도루
    [SerializeField]
    private int p_speed = 100;
    [SerializeField]
    private int defense = 100; // 수비 : 실책, 호수비 등
    [SerializeField]
    private int contact = 100; // 컨택 : 안타 등
    [SerializeField]
    private int p_contact = 100;
    [SerializeField]
    private int eye = 100; //선구안 : 공을 골라내는 능력
    [SerializeField]
    private int p_eye = 100;
    [SerializeField]
    private int intelligence = 100; //지능 : 훈련 시 상승량, 투수와의 수싸움
    [SerializeField]
    private int p_intelligence = 100;
    [SerializeField]
    private bool hand = false; // false = right , true = left

    // 포지션 stats는 각 포지션 별 능력치, 같이 묶는게 좋을 수도 있긴 한데, 일단은 분리
    // 일단 유격 - 2루(연관성 크다)
    //      3루 - 1루
    //      중견 - 좌/우익
    // 1루는 왠만하면 다들 적당히 한다.
    // 유격수로 뛰어나면, 3루, 외야에서도 왠만큼 한다.
    // 2루수 또한 외야 컨버젼이 가능하다.
    // 수비능력치(defense 0 ~ 100) 기준 사람같이 한다는 1루수 20정도? 좌익/우익은 50정도?
    // 3루 55, 포수 - 포수출신 아니면 거의 가능한 사람이 없는 걸로. 선천적으로 해본 적 없으면
    // 2루/ 중견 70? 유격 80 --> 이후 실 능력치에 따라 포지션 수비 훈련 효율이 달라지도록/
    private def_position main_pos;
    private def_position[] sub_pos;
    private int pos_stats_main;
    private int[] pos_stats_sub;
    // Constructor
    public Batter_Stats()
    {
        p_power = Random.Range(20, 101); 
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        power = Random.Range(0, p_power);
        speed = Random.Range(0,p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(20, 101);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;
    }
    public void ReuseData()
    {
        p_power = Random.Range(20, 101);
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_defense = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        power = Random.Range(0, p_power);
        speed = Random.Range(0, p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(0, p_defense);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;
    }
}
