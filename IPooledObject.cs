/// <summary>
/// Interface for pooled object.
/// </summary>
public interface IPooledObject 
{
    void Spawn(ObjectPool _objectPool);
    void Despawn();
}
