using System.Collections.Generic;
using UnityEngine;

///Hacemos un pool generico para especificar de que tipo de clase que herede de MB queremos usarlo de pool
public class ObjectPool
{
    private FactoryObject _factoryObj;
    private  Queue<GameObject> _pooledObjects = new Queue<GameObject>();

    public ObjectPool(FactoryObject factoryObj)
    {
        _factoryObj = factoryObj; 
    }
    public void Initialization(int countToPool)
    {
        for (int i = 0; i < countToPool; i++)
        {
            var obj = _factoryObj.CreateObj();
            obj.SetActive(false);
            _pooledObjects.Enqueue(obj.gameObject); /// se utiliza para agregar un elemento al final de la cola
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject poolObject;
        if (_pooledObjects.Count > 0)
        {
            poolObject = _pooledObjects.Dequeue();
            poolObject.SetActive(true);
            return poolObject;
        }
        else ///en caso de necesitar un objeto mas porque utilizaste todos, instancia uno
        {
            poolObject = _factoryObj.CreateObj();
            _pooledObjects.Enqueue(poolObject); /// para sacar el elemento que primero se agrego o el mas antiguo
            return poolObject;
        }
    }
    public void ReturnToPool(GameObject poolObject) /// esta funcion se usa para devolver al pool la bala (es cuando ya no necesita ser usada)
    {
        poolObject.gameObject.SetActive(false);
        _pooledObjects.Enqueue(poolObject);
    }
}