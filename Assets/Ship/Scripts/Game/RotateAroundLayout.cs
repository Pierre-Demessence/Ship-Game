using System.Collections.Generic;
using System.Linq;
using Smart.Extensions;
using UnityEngine;

public class RotateAroundLayout : MonoBehaviour
{
    private List<Transform> _componentsInChildren = new List<Transform>();
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;

    private void Start()
    {
        CalculatePosition();
    }

    private void OnTransformChildrenChanged()
    {
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        _componentsInChildren = gameObject.GetChildren().ToList();

        var offset = 360f / _componentsInChildren.Count;
        for (var i = 0; i < _componentsInChildren.Count; i++)
        {
            var childTransform = _componentsInChildren[i];

            childTransform.position = new Vector3(_radius, 0, 0);
            var rot = childTransform.rotation;
            childTransform.RotateAround(transform.position, Vector3.forward, offset * i);
            childTransform.rotation = rot;
        }
    }

    private void FixedUpdate()
    {
        foreach (var childTransform in _componentsInChildren)
        {
            if (childTransform == transform) continue;
            var rot = childTransform.rotation;
            childTransform.RotateAround(transform.position, Vector3.forward, _speed);
            childTransform.rotation = rot;
        }
    }
}