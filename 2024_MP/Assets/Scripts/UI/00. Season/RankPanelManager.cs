using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI._00._Season
{
    public class RankPanelManager : MonoBehaviour
    {
        public List<Image> teamIcons = new List<Image>(10);
        public List<TMP_Text> teamNames = new List<TMP_Text>(10);

        public int[] ranking = new int[10];
        
        private void Update()
        {
            ranking = MainSystem._instance.Ranking;

            for (int i = 0; i < 10; i++)
            {
                int team = ranking[i];

                teamIcons[i].sprite = UIManager.Instance.TeamSprites[team];
                teamIcons[i].SetNativeSize();
                teamNames[i].text = UIManager.Instance.TeamNames[team];
            }
            
        }
    }
}
