using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab9A {
        interface IMyDictionary<TKey, TValue> {
        int Count { get; }
        void Add(TKey key, TValue value);
        bool Contains(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        bool Remove(TKey key);
    }

public class MyDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>, IEnumerable<(TKey, TValue)>
        where TKey : struct
{
        private class Element {
            public TKey key { get; set; }
            public TValue value { get; set; }

            public Element(TKey key, TValue value) {
                this.key = key;
                this.value = value;
            }
        }
        
        Element[] elements;
        int count;
        public int Count { get { return count; } }

        public MyDictionary() {
            this.count = 0;
            elements = new Element[4];
        }

        Element Search(TKey key) {
            for(int i = 0; i < count; i++) {
                if (elements[i].key.Equals(key)) {
                    Element tmp = elements[i];
                    if (Count > 0) {
                        elements[i] = elements[i - 1];
                        elements[i - 1] = tmp;
                    }
                    return tmp;
                }
            }
            return null;
        }
         Element SearchNoModify(TKey key) {
            for (int i = 0; i < count; i++) {
                if (elements[i].key.Equals(key)) {
                    return elements[i];
                }
            }
            return null;
        }

        public void Add(TKey key, TValue value) {
            Element elem = Search(key);
            if(elem != null){
                elem.value = value;
                return;
            }

            if(elements.Length == Count) {
                Element[] tmp = new Element[Count * 2];
                elements.CopyTo(tmp, 0);
                elements = tmp;
            }

            elements[count++] = new Element(key, value);
        }
        public bool Contains(TKey key) {
            Element tmp = SearchNoModify(key);
            return tmp != null;
        }
        public bool TryGetValue(TKey key, out TValue value) {
            Element tmp = SearchNoModify(key);

            if(tmp != null) {
                value = tmp.value;
                return true;
            }
            else {
                value = default!;
                return false;
            }
        }
        public bool Remove(TKey key) {
            for (int i = 0; i < count; i++) {
                if (elements[i].key.Equals(key)) {
                    count--;
                    for(int j = i; j < count; j++)
                        elements[count] = elements[count + 1];
                    elements[count] = null;
                    return true;
                }
            }

            return false;
        }

        public override string ToString() {
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < count; i++)
                content.Append($"[{elements[i].key} : {elements[i].value}]");
            return content.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<(TKey, TValue)> GetEnumerator() {
            for(int i = 0; i < count; i++) {
                Element tmp = elements[i];
                yield return (tmp.key, tmp.value);
            }
            
        }
    }

    public static class MyDictionaryExtensions {

        public static TKey[] GetKeys<TKey, TValue>(this MyDictionary<TKey, TValue> dict)
            where TKey : struct 
        {
            TKey[] tmp = new TKey[dict.Count]; 
            int index = 0;

            foreach (var elem in dict)
                tmp[index++] = elem.Item1;
            return tmp;
        }

        public static TValue MaxValue<TKey, TValue>(this MyDictionary<TKey, TValue> dict)
        where TKey : struct
        where TValue : IComparable<TValue> {
            TValue max;
            var iter = dict.GetEnumerator();

            if (iter.MoveNext())
                max = iter.Current.Item2;
            else
                return default!;

            while (iter.MoveNext()) {
                if (iter.Current.Item2.CompareTo(max) > 0)
                    max = iter.Current.Item2;
            }
            return max;
        }
    }

    
}

