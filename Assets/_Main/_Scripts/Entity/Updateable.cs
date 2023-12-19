using UnityEngine;

public class Updateable : MonoBehaviour 
{
    public virtual void Start()
    {
        UpdateManager.Instance.Add(this);
    }
    ///Especializacion
    public virtual void CustomUpdate()
    {
        ///Se deja este metodo vacio para que las clases derivadas lo modifiquen a su gusto: Player y Enemy,Bullet,etc
    }
}
