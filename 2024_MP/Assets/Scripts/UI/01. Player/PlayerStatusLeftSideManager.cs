using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI._01._Player;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        public Slider playerInjurySlider;
        
        [Header("선수 리스트")]
        public List<GameObject> playerList = new List<GameObject>(); // 선수 리스트
        private List<Batter_Stats> _batterStatsList = new List<Batter_Stats>();
        public List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>(); // 드롭다운 리스트
        public Team_Scripts myTeam;
        
        void Start()
        {
            Init();
        }

        private void Awake()
        {
            // Singleton
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
            InitPlayerList();
            
            // 이름 설정
            OnChangedPlayer();
        }

        // 선수 리스트 초기화
        public void InitPlayerList()
        {
            // TODO : 선수 리스트 받아오기
            playerList = myTeam.Batterrplayerlist;
            
            foreach (var gameObject in playerList)
            {
                _batterStatsList.Add(gameObject.GetComponent<Batter_Stats>());
            }
            
            if (playerList.Count > 0)
            {
                foreach (var player in _batterStatsList)
                {
                    optionData.Add(new TMP_Dropdown.OptionData(player.getname()));
                }
            }
            else if(playerList == null || playerList.Count <= 0)
            {
                Debug.Log("선수 리스트가 존재하지 않습니다.");
            }
            
            // 위에서 생성한 optionList를 _dropdown의 옵션 값에 추가
            dropdown.AddOptions(optionData);
        
            // 현재 _dropdown 값을 첫 번째 값으로 설정
            dropdown.RefreshShownValue();
            dropdown.value = 0;
        }

        // Dropdown에서 선택한 플레이어가 변경됐을 때 실행시킬 함수
        public void OnChangedPlayer()
        {
            GameObject go = playerList[dropdown.value];
            Batter_Stats player = go.GetComponent<Batter_Stats>();
            
            // 현재 선택된 플레이어 이름으로 변경
            string newPlayerName = dropdown.options[dropdown.value].text;
            playerName.text = newPlayerName;

            // 기본 스탯 UI 변경
            SetPlayerBasicStatsUI(player);
            
            // 스탯 변경
            // 툴 변경
            PlayerStatusCenterSideManager.Instance.SetPlayerToolUI(player);
            // 스킬 변경
            PlayerStatusCenterSideManager.Instance.SetPlayerSkillUI(player);
        }

        // 기본 스탯 UI 설정
        void SetPlayerBasicStatsUI(Batter_Stats player)
        {
            // 나이/키/몸무게 설정
            playerAge.text = player.getage().ToString() + "살";
            playerHeight.text = player.getheight().ToString() + "cm";
            playerWeight.text = player.getweight().ToString() +"kg";
            
            // 부상 상태 설정
            injury injury = player.getinjurytype();
            switch (injury)
            {
                case injury.fine:
                    playerInjurySlider.value = 0f;
                    break;
                case injury.fatigue:
                    playerInjurySlider.value = 1.1f;
                    break;
                case injury.hurt:
                    playerInjurySlider.value = 2.1f;
                    break;
                case injury.serious:
                    playerInjurySlider.value = 3.1f;
                    break;
                case injury.critical:
                    playerInjurySlider.value = 4.1f;
                    break;
                case injury.dead:
                    playerInjurySlider.value = 5.0f;
                    break;
            }
        }
    }
}
