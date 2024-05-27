using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamHistoryPanelManager : MonoBehaviour
{
    [Header("ÆÀ ¸®½ºÆ®")]
    public GameObject content;
    public List<string> teamNames = new List<string>();
    public List<Image> teamIcons = new List<Image>();
    public List<Button> teamBtns = new List<Button>();

    [Header("ÆÀ Á¤º¸")]
    public GameObject teamInformPanel;
    public TMP_Text teamName;
    public Image teamIcon;
    public TMP_Text gameTxt;
    public TMP_Text winTxt;
    public TMP_Text loseTxt;
    public TMP_Text drawTxt;
    public TMP_Text rateTxt;
    public TMP_Text rankTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject team = content.transform.GetChild(i).gameObject;
            
            teamNames.Add(team.GetComponentInChildren<TMP_Text>().text);
            teamIcons.Add(team.transform.GetChild(0).GetComponent<Image>());
            teamBtns.Add(team.GetComponentInChildren<Button>());
            int index = i;
            teamBtns[i].onClick.AddListener( () => SelectTeamButton(index));
        }
    }

    void SelectTeamButton(int num)
    {
        teamInformPanel.SetActive(true);
        Team_Scripts curTeam = MainSystem._instance.teams[num].GetComponent<Team_Scripts>();
        
        // -------- ÆÀ ÇÁ·ÎÇÊ ¼³Á¤ ---------
        teamName.text = teamNames[num];
        teamIcon.sprite = teamIcons[num].sprite;
        
        // -------- ÆÀ ÀüÀû ¼³Á¤ ---------
        int win = curTeam.Win;
        int lose = curTeam.Lose;
        int draw = curTeam.Draw;
        float rate = curTeam.WinRate;
        int rank = curTeam.Rank;
        int game = win + lose + draw;
        SettingTeamInformationText(game, win, lose, draw, rate, rank);
    }

    public void OnClickTeamInformPanelCloseBtn()
    {
        teamInformPanel.SetActive(false);
    }
    
    public void SettingTeamInformationText(int game, int win, int lose, int draw, float rate, int rank)
    {
        gameTxt.text = $"{game}";
        winTxt.text = $"{win}";
        loseTxt.text = $"{lose}";
        drawTxt.text = $"{draw}";
        rateTxt.text = $"{rate}";
        rankTxt.text = $"{rank}";
    }
}
