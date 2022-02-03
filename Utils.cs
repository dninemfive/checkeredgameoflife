using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CheckeredGameOfLife
{
    public class DefaultDict<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> InternalDict = new();
        private TValue Default;
        public DefaultDict(TValue @default = default)
        {
            Default = @default;
        }
        public TValue this[TKey key]
        {
            get => InternalDict.ContainsKey(key) ? InternalDict[key] : Default;
            set => InternalDict[key] = value;
        }
        public ICollection<TKey> Keys => InternalDict.Keys;
        public ICollection<TValue> Values => InternalDict.Values;
        public int Count => InternalDict.Count;
        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value) => InternalDict[key] = value;
        public void Add(KeyValuePair<TKey, TValue> item) => InternalDict.Add(item.Key, item.Value);
        public void Clear() => InternalDict.Clear();
        public bool Contains(KeyValuePair<TKey, TValue> item) => InternalDict.Contains(item);
        public bool ContainsKey(TKey key) => InternalDict.ContainsKey(key);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => InternalDict.GetEnumerator();
        public bool Remove(TKey key) => InternalDict.Remove(key);
        public bool Remove(KeyValuePair<TKey, TValue> item) => InternalDict.Remove(item.Key);
        public bool TryGetValue(TKey key, out TValue value) => InternalDict.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public static class GridExtensions
    {
        public static void Add(this Grid grid, Tile t)
        {
            Image toAdd = new()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Source = t.IconUri.BitmapImage()
            };
            Grid.SetRow(toAdd, 7 - t.Pos.y);
            Grid.SetColumn(toAdd, t.Pos.x);
            grid.Children.Add(toAdd);
        }        
    }
    public static class UriExtensions
    {
        public static BitmapImage BitmapImage(this Uri uri)
        {
            BitmapImage ret = new();
            ret.BeginInit();
            ret.UriSource = uri;
            ret.EndInit();
            return ret;
        }
    }
}
