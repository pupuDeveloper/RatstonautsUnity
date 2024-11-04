using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewBehaviourScript : PanelSignleton<PanelManager>
{
    private List<PanelInstanceModel> _listInstances = new List<PanelInstanceModel>(); //holds all panel instances
    private ObjectPool _objectPool; //pool of panels

    private void Start()
    {
        _objectPool = ObjectPool.Instance; //cache the pool
    }

    public void ShowPanel(string panelId, PanelShowBehaviour behaviour = PanelShowBehaviour.KEEP_PREVIOUS)
    {
        GameObject panelInstance = _objectPool.GetObjectFromPool(panelId);

        if (panelInstance != null)
        {
            if (behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelsInQueue() > 0)
            {
                var lastPanel = GetLastPanel();
                if (lastPanel != null)
                {
                    lastPanel.PanelInstance.SetActive(false);
                }
            }

            _listInstances.Add(new PanelInstanceModel //add this panel to the queue
            {
                Panelid = panelId,
                PanelInstance = panelInstance
            });
        }
        else
        {
            Debug.LogWarning($"Panel id = {panelId} not found");
        }
    }

    public void HideLastPanel()
    {
        if (AnyPanelShowing())
        {
            var lastPanel = GetLastPanel();


            _listInstances.Remove(lastPanel);
            _objectPool.PoolObject(lastPanel.PanelInstance);

            if (GetAmountPanelsInQueue > 0)
            {
                lastPanel = GetLastPanel();
                if (lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.panelInstance.SetActive(true);
                }
            }
        }
    }

    PanelInstanceModel GetLastPanel()
    {
        return _listInstances[_listInstances.Count - 1];
    }

    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }

    public int GetAmountPanelsInQueue()
    {
        return _listInstances.Count;
    }
}

