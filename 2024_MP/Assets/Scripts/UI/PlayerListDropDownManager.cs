using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerListDropDownManager : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        
        // 새로운 옵션 설정을 위한 OptionData 생성
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();
        optionList.Add(new TMP_Dropdown.OptionData("이장현"));
        optionList.Add(new TMP_Dropdown.OptionData("심기호"));
        
        // 위에서 생성한 optionList를 _dropdown의 옵션 값에 추가
        _dropdown.AddOptions(optionList);
        
        // 현재 _dropdown 값을 첫 번째 값으로 설정
        _dropdown.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
