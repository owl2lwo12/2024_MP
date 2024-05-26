using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI._04._Setting
{
    public class SettingPanel : MonoBehaviour
    {
        private void Start()
        {
        }

        public void OnClickExitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // 어플리케이션 종료
#endif
        }
        
    }
}
