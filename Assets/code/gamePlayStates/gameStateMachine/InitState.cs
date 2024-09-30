using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : GameStateBase
{
    public override string SceneName { get { return "Init"; } }
    public override StateType Type { get { return StateType.Initialization; } }

    public InitState() : base()
    {
        AddTargetState(StateType.InGame);
        AddTargetState(StateType.MainMenu);
    }
}
