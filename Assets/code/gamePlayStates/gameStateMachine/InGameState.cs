using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameStateBase
{
    public override string SceneName { get { return "InGame"; } }
    public override StateType Type { get { return StateType.InGame; } }

    public InGameState() : base()
    {
        AddTargetState(StateType.MainMenu);
    }
}
