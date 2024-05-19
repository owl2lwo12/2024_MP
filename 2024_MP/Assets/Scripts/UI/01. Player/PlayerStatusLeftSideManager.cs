using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI._01._Player;
using UnityEngine.Serialization;

namespace UI.Player
{
    public class PlayerStatusLeftSideManager : MonoBehaviour
    {
        public static PlayerStatusLeftSideManager Instance = null;
        
        [Header("Status")]
        public TMP_Dropdown dropdown;
        
        [Header("선수 정보")]
        public TMP_Text playerName;
        public TMP_Text playerAge;
        public TMP_Text playerHeight;
        public TMP_Text playerWeight;
        
        
        [Header("선수 리스트")]
        public List<Batter_Stats> playerList = new List<Batter_Stats>();
        
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // 초기화 함수
        void Init()
        {
            // 선수 리스트 초기화
            List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
            
            // TODO : 선수 리스트 데이터 받아오기
            optionData.Add(new TMP_Dropdown.OptionData("이장현"));
            optionData.Add(new TMP_Dropdown.OptionData("심기호"));
        
            // 위에서 생성한 optionList를 _dropdown의 옵션 값에 추가
            dropdown.AddOptions(optionData);
        
            // 현재 _dropdown 값을 첫 번째 값으로 설정
            dropdown.RefreshShownValue();
            dropdown.value = 0;
            
            // 이름 설정
            OnChangedPlayer();
        }

        // Dropdown에서 선택한 플레이어가 변경됐을 때 실행시킬 함수
        public void OnChangedPlayer()
        {
            Batter_Stats player = new Batter_Stats();
            
            // 현재 선택된 플레이어 이름으로 변경
            string newPlayerName = dropdown.options[dropdown.value].text;
            playerName.text = newPlayerName;
            
            // 나이/키/몸무게 설정하기
            playerAge.text = player.getage().ToString();
            playerHeight.text = player.getheight().ToString();
            playerWeight.text = player.getweight().ToString();
             
            
            // 스탯 변경
            // 툴 변경
            PlayerStatusCenterSideManager.Instance.SetPlayerToolUI(player);
            // 스킬 변경
            PlayerStatusCenterSideManager.Instance.SetPlayerSkillUI(player);
        }
    }
}
