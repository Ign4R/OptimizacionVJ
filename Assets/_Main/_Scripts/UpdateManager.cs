using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance { get;  private set; }

    private List<IUpdateable> objsToUpdate = new List<IUpdateable>();
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
        var count = objsToUpdate.Count;  //*QUEST [Porque es necesario almacenar en una variable temporal el dato?]
        for (int i = 0; i < count; i++) /// for (int i = 0; i < *objsToUpdate.Length; i++)
        {
            objsToUpdate[i].CustomUpdate();       
        }
    }
    public void Add(IUpdateable obj)
    {
        objsToUpdate.Add(obj);
    }

    public void Remove(IUpdateable obj)
    {
        objsToUpdate.Remove(obj);
    }
}
