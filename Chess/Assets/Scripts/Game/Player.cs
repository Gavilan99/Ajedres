using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public TeamColor team;

    public void SetTeamColor(TeamColor teamColor)
    {
        this.team = teamColor;
    }

    public TeamColor GetTeamColor()
    {
        return team;
    }
}
