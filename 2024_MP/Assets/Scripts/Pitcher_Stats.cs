using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher_Stats : ScriptableObject
{
    //stats
    [SerializeField]
    private int control = 100; // ����
    [SerializeField]
    private int pControl = 100; // ���� ���ټ�
    [SerializeField]
    private int speed = 100; // ����
    [SerializeField]
    private int pSpeed = 100; // ���� ���ټ�
    [SerializeField]
    private int stamina = 100; //���¹̳�(ü��)
    [SerializeField]
    private int pStamina = 100; // ü�� ���ټ�
    [SerializeField]
    private bool hand = false; // false = right , true = left
    [SerializeField]
    private int age = 20;

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
}
