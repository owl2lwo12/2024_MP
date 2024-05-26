using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI._04._Setting
{
    public class SettingPanel : MonoBehaviour
    {
        [Header("�����̴�")] 
        public Slider sfxSlider;
        public Slider musicSlider;
        
        public void OnClickExitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // ���ø����̼� ����
#endif
        }
        
    }
}
