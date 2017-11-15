using UnityEngine;
using UnityEngine.EventSystems;

public class Scene : MonoBehaviour
{
    private GameObject _lastselect;

    protected virtual void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(_lastselect);
        else
            _lastselect = EventSystem.current.currentSelectedGameObject;
    }
}