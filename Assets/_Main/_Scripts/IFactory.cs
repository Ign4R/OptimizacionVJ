using System.Collections;
using UnityEngine;

public interface IFactory<T> where T : Updateable
{
    T CreateObject();
}