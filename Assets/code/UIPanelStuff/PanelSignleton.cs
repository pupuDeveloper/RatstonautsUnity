
using UnityEngine;

public class PanelSignleton<T> : MonoBehaviour where T : using MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = (T) (object) this;
    }
}
