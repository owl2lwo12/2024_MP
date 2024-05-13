using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI.Player
{
    public class PlayerStatusLeftSideManager : MonoBehaviour
    {
        public TMP_Dropdown Dropdown; 
    
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
            // 선수 리스트
            List<TMP_Dropdown.OptionData> playerList = new List<TMP_Dropdown.OptionData>();
            
            
            playerList.Add(new TMP_Dropdown.OptionData("이장현"));
            playerList.Add(new TMP_Dropdown.OptionData("심기호"));
        
            // 위에서 생성한 optionList를 _dropdown의 옵션 값에 추가
            Dropdown.AddOptions(playerList);
        
            // 현재 _dropdown 값을 첫 번째 값으로 설정
            Dropdown.value = 0;
        }
    }
}
