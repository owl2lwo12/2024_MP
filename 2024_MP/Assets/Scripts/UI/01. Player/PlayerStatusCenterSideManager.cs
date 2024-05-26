using System;
using System.Collections.Generic;
using TMPro;
using UI.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI._01._Player
{
    public class PlayerStatusCenterSideManager : MonoBehaviour
    {
        public static PlayerStatusCenterSideManager Instance;

        [Header("툴")] 
        public Slider powerSlider;
        public Slider speedSlider;
        public Slider defSlider;
        public Slider contactSlider;
        public Slider eyeSlider;
        public Slider intelligenceSlider;
        public TMP_Text powerText;
        public TMP_Text speedText;
        public TMP_Text defText;
        public TMP_Text contactText;
        public TMP_Text eyeText;
        public TMP_Text intelligenceText;

        [Header("성장")] 
        public Slider maintainSlider;
        public Slider batSlider;
        public Slider hittingSlider;
        public Slider zoneSlider;
        public Slider driveSlider;

        private int _minStatus = 20;
        private int _maxStatus = 100;
    
        
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

        private void Start()
        {
            Init();
        }

        // 초기화
        void Init()
        {
            // 툴 스탯 초기화
            InitSlider(powerSlider);
            InitSlider(speedSlider);
            InitSlider(defSlider);
            InitSlider(contactSlider);
            InitSlider(eyeSlider);
            InitSlider(intelligenceSlider);
        }

        // 슬라이더 최대값 최소값 설정
        public void InitSlider(Slider slider)
        {
            slider.minValue = _minStatus;
            slider.maxValue = _maxStatus;
        }

        /// <summary>
        /// 플레이어의 Tool UI를 설정해주는 함수
        /// </summary>
        /// <param name="player">해당 플레이어 파라미터</param>
        public void SetPlayerToolUI(Batter_Stats player)
        {
            // 힘
            int pow = player.getpower();
            powerSlider.value = pow;
            powerText.text = $"{pow}/{_maxStatus}";
            
            // 속도
            int speed = player.getspeed();
            speedSlider.value = speed;
            speedText.text = $"{speed}/{_maxStatus}";
            
            // 방어
            int def = player.getdefense();
            defSlider.value = def;
            defText.text = $"{def}/{_maxStatus}";
            
            // 컨택
            int con = player.getcontact();
            contactSlider.value = con;
            contactText.text = $"{con}/{_maxStatus}";
            
            // 선구안
            int eye = player.geteye();
            eyeSlider.value = eye;
            eyeText.text = $"{eye}/{_maxStatus}";
            
            // 지능
            int intel = player.getintelligence();
            intelligenceSlider.value = intel;
            intelligenceText.text = $"{intel}/{_maxStatus}";
        }

        /// <summary>
        /// 플레이어의 Train UI를 설정해주는 함수
        /// </summary>
        /// <param name="player">해당 플레이어 파라미터</param>
        public void SetPlayerTrainUI(Batter_Stats player)
        {
            maintainSlider.value = player.Training_style;
            batSlider.value = player.Batting_theory;
            hittingSlider.value = player.Batting_position;
            zoneSlider.value = player.Zone_size;
            driveSlider.value = player.Batting_positive;
            
        }

        public void OnValueChangedTrainingStyle()
        {
            Batter_Stats player = PlayerStatusLeftSideManager.Instance.GetCurPlayer();
            player.Training_style = (int)maintainSlider.value;
        }
        public void OnValueChangedBattingTheory()
        {
            Batter_Stats player = PlayerStatusLeftSideManager.Instance.GetCurPlayer();
            player.Batting_theory = (int)batSlider.value;
        }
        public void OnValueChangedBattingPosition()
        {
            Batter_Stats player = PlayerStatusLeftSideManager.Instance.GetCurPlayer();
            player.Batting_position = (int)hittingSlider.value;
        }
        public void OnValueChangedZoneSize()
        {
            Batter_Stats player = PlayerStatusLeftSideManager.Instance.GetCurPlayer();
            player.Zone_size = (int)zoneSlider.value;
        }
        public void OnValueChangedBattingPositive()
        {
            Batter_Stats player = PlayerStatusLeftSideManager.Instance.GetCurPlayer();
            player.Batting_positive = (int)driveSlider.value;
        }
    }
}
