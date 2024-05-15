using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UI.Player
{
    public class PlayerStatusLeftSideManager : MonoBehaviour
    {
        [Header("Status")]
        public TMP_Dropdown Dropdown;
        public TMP_Text playerName;
        
        [Header("선수 리스트")]
        public List<Human> playerList = new List<Human>();
        
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // 초기화 함수
        void Init()
        {
            // 선수 리스트 초기화
            List<TMP_Dropdown.OptionData> playerList = new List<TMP_Dropdown.OptionData>();
            
            // TODO : 선수 리스트 데이터 받아오기
            playerList.Add(new TMP_Dropdown.OptionData("이장현"));
            playerList.Add(new TMP_Dropdown.OptionData("심기호"));
        
            // 위에서 생성한 optionList를 _dropdown의 옵션 값에 추가
            Dropdown.AddOptions(playerList);
        
            // 현재 _dropdown 값을 첫 번째 값으로 설정
            Dropdown.RefreshShownValue();
            Dropdown.value = 0;
            
            // 이름 설정
            SettingPlayerName();
        }

        // 현재 드롭다운의 값으로 이름 설정
        public void SettingPlayerName()
        {
            playerName.text = Dropdown.options[Dropdown.value].text;
        }
    }
}
