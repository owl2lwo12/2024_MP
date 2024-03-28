using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "batter_stats",menuName = "ScriptableObject/batter_stats",order = 3)]
public class Batter_Stats : ScriptableObject
{
    //stats
    //normal naming : current stats | p_ : potential of stats
    [SerializeField]
    private int age = 20; //현재 나이
    [SerializeField]
    private int g_age = 30; // 성장할 수 있는 나이(?) 이 나이 이후로는 성장이 거의 없다시피 할 것.
    [SerializeField]
    private int c_age = 37; // 에이징 커브가 시작 될 나이. 이때부터 능력치가 감소하기 시작한다.
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
    private int p_defense = 100;
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

    // Constructor
    public Batter_Stats()
    {
        p_power = Random.Range(20, 101); 
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_defense = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        power = Random.Range(0, p_power);
        speed = Random.Range(0,p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(0, p_defense);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;

        if (Random.Range(0, 100) > 90) age = 23; //대졸 draft
        else age = 20; //고졸 draft
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

        if (Random.Range(0, 100) > 90) age = 23; //대졸 draft
        else age = 20; //고졸 draft
    }

    public void Retirement() //선수 은퇴 ---> 비활성화 할 것
    {

    }

    public void Aged()
    {
        age++;

        if (age <= g_age) //성장할 수 있는 나이
        {

        }
        else if (age <= c_age) //성장이 멈추는 나이
        {

        }
        else //에이징 커브가 시작되는 나이
        {

        }
    }

}
