namespace DisableWaterMark
{
    using EloBuddy;

    internal class Program
    {
        static void Main()
        {
            if (EloBuddy.Hacks.RenderWatermark)
            {
                EloBuddy.Hacks.RenderWatermark = false;
            }
            
            if (EloBuddy.Hacks.IngameChat)
            {
                EloBuddy.Hacks.IngameChat = false;
            }
        }
    }
}
