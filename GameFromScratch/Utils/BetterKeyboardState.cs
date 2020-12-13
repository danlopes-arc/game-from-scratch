using Microsoft.Xna.Framework.Input;

namespace PlanetDefender.Utils
{
    public static class BetterKeyboardState
    {
        public static KeyboardState Last { get; private set; } = Keyboard.GetState();
        public static KeyboardState Current { get; private set; } = Keyboard.GetState();

        public static bool IsJustDown(Keys key) =>
            Current.IsKeyDown(key) && !Last.IsKeyDown(key);
        public static bool IsJustUp(Keys key) =>
            Current.IsKeyUp(key) && !Last.IsKeyUp(key);


        public static void Update()
        {
            Last = Current;
            Current = Keyboard.GetState();
        }
    }
}
