using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pitcher_stats", menuName = "ScriptableObject/pitcher_stats", order = 2)]
public class Pitcher_Stats : ScriptableObject
{
    //stats
    //현재 투수의 stat을 세부적으로 구성하고 다루기엔 시간이 많이 부족할 것 같다.
    //따라서, 각각의 stats은 성적을 만들어 내는 데에만 사용될 것.
    // stat : current stats | p_stats : potential stats
    [SerializeField]
    private int control = 100; // 제구 --> 존에 들어오는 비율 결정.(빠지는 공, 걸치는 공, 몰린 공)
    [SerializeField]
    private int pControl = 100;
    [SerializeField]
    private int speed = 100; // 구속 --> 장타확률에 영향 + 
    [SerializeField]
    private int pSpeed = 100;
    [SerializeField]
    private int stamina = 100; //스태미너(체력) --> 투수의 투구수에 따른 능력치 저하.
    [SerializeField]
    private int pStamina = 100;
    [SerializeField]
    private bool hand = false; // false = right , true = left

    // Constructor
    public Pitcher_Stats()
    {
        pSpeed = Random.Range(20, 101);
        pStamina = Random.Range(20, 101);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0,pControl + 1);
        stamina = Random.Range (0,pStamina + 1);

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;
    }
    public void ReuseData()
    {
        pSpeed = Random.Range(20, 101);
        pStamina = Random.Range(20, 101);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0, pControl + 1);
        stamina = Random.Range(0, pStamina + 1);

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;
    }
}
