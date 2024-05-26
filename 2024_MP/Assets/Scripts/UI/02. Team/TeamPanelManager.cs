using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamPanelManager : Singleton<TeamPanelManager>
{
    public TMP_Text gameTxt;
    public TMP_Text winTxt;
    public TMP_Text loseTxt;
    public TMP_Text drawTxt;
    public TMP_Text rateTxt;
    public TMP_Text rankTxt;

    private Team_Scripts _myTeam;
    
    private void Update()
    {
        _myTeam = MainSystem._instance.teams[0].GetComponent<Team_Scripts>();
        int win = _myTeam.Win;
        int lose = _myTeam.Lose;
        int draw = _myTeam.Draw;
        float rate = _myTeam.WinRate;
        int rank = _myTeam.Rank;
        int game = win + lose + draw;
        SettingTeamInformationText(game, win, lose, draw, rate, rank);
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
