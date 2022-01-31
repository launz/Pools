# Pools
Unity tool for object-pooling

## Description:
Pools is a simple Unity tool for generic object-pooling.

## Setup:
Just move content of Pools into Unity project.

## How-To:

### Using generic Pools:

Create a new ObjectPooler asset: Create/OT/Pools/ObjectPooler. This is the main asset to store all the information of pools in use as well as controlling all the pooling functions like spawning and despawning pooled objects. On the ObjectPooler create ObjectPools for all different items that should be pooled with a name, a reference to a prefab and the pool-size and spawning parameters. Reference the ObjectPooler scriptable object from a script in the scene and call InitPools() to initialize them. There can be multiple ObjectPoolers in the project to organize pools with different functionality and to initialize them in an asynchronous way. Use Spawn() and Despawn() to pool and unpool objects into the scene.
