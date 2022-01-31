# Pools
Unity tool for object-pooling

## Description:
Pools is a simple Unity tool for generic object-pooling. When dealing with large amounts of GameObjects to instantiate and destroy, there can be issues with performance. Also there can be issues with clarity when instantiating many unnamed GameObjects that don't follow a specific order in the scene hierarchy. Object Pooling is a concept of instantiating a fixed amount of items on an initialization step and later just reusing them by turning them on and off. Pools allows for an easy to setup and use Object Pooling system that also automatically organizes the hierarchy for the pooled objects in the scene for more clarity.

## Setup:
Just move content of Pools into Unity project.

## How-To:

### Using generic Pools:

Create a new ObjectPooler asset: Create/OUT/Pools/ObjectPooler. This is the main asset to store all the information of pools in use as well as controlling all the pooling functions like spawning and despawning pooled objects. On the ObjectPooler create ObjectPools for all different items that should be pooled with a name, a reference to a prefab and the pool-size and spawning parameters. Reference the ObjectPooler scriptable object from a script in the scene and call InitPools() to initialize them. There can be multiple ObjectPoolers in the project to organize pools with different functionality and to initialize them in an asynchronous way. Use Spawn() and Despawn() to pool and unpool objects into the scene.
