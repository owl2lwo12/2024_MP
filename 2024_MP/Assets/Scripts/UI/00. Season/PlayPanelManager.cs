using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Season
{
    public class PlayPanelManager : MonoBehaviour
    {
        [Header("UI")]
        public TMP_Text progressText; // 경기 수 표시 텍스트
        public GameObject playingPanel;
        
        [Header("팀 아이콘")]
        public List<GameObject> teamIcons = new List<GameObject>();
        
        private int[] _curMatch = new int[10];
        
        
        void Start()
        {
            // 초기 설정
            Init();
        }

        private void Update()
        {
            _curMatch = MainSystem._instance.currmatch;
            
        }

        void Init()
        {
            // 현재 경기 수 초기화
            SetProgressText();
            
            // 매칭 아이콘 설정
            Invoke("SetMatchIcons", 0.5f);
            
            // 매치 결과 패널 비활성화
            playingPanel.SetActive(false);
        }
        
        // 현재 경기 수를 설정 함수
        public void SetProgressText()
        {
            int curMatch = UIManager.Instance.curMatch;
            int maxMatch = UIManager.Instance.maxMatch;
            progressText.text = $"{curMatch} 경기 / {maxMatch} 경기";
        }

        // 매치 아이콘 설정 함수
        public void SetMatchIcons()
        {
            Debug.Log("아이콘 설정");
            
            for(int i = 0; i < _curMatch.Length; i++)
            {
                // i 번째 순서의 팀
                int numOfTeam = _curMatch[i];
                
                // 현재 UI 내 이미지 불러오기
                Image curSprite = teamIcons[i].GetComponent<Image>();
                
                // 바꿀 스프라이트 UIManger에서 팀 정보 불러오기
                Sprite changeSprite = UIManager.Instance.TeamSprites[numOfTeam];
                
                // 스프라이트 변경 및 본래 사이즈로 설정
                curSprite.sprite = changeSprite;
                curSprite.SetNativeSize();
            }
        }
        
        // 경기 시작 함수
        public void PlayMatch()
        {
            // 매치 진행 중인 패널 표시
            playingPanel.SetActive(true);
            
            // UIManager에 OnStartMatch 실행
            UIManager.Instance.OnStartMatch();
            
        }

        // 매치 패널에서 '경기 종료' 버튼 클릭 시
        public void OnMatchOver()
        {
            // 매치 결과 패널 비활성화
            playingPanel.SetActive(false);
            
            // 매치 넘버 증가
            MainSystem._instance.IncreaseMatchNum();
            UIManager.Instance.curMatch++;
            
            // 아이콘 재설정
            SetMatchIcons();
            
            // 텍스트 수정
            SetProgressText();
        }
    }
}
