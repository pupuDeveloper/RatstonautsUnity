using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string Panelid; //id of the panel we want to show
    public PanelShowBehaviour Behaviour;
    private PanelsManager _panelsManager; // cached panels manager

    private void Start()
    {
        _panelsManager = PanelsManager.Instance;
    }

    public void DoShowPanel()
    {
        _panelsManager.ShowPanel(Panelid, Behaviour);
    }
}
