using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void GetDamage() { }
    
    protected virtual void Die() => this.gameObject.SetActive(false);
}