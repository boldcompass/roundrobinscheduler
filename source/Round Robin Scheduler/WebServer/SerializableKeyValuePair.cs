using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public struct SerializableKeyValuePair<K, V>
    {
        [XmlElement("Key")]
        public K Key;

        [XmlElement("Value")]
        public V Value;

        public SerializableKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

    }
}