using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct SerializableKeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;
}
