using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private List<Updateable> _objsToUpdate= new List<Updateable>();
    public static UpdateManager Instance { get ; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;         
        }
    }

    private void Update()
    { 
        var count = _objsToUpdate.Count;  //*QUEST [Porque es necesario guardarlo en una variable?]
        for (int i = 0; i < count; i++) 
        {
            _objsToUpdate[i].CustomUpdate();       
        }
    }
    public void Add(Updateable obj)
    {
        if (!_objsToUpdate.Contains(obj))
        {
            _objsToUpdate.Add(obj);
        }      
    }
    public void Remove(Updateable obj)
    {
        if (_objsToUpdate.Contains(obj))
        {
            _objsToUpdate.Remove(obj);
        }
    }
}
