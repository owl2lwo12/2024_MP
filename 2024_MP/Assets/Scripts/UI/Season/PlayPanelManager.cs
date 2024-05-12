using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Season
{
    public class PlayPanelManager : MonoBehaviour
    {
        public TMP_Text progressText; // 경기 수 표시 텍스트

        public List<GameObject> TeamIcons = new List<GameObject>();

        void Start()
        {
            // 초기 설정
            Init();
        }

        void Init()
        {
            // 현재 경기 수 초기화
            SetProgressText();
            
            // 매칭 설정
            SetMatchIcons();
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
            for(int i = 0; i < TeamIcons.Count; i++)
            {
                // 현재 UI 내 이미지 불러오기
                Image curSprite = TeamIcons[i].GetComponent<Image>();
                
                // 바꿀 스프라이트 불러오기
                Sprite changeSprite = UIManager.Instance.TeamSprites[i];
                
                // 스프라이트 변경 및 본래 사이즈로 설정
                curSprite.sprite = changeSprite;
                curSprite.SetNativeSize();
            }
        }
        
        // 경기 시작 함수
        public void PlayMatch()
        {
            Debug.Log("경기 시작");
            
            // UIManager에 OnStartMatch 실행
            UIManager.Instance.OnStartMatch();
        }
        
        
    }
}
