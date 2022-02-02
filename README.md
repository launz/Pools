# 🌊 Pools
Unity tool for object-pooling

## 🗒️ Description:
*Pools* is a simple Unity tool for generic object pooling. When dealing with large amounts of GameObjects to instantiate and destroy, there can be issues with performance. Also there can be issues with clarity when instantiating many unnamed GameObjects that don't follow a specific order in the scene hierarchy. Object pooling is a concept of instantiating a fixed amount of items on an initialization step and later just reusing them by turning them on and off. *Pools* allows for an easy to setup and use object pooling system that also automatically organizes the hierarchy for the pooled objects in the scene for more clarity.

## 🛠️ Setup:
Just move content of Pools into Unity project.

## 💡 How-To:

### Using generic Pools:

First create a new ObjectPool in a script. Data, like the prefab to be used as well as pool size, can be filled in manually at creation or with the help of a ObjectPoolData asset. Especially when many pools should be created at runtime with the same properties ObjectPoolData can be useful. Every ObjectPool can be controlled through the static functions in the *Pools* script. When an ObjectPool is used for the first time it has to be initiated with Init(). Use Spawn() to pool in new objects into the scene and Despawn() to unpool them again.

### IPooledObject and OnSpawn()/ OnDespawn():

Since objects pooled with *Pools* technically only get enabled and disabled, behavior that might normally live in a Start() function of a Monobehavior should not be used like that on pooled objects. It would fire right at the start when the objects are first getting instantiated and wouldn't get called again when the objects actually get spawned. For this reason there is an interface called IPooledObject. So if an object should do something right when it is spawned into the scene, there can be a script added to the gameobject that uses this Interface (public class MyScript: Monobehavior, IPooledObject {...). OnSpawn() and OnDespawn() can then be defined on that script. They will automatically be called at the right time when the object spawns or despawns.

### maxSize and OverflowType:

Another big reason to use object pooling is that it easily allows to track how many objects of a given pool are in use and also to limit a pool from spawning more if it has reached it's limit.
Every pool has a property called OverflowType. This controls what happens when a pool has spawned all of it's objects and is still being called to spawn new ones.

  - OverflowType.Limit: This is the default behavior. Limit just prevents new objects from being spawned from the pool.
  - OverflowType.ReuseFirst: The oldest object of the pool in the scene gets despawned and used as a new object to be spawned.
  - OverflowType.AutoResize: Automatically increases the maxSize of the pool and creates a new item to be spawned. (When used excessively it kind of defeats the purpose of a pool because items are pretty much just normally instantiated.)
