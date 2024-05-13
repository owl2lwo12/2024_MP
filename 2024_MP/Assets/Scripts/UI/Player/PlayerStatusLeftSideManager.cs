using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI.Player
{
    public class PlayerStatusLeftSideManager : MonoBehaviour
    {
        public TMP_Dropdown Dropdown; 
    
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
            // ���� ����Ʈ
            List<TMP_Dropdown.OptionData> playerList = new List<TMP_Dropdown.OptionData>();
            
            
            playerList.Add(new TMP_Dropdown.OptionData("������"));
            playerList.Add(new TMP_Dropdown.OptionData("�ɱ�ȣ"));
        
            // ������ ������ optionList�� _dropdown�� �ɼ� ���� �߰�
            Dropdown.AddOptions(playerList);
        
            // ���� _dropdown ���� ù ��° ������ ����
            Dropdown.value = 0;
        }
    }
}
