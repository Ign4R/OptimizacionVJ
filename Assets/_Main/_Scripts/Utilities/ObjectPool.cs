using System.Collections.Generic;
using UnityEngine;

/// <Heap>
/// Utilize un ObjectPool para gestionar la reutilizacion de objetos en el juego
/// para no crear y destruir objetos constantemente como las balas o los tanques. 
/// Evitando asi operaciones costosas en la memoria dinamica
/// <Heap>
public class ObjectPool<T> where T : MonoBehaviour
{
    private FactoryObject<T> _factoryObj;
    private Queue<T> _pooledObjects = new Queue<T>();

    public ObjectPool(FactoryObject<T> factoryObj)
    {
        _factoryObj = factoryObj; 
    }
    public void Initialization(int countToPool)
    {
        for (int i = 0; i < countToPool; i++)
        {
            var obj = _factoryObj.CreateObj();

            obj.gameObject.name = obj.gameObject.name + i;
            obj.gameObject.SetActive(false);
            _pooledObjects.Enqueue(obj); /// se utiliza para agregar un elemento al final de la cola
        }
    }
    public T GetPooledObject()
    {
        T poolObject;
        if (_pooledObjects.Count > 0)
        {
            poolObject = _pooledObjects.Dequeue();
            poolObject.gameObject.SetActive(true);
            return poolObject;
        }
        else ///en caso de necesitar un objeto mas porque utilizaste todos, instancia uno
        {
            poolObject = _factoryObj.CreateObj();
            _pooledObjects.Enqueue(poolObject); /// para sacar el elemento que primero se agrego o el mas antiguo
            return poolObject;
        }

    }
    public void ReturnToPool(T poolObject) /// esta funcion se usa para devolver al pool el obj (es cuando ya no necesita ser usado)
    {
        poolObject.gameObject.SetActive(false);
        _pooledObjects.Enqueue(poolObject);

    }

    public int GetCountPool()
    {
        return _pooledObjects.Count;
    }
}