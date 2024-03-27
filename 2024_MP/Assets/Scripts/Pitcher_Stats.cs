using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pitcher_stats", menuName = "ScriptableObject/pitcher_stats", order = 2)]
public class Pitcher_Stats : ScriptableObject
{
    //stats
    //현재 투수의 stat을 세부적으로 구성하고 다루기엔 시간이 많이 부족할 것 같다.
    //따라서, 각각의 stats은 성적을 만들어 내는 데에만 사용될 것.
    [SerializeField]
    private int control = 100; // 제구 --> 존에 들어오는 비율 결정.(빠지는 공, 걸치는 공, 몰린 공)
    [SerializeField]
    private int pControl = 100; // 제구 포텐셜
    [SerializeField]
    private int speed = 100; // 구속 --> 장타확률에 영향 + 
    [SerializeField]
    private int pSpeed = 100; // 구속 포텐셜
    [SerializeField]
    private int stamina = 100; //스태미너(체력) --> 투수의 투구수에 따른 능력치 저하.
    [SerializeField]
    private int pStamina = 100; // 체력 포텐셜
    [SerializeField]
    private bool hand = false; // false = right , true = left
    [SerializeField]
    private int age = 20; //현재 나이
    [SerializeField]
    private int g_age = 30; // 성장할 수 있는 나이(?) 이 나이 이후로는 성장이 거의 없다시피 할 것.
    [SerializeField]
    private int c_age = 37; // 에이징 커브가 시작 될 나이. 이때부터 능력치가 감소하기 시작한다.


    // Constructor
    public Pitcher_Stats()
    {
        pSpeed = Random.Range(20, 101);
        pStamina = Random.Range(20, 101);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0,pControl + 1);
        stamina = Random.Range (0,pStamina + 1);

        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;

        if (Random.Range(0, 100) > 90) age = 23;
        else age = 20;
    }
    public void ReuseData()
    {
        pSpeed = Random.Range(20, 101);
        pStamina = Random.Range(20, 101);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0, pControl + 1);
        stamina = Random.Range(0, pStamina + 1);

        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;

        if (Random.Range(0, 100) > 90) age = 23;
        else age = 20;
    } 
    //both Pitcher_Stats and ReuseData are use for make new pitchers but one is constructor and 
    //other is for reuse Pitcher_Stats() class objects if pitcher's career is end. instead delete data.
    

}
