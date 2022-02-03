using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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
            Image image = new()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Source = t.IconUri.BitmapImage()
            };
            image.SetGridCoords(t.Pos);
            grid.Children.Add(image);
            TextBlock textBlock = new()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Text = t.Name                
            };
            textBlock.SetGridCoords(t.Pos);
            grid.Children.Add(textBlock);
        }
        public static Ellipse Add(this Grid grid, Player p)
        {
            Ellipse circle = new()
            {
                Fill = new SolidColorBrush(p.Color)
            };
            circle.SetGridCoords(p.Pos);
            grid.Children.Add(circle);
            return circle;
        }
        public static void SetGridCoords(this UIElement el, int x, int y)
        {
            Grid.SetColumn(el, x);
            Grid.SetRow(el, 7 - y);
        }
        public static void SetGridCoords(this UIElement el, (int x, int y) pos) => SetGridCoords(el, pos.x, pos.y);
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
    public static class Constants
    {
        public static string WorkingDirectory => Environment.CurrentDirectory;
        public const byte PlayerOpacity = 200;
    }
    public static class ColorExtensions 
    {
        public static Color PlayerColor(this Color c)
        {
            c.A = Constants.PlayerOpacity;
            return c;
        }
    }
}
