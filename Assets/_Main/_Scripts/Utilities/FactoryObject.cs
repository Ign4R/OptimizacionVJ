using UnityEngine;
public class FactoryObject<T> where T: MonoBehaviour
{
    private T _instance;
    private Transform _parent;
    public FactoryObject(T instance, Transform parent)
    {
        _instance = instance;
        _parent = parent;
    }
    public T CreateObj()
    {
        return Object.Instantiate(_instance, _parent);
    }
}
