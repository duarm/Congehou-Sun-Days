using System;
using System.Collections.Generic;
using UnityEngine;

//Four Parameter
public abstract class ObjectPool<TPool, TObject, TInfo, TInfoB, TInfoC, TInfoD> : ObjectPool<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo, TInfoB, TInfoC, TInfoD>
    where TObject : PoolObject<TPool, TObject, TInfo, TInfoB, TInfoC, TInfoD>, new()
{
    void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            TObject newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    public virtual TObject Pop(TInfo info, TInfoB infoB, TInfoC infoC, TInfoD infoD)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].inPool)
            {
                pool[i].inPool = false;
                pool[i].WakeUp(info, infoB, infoC, infoD);
                return pool[i];
            }
        }

        TObject newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp(info, infoB, infoC, infoD);
        return newPoolObject;
    }
}

//Three Parameter
public abstract class ObjectPool<TPool, TObject, TInfo, TInfoB, TInfoC> : ObjectPool<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo, TInfoB, TInfoC>
    where TObject : PoolObject<TPool, TObject, TInfo, TInfoB, TInfoC>, new()
{
    void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            TObject newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    public virtual TObject Pop(TInfo info, TInfoB infoB, TInfoC infoC)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].inPool)
            {
                pool[i].inPool = false;
                pool[i].WakeUp(info, infoB, infoC);
                return pool[i];
            }
        }

        TObject newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp(info, infoB, infoC);
        return newPoolObject;
    }
}

//Two Parameter
public abstract class ObjectPool<TPool, TObject, TInfo, TInfoB> : ObjectPool<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo, TInfoB>
    where TObject : PoolObject<TPool, TObject, TInfo, TInfoB>, new()
{
    void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            TObject newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    public virtual TObject Pop(TInfo info, TInfoB infoB)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].inPool)
            {
                pool[i].inPool = false;
                pool[i].WakeUp(info, infoB);
                return pool[i];
            }
        }

        TObject newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp(info, infoB);
        return newPoolObject;
    }
}

public abstract class ObjectPool<TPool, TObject, TInfo> : ObjectPool<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo>
    where TObject : PoolObject<TPool, TObject, TInfo>, new()
{
    void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            TObject newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    public virtual TObject Pop(TInfo info)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].inPool)
            {
                pool[i].inPool = false;
                pool[i].WakeUp(info);
                return pool[i];
            }
        }

        TObject newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp(info);
        return newPoolObject;
    }
}

public abstract class ObjectPool<TPool, TObject> : MonoBehaviour
    where TPool : ObjectPool<TPool, TObject>
    where TObject : PoolObject<TPool, TObject>, new()
{
    [Header("Pool Values")]
    public GameObject prefab;
    public int initialPoolCount = 10;
    [HideInInspector]
    public List<TObject> pool = new List<TObject>();

    void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            TObject newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    protected TObject CreateNewPoolObject()
    {
        TObject newPoolObject = new TObject();
        newPoolObject.instance = Instantiate(prefab);
        newPoolObject.instance.transform.SetParent(transform);
        newPoolObject.inPool = true;
        newPoolObject.SetReferences(this as TPool);
        newPoolObject.Sleep();
        return newPoolObject;
    }

    public virtual TObject Pop()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].inPool)
            {
                pool[i].inPool = false;
                pool[i].WakeUp();
                return pool[i];
            }
        }

        TObject newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp();
        return newPoolObject;
    }

    public virtual void Push(TObject poolObject)
    {
        poolObject.inPool = true;
        poolObject.Sleep();
    }
}

//Four Parameter
[Serializable]
public abstract class PoolObject<TPool, TObject, TInfo, TInfoB, TInfoC, TInfoD> : PoolObject<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo, TInfoB, TInfoC, TInfoD>
    where TObject : PoolObject<TPool, TObject, TInfo, TInfoB, TInfoC, TInfoD>, new()
{
    public virtual void WakeUp(TInfo info, TInfoB infoB, TInfoC infoC, TInfoD infoD)
    { }
}

//Three Parameter
[Serializable]
public abstract class PoolObject<TPool, TObject, TInfo, TInfoB, TInfoC> : PoolObject<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo, TInfoB, TInfoC>
    where TObject : PoolObject<TPool, TObject, TInfo, TInfoB, TInfoC>, new()
{
    public virtual void WakeUp(TInfo info, TInfoB infoB, TInfoC infoC)
    { }
}

//Two Parameter
[Serializable]
public abstract class PoolObject<TPool, TObject, TInfo, TInfoB> : PoolObject<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo, TInfoB>
    where TObject : PoolObject<TPool, TObject, TInfo, TInfoB>, new()
{
    public virtual void WakeUp(TInfo info, TInfoB infoB)
    { }
}

//One Parameter
[Serializable]
public abstract class PoolObject<TPool, TObject, TInfo> : PoolObject<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo>
    where TObject : PoolObject<TPool, TObject, TInfo>, new()
{
    public virtual void WakeUp(TInfo info)
    { }
}

//No Parameter
[Serializable]
public abstract class PoolObject<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject>
    where TObject : PoolObject<TPool, TObject>, new()
{
    public bool inPool;
    public GameObject instance;
    public TPool objectPool;

    public void SetReferences(TPool pool)
    {
        objectPool = pool;
        SetReferences();
    }

    protected virtual void SetReferences()
    { }

    public virtual void WakeUp()
    { }

    public virtual void Sleep()
    { }

    public virtual void ReturnToPool()
    {
        TObject thisObject = this as TObject;
        objectPool.Push(thisObject);
    }
}