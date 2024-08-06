using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameStateBase
{
    private List<StateType> _targetStates = new List<StateType>();
    public abstract string SceneName { get; }
    public abstract StateType Type { get; }
    public virtual bool isAdditive { get { return false; } } //by default, state is not additive, returns false. this can be overritten in a child class

    protected GameStateBase() //constructor, can only be called from child class. It is automatically called when a new instance of a child class is created
    {

    }

    protected void AddTargetState (StateType stateType)
    {
        //if state is not in list add it.
        if (!_targetStates.Contains(stateType))
        {
            _targetStates.Add(stateType);
        }
    }
    protected bool RemoveTargetState (StateType stateType)
    {
        return _targetStates.Remove(stateType); //remove method already checks if list contains the item to be removed
    }

    public virtual void Activate(bool forceLoad = false)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (forceLoad || currentScene.name.ToLower() != SceneName.ToLower())
        {
            LoadSceneMode loadMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single; //if isAdditive is true, then loadMode is set to additive, else then to Single
            SceneManager.LoadScene(SceneName, loadMode);
        }
    }

    public virtual void Deactivate()
    {
        if (isAdditive)
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }
    }
    public bool IsValidTarget (StateType targetStateType)
    {
        return _targetStates.Contains(targetStateType);
    }
}
