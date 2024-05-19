using System;
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

        [Header("스킬")] 
        public Slider zoneSlider;

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

        // 툴 UI 세팅
        public void SetPlayerToolUI(Batter_Stats player)
        {
            // 힘
            int pow = player.getpower();
            powerSlider.value = pow;
            
            // 속도
            int speed = player.getspeed();
            speedSlider.value = speed;
            
            // 방어
            int def = player.getdefense();
            speedSlider.value = def;
            
            // 컨택
            int con = player.getcontact();
            speedSlider.value = con;
            
            // 선구안
            int eye = player.geteye();
            speedSlider.value = eye;
            
            // 지능
            int intel = player.getintelligence();
            speedSlider.value = intel;
        }

        // 
        public void SetPlayerSkillUI(Batter_Stats player)
        {
            
        }
    }
}
