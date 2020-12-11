using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        #region Oprators

        public static RichString operator +(RichString r, string s)
        {
            return r.Append(s);
        }

        public static RichString operator +(string s, RichString r)
        {
            return r.Append(s);
        }

        public static RichString operator +(RichString r, (string text, Color color) sc)
        {
            return r.Append(sc.text, sc.color);
        }

        public static RichString operator +((string text, Color color) sc, RichString r)
        {
            return r.Append(sc.text, sc.color);
        }

        #endregion

        public RichString Append(string text, Color color)
        {
            text = Regex.Replace(text, @"(\r\n)|\n", Environment.NewLine);
            var lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                var txt = lines[i];
                if (txt != "")
                {
                    strings.Add(txt);
                    colors.Add(color);
                    Count++;
                }

                if (i < lines.Length - 1)
                {
                    strings.Add(Environment.NewLine);
                    colors.Add(color);
                    Count++;
                }
            }

            return this;
        }

        public RichString Append(string text)
        {
            return Append(text, DefaultColor);
        }

        public RichString AppendLine(string text, Color color)
        {
            return Append(text + Environment.NewLine, color);
        }

        public RichString AppendLine(string text)
        {
            return Append(text, DefaultColor);
        }

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