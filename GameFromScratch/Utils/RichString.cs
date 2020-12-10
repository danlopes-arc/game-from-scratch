using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameFromScratch.Utils
{
    public class RichString
    {
        private List<string> strings = new List<string>();
        private List<Color> colors = new List<Color>();

        public List<string> Strings
        {
            get => new List<string>(strings);
        }

        public List<Color> Colors
        {
            get => new List<Color>(colors);
        }

        public int Count { get; private set; }
        public Color DefaultColor { get; set; }
        public RichString(Color defaultColor)
        {
            DefaultColor = defaultColor;
        }

        public RichString() : this(Color.White)
        {
        }

        public RichString Append(string text, Color color)
        {
            strings.Add(text);
            colors.Add(color);
            Count++;
            return this;
        }
        public RichString Append(string text)
        {
            return Append(text, DefaultColor);
        }
        
        // public RichString AppendLine(string text, Color color)
        // {
        //     strings.Add(text + Environment.NewLine);
        //     colors.Add(color);
        //     Count++;
        //     return this;
        // }
        // public RichString AppendLine(string text)
        // {
        //     return Append(text, DefaultColor);
        // }

        public void Clear()
        {
            strings.Clear();
            colors.Clear();
            Count = 0;
        }
        
        public override string ToString()
        {
            var text = new StringBuilder();
            foreach (var s in strings)
            {
                text.Append(s);
            }

            return text.ToString();
        }
    }
}