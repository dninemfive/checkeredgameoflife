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
        public static void Add(this Grid grid, UIElement el, (int x, int y) pos)
        {
            el.SetGridCoords(pos);
            grid.Children.Add(el);
        }
        public static void Add(this Grid grid, Tile t)
        {
            grid.Add(UIElements.Image(t.IconUri), t.Pos);
            grid.Add(UIElements.TextBlock(t.Name), t.Pos);
        }
        public static Ellipse Add(this Grid grid, Player p)
        {
            Ellipse circle = UIElements.Circle(p.Color.PlayerColor());
            grid.Add(circle, p.Pos);
            grid.Add(UIElements.TextBlock(p.Name), p.Pos);
            return circle;
        }
        public static void SetGridCoords(this UIElement el, int x, int y)
        {
            Grid.SetColumn(el, x);
            Grid.SetRow(el, (Constants.GridSize - 1) - y);
        }
        public static void SetGridCoords(this UIElement el, (int x, int y) pos) => SetGridCoords(el, pos.x, pos.y);
        public static void HighlightCoords(this Grid grid, int x, int y)
        {
            grid.Add(UIElements.Square(Constants.HighlightColor), (x, y));
        }
        public static void HighlightCoords(this Grid grid, (int x, int y) pos) => HighlightCoords(grid, pos.x, pos.y);
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
        public const int GridSize = 8;
        public const int DieSize = 6;
        public const int WinPoints = 100;
        public static readonly Color HighlightColor = Colors.Yellow.SetOpacity(120);
        public static readonly List<Color> CpuPlayerColors = new()
        {
            Colors.Red,
            Colors.Blue,
            Colors.Green,
            Colors.Purple,
            Colors.Orange
        };
        public static readonly List<Color> TakenCpuColors = new();
        public static List<Color> NonTakenCpuColors => CpuPlayerColors.Where(x => !TakenCpuColors.Contains(x)).ToList();
    }
    public static class MiscExtensions
    {
        public static Color SetOpacity(this Color c, byte opacity)
        {
            c.A = opacity;
            return c;
        }
        public static Color PlayerColor(this Color c) => SetOpacity(c, Constants.PlayerOpacity);
        public static bool IsInRange(this int n, int min, int max) => n >= min && n < max;
        public static bool IsInbounds(this int n) => n.IsInRange(0, Constants.GridSize);
        public static bool IsInbounds(this (int x, int y) pos) => pos.x.IsInbounds() && pos.y.IsInbounds();
        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(Game.Random.Next(0, enumerable.Count())); 
    }
    public static class UIElements
    {
        public static Image Image(Uri uri) => new()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Source = uri.BitmapImage()
        };
        public static TextBlock TextBlock(string text) => new()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Text = text
        };
        public static Ellipse Circle(Color color) => new()
        {
            Fill = new SolidColorBrush(color)
        };
        public static Rectangle Square(Color color) => new()
        {
            Fill = new SolidColorBrush(color)
        };
}
}
