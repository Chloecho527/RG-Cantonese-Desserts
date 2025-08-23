using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    // key:物体名字  value：物体
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    // 对象池父物体
    private GameObject pool;
    
    
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    
    /// <summary>
    /// 获取对应游戏对象
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab)
    {
        // 用于获取池中物体并返回给调用者
        GameObject _object;
        
        // 使用预制体的名字查询字典是否存在存储该预制体的池 & 检查池中待分配的物体数
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            // 没有则实例化一个新物体并使用 PushObject 函数放入池中
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);

            // 对象池父物体
            if (pool == null)
            {
                pool = new GameObject("ObjectPool");
            }
            
            // 子对象池子的父物体（对应预制体，设为对象池物体的子物体）
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            
            // 将刚生成的物体设置为子对象池物体的子物体
            _object.transform.SetParent(childPool.transform);
        }
        
        // 按预制体名字获取到对象池内一个物体并激活该物体，返回给调用者使用
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }
    
    /// <summary>
    /// 用完的物体放回对象池
    /// </summary>
    /// <param name="prefab"></param>
    public void PushObject(GameObject prefab)
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
        {
            objectPool.Add(_name, new Queue<GameObject>());
        }
        // 放入池中并取消激活
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}
