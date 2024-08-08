using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : GameStateBase
{
    public override string SceneName { get { return "MainMenu"; } }
    public override StateType Type { get { return StateType.MainMenu; } }

    public MainMenuState() : base()
    {
        AddTargetState(StateType.InGame);
        AddTargetState(StateType.Options);
    }
}
