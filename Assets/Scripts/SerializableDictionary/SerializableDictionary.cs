using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> entries = new();
    
    public void Add(TKey key, TValue value)
    {
        entries.Add(new SerializableKeyValuePair<TKey, TValue> { key = key, value = value });
    }
    
    public Dictionary<TKey, TValue> ToDictionary()
    {
        var dict = new Dictionary<TKey, TValue>();
        foreach (var entry in entries)
        {
            dict[entry.key] = entry.value;
        }
        
        return dict;
    }
    
    public TValue GetValue(TKey key)
    {
        foreach (var entry in entries)
        {
            if (entry.key.Equals(key))
            {
                return entry.value;
            }
        }
        throw new KeyNotFoundException($"Ключ {key} не найден в словаре!");
    }
    
    public bool TryGetValue(TKey key, out TValue value)
    {
        foreach (var entry in entries)
        {
            if (!entry.key.Equals(key)) continue;
            
            value = entry.value;
            return true;
        }
        
        value = default;
        return false;
    }
    
}
// public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
// {
//     [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> serializedPairs = new();
//
//     private int lastSerializedCount = -1;
//
//     public void OnBeforeSerialize()
//     {
//         if (Count == lastSerializedCount) return;
//         
//         serializedPairs.Clear();
//         foreach (var pair in this)
//         {
//             serializedPairs.Add(new SerializableKeyValuePair<TKey, TValue> { Key = pair.Key, Value = pair.Value });
//         }
//
//         lastSerializedCount = Count;
//     }
//
//     public void OnAfterDeserialize()
//     {
//         Clear();
//         foreach (var pair in serializedPairs)
//         {
//             this[pair.Key] = pair.Value;
//         }
//     }
// }