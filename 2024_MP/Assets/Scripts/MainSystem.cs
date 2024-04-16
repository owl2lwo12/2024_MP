using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *  메인 시스템 - 팀 - 선수 구조로 배치할 것
 *  시스템 - team[] - player[] 느낌으로 시스템이 team의 data를 다루고 team들이 player의 data를 다룰 수 있도록
 *  시스템에서 수행하여야할 list
 *  1. 매 시즌 각 팀의 경기 일정을 짠다.
 *  2. 매 시즌 이후 team에 신인 선수를 배정한다.
 *  3. 각 시즌별 등수기록 등을 영구히 가질 수 있도록 한다.
 *  4. 진행중인 시즌의 현황을 가질 수 있어야 한다.
 */


public class MainSystem : MonoBehaviour
{
    public GameObject[] teams;


    public void MakeSchedule()
    {
        // 팀 당 총 144경기 (팀별 16경기 3 3 3 3 2 2 순으로)
        // 보통 개막 시리즈는 2경기로
        /* 홈 - 어웨이
         * 1 - 6
         * 2 - 7
         * 3 - 8
         * 4 - 9
         * 5 - 10
         * 와 같이 배치된다.
         * 이후 연속되는 팀과의 경기가 없도록 매 27경기가 3경기씩 배치되고
         * 이것이 4번 반복된 후 18경기를 2경기씩 배치하고, 개막전에 경기한 팀을 제외한 
         * 나머지 8팀과 2경기씩 추가로 이루어지면 정규시즌이 마무리된다. 
         * 홈 어웨이 또한 나누어져야하면 3연전은 홈홈 어어 홈홈 어어로 묶고
         * 2연전은 홈홈홈 어어어 와 같이 묶는 것이 좋아보인다.
         */
    }
}
