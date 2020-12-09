using System;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Utils
{
    public static class BetterMouseState
    {
        public static MouseState Last { get; private set; } = Mouse.GetState();
        public static MouseState Current { get; private set; } = Mouse.GetState();

        public static bool IsDown(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => Current.LeftButton == ButtonState.Pressed,
                MouseButton.Right => Current.RightButton == ButtonState.Pressed,
                MouseButton.Middle => Current.MiddleButton == ButtonState.Pressed,
                MouseButton.X1 => Current.XButton1 == ButtonState.Pressed,
                MouseButton.X2 => Current.XButton2 == ButtonState.Pressed,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };
        }

        public static bool IsJustDown(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => Current.LeftButton == ButtonState.Pressed && Last.LeftButton != ButtonState.Pressed,
                MouseButton.Right => Current.RightButton == ButtonState.Pressed &&
                                     Last.RightButton != ButtonState.Pressed,
                MouseButton.Middle => Current.MiddleButton == ButtonState.Pressed &&
                                      Last.MiddleButton != ButtonState.Pressed,
                MouseButton.X1 => Current.XButton1 == ButtonState.Pressed && Last.XButton1 != ButtonState.Pressed,
                MouseButton.X2 => Current.XButton2 == ButtonState.Pressed && Last.XButton2 != ButtonState.Pressed,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };
        }

        public static bool IsJustUp(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => Current.LeftButton == ButtonState.Released && Last.LeftButton != ButtonState.Released,
                MouseButton.Right => Current.RightButton == ButtonState.Released &&
                                     Last.RightButton != ButtonState.Released,
                MouseButton.Middle => Current.MiddleButton == ButtonState.Released &&
                                      Last.MiddleButton != ButtonState.Released,
                MouseButton.X1 => Current.XButton1 == ButtonState.Released && Last.XButton1 != ButtonState.Released,
                MouseButton.X2 => Current.XButton2 == ButtonState.Released && Last.XButton2 != ButtonState.Pressed,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };
        }

        public static void Update()
        {
            Last = Current;
            Current = Mouse.GetState();
        }
    }
}
