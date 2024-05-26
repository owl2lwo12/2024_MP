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
