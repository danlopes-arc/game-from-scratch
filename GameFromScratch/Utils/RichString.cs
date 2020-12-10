using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFromScratch.Utils
{
    public class RichString
    {
        private List<string> strings = new List<string>();
        private List<Color> colors = new List<Color>();
        
        public List<string> Strings { get => new List<string>(strings); }
        public List<Color> Colors { get => new List<Color>(colors); }
        public int Count { get; private set; }
        
        public RichString Append(string text, Color color)
        {
            strings.Add(text);
            colors.Add(color);
            Count++;
            return this;
        }

    }
}