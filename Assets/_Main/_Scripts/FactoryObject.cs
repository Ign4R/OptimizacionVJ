using UnityEngine;
public class FactoryObject
{
    private GameObject _instance;
    private Transform _parent;

    public FactoryObject(GameObject instance, Transform parent)
    {
        _instance = instance;
        _parent = parent;

    }
    public GameObject CreateObj()
    {
        return Object.Instantiate(_instance, _parent);
    }
}
