using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Pitcher_Stats : Human
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
    private int stamina = 30; //스태미너(체력) --> 투수의 투구수에 따른 능력치 저하.
    [SerializeField]
    private int cStamina = 30; //현재 잔여 스태미너
    [SerializeField]
    private bool hand = false; // false = right , true = left

    //season records
    private int era = 0;//실점
    private float inning = 0;//수행한 이닝

    // Constructor
    public Pitcher_Stats()
    {
        pSpeed = Random.Range(20, 101);
        stamina = Random.Range(20, 121);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0,pControl + 1);
        cStamina = stamina;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;
    }
    public void ReuseData()
    {
        pSpeed = Random.Range(20, 101);
        stamina = Random.Range(20, 121);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0, pControl + 1);
        cStamina= stamina;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;
    }
    // Getter
    public int getcontrol() { return control; }
    public int getstamina() { return stamina; }
    public int getspeed() { return speed; }
    public bool gethand() { return hand; }

    public int getTotal()
    {
        int tot = 0;
        tot += control + speed;
        if (tot > 170) tot *= 3; //불펜투수라서 버려지는 것을 방지
        if(stamina > 80) //선발투수 가점
        {
            tot += stamina * 2;
        }
        else
        {
            tot += stamina;
        }
        tot -= getage(); //비슷하면 나이 많은 사람이 짤리도록

        if (getinjurytype() != injury.fine) tot = -tot;

        return tot;
    } //총 stat 확인용

    public void After_Match() //일정 매치마다 실행시켜 줄 성장함수
    {
        float p;
        if (getage() <= getgage())
        {
            p = Random.Range(0, 1);
            if (control < pControl)
            {
                if (p > 0.9f) control++;
            }
            else
            {
                if (p > 0.995f) control++;
            }
            p = Random.Range(0, 1);
            if (speed < pSpeed)
            {
                if (p > 0.9f) speed++;
            }
            else
            {
                if (p > 0.995f) speed++;
            }
        }
        else if(getage() <= getcage())
        {
            p = Random.Range(0, 6);
            if (control > pControl)
                control -= (int)p/5;
            if(speed > pSpeed)
                speed -= (int)p/5;
        }
        else
        {
            p = Random.Range(-3, 1);
            control += (int)p;
            p = Random.Range(-3, 1);
            speed += (int)p;
        }
    }
    public void One_Match()
    {
        if (stamina - cStamina > 20)
        {
            cStamina += 20;
        }
        else if (stamina - cStamina <= 20)
        {
            cStamina = stamina;
        }
    }
}
