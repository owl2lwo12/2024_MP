using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Season
{
    public class PlayPanelManager : Singleton<PlayPanelManager>
    {
        [Header("UI")]
        public TMP_Text progressText; // 경기 수 표시 텍스트
        public GameObject playingPanel;
        public GameObject matchBtn;
        
        [Header("팀 아이콘")]
        public List<GameObject> teamIcons = new List<GameObject>();

        [Header("경기 진행")] 
        public Image leftImage;
        public Image rightImage;
        public TMP_Text leftScore;
        public TMP_Text rightScore;
        public TMP_Text winOrLose;
        public float matchTime = 2f;

        [Header("오브젝트 관리")] 
        public GameObject loadingPanel;
        public GameObject overBtn;
        public GameObject teamGroup;
        public GameObject scoreGroup;
        
        [SerializeField]
        private int[] _curMatch = new int[10];
        
        
        void Start()
        {
            // 초기 설정
            Init();
        }

        private void Update()
        {
            _curMatch = MainSystem._instance.currmatch;
            
            
            // 아이콘 재설정
            SetMatchIcons();
            
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

            leftImage.sprite = teamIcons[0].GetComponent<Image>().sprite;
            leftImage.SetNativeSize();
            rightImage.sprite = teamIcons[1].GetComponent<Image>().sprite;
            rightImage.SetNativeSize();
        }
        
        // 경기 시작 함수
        public void PlayMatch()
        {
            // 매치 진행 중인 패널 표시
            playingPanel.SetActive(true);
            loadingPanel.SetActive(true);
            overBtn.SetActive(false);
            teamGroup.SetActive(false);
            scoreGroup.SetActive(false);
            
            Invoke("OnLoadMatchResult", matchTime);
            
            // UIManager에 OnStartMatch 실행
            UIManager.Instance.OnStartMatch();
            
        }

        public void OnLoadMatchResult()
        {
            loadingPanel.SetActive(false);
            overBtn.SetActive(true);
            teamGroup.SetActive(true);
            scoreGroup.SetActive(true);
        }

        // 매치 패널에서 '경기 종료' 버튼 클릭 시
        public void OnMatchOver()
        {
            // 매치 결과 패널 비활성화
            playingPanel.SetActive(false);
            
            // 매치 넘버 증가
            MainSystem._instance.IncreaseMatchNum();

            if (UIManager.Instance.curMatch == UIManager.Instance.maxMatch) // 현재 매치가 144면
            {
                matchBtn.SetActive(false);
                Debug.Log("시즌 종료");
            }
            else
            {
                UIManager.Instance.curMatch++;
            }
            
            // 텍스트 수정
            SetProgressText();
        }

        public void SettingMatchResultUI(int newleftScore, int newRightScore, string newWinOrLose)
        {
            leftScore.text = $"{newleftScore}";
            rightScore.text = $"{newRightScore}";
            winOrLose.text = $"{newWinOrLose}";
        }
    }
}
