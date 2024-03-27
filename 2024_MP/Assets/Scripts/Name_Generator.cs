using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "name_generator", menuName = "ScriptableObject/name_generator", order = 1)]
public class Name_Generator : ScriptableObject
{
    [SerializeField]
    private string[] familyname; //성 저장할 것
    [SerializeField]
    private string[] lastname; //이름 저장할 것

    public string GenerateName()
    {
        string fname = familyname[Random.Range(0, familyname.Length)]; //familyname 중 하나를 랜덤하게 가져올 것
        string lname = lastname[Random.Range(0, lastname.Length)]; //lastname 중 하나를 또한 가져올 것
        string name = fname + lname; //두개를 합칠 것
        return name;
    }
}
