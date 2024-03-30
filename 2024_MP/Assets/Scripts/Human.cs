using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fine은 default
//fatigue는 피로 상태로 (일정 기간동안 능력치 감소) 시간이 지나면 자동적으로 해소
//fine과 fatigue상태에서는 경기 출전이 가능하다.
//hurt의 경우 담, 발목 삠, 면도하다가 손을 베임 등의 단기간동안 출전이 불가능한 부상
//fine과 fatigue, hurt 상태에서의 영구적인 능력치 손상은 없음.
//serious의 경우 햄스트링, 토미존 등의 부상으로 능력치의 영구적인 감소가 소폭 발생한다.
//critical의 경우 관절와순, 뇌진탕 등의 부상으로 능력치의 영구적인 감소가 대폭 발생한다.
//serious와 critical의 경우에 발생하는 영구적인 능력치 감소를 출전 불가 기간이 끝난 후, 회복할 수 있는 recovery 시스템이 있어도 좋을 것 같다.
//dead의 경우 선수생명이 끝난 것을 의미하고, 선수 활동이 불가능한 부상, 노화, 사고 등으로 은퇴함을 뜻한다.
public enum injury { fine, fatigue, hurt, serious, critical, dead };
public class Human : MonoBehaviour 
{
    [SerializeField]
    private int age = 20; //현재 나이
    [SerializeField]
    private int g_age = 30; // 성장할 수 있는 나이(?) 이 나이 이후로는 성장이 거의 없다시피 할 것.
    [SerializeField]
    private int c_age = 37; // 에이징 커브가 시작 될 나이. 이때부터 능력치가 감소하기 시작한다.
    [SerializeField]
    private int height = 170; // 키
    [SerializeField]
    private int weight = 100; // 몸무게
    [SerializeField]
    private int muscle = 30; // 근육량
    [SerializeField]
    private int injuredtime = 0; //부상이 지속되는 기간
    [SerializeField]
    private injury injured = injury.fine; //부상 종류

    public Human()
    {
        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 90) age = 23; //대졸 draft
        else age = 20; //고졸 draft
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
    public void Retirement() //선수 은퇴 ---> 비활성화 할 것
    {

    }
}
