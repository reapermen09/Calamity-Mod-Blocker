using Terraria.ModLoader;
using System.Windows.Forms;
using Terraria;
using System.Threading;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace CalamityModBlocker
{
    public class CalamityModBlocker : Mod
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod != null)
            {
                MessageBox.Show("Calamity Mod is enabled! Please disable it to continue.",
                                "Calamity Mod Blocker v103452894",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
    public class Consequences : ModSystem
    {
        private bool calamityOn = false;
        private int timer = 1800;
        private int timer2 = 60;
        Color color;
        public override void PostUpdateTime()
        {
            if (calamityOn == true)
            {
                if (timer > 0)
                    timer--;
                else
                {

                    WorldGen.meteor(WorldGen.genRand.Next(Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY));
                    WorldGen.PlaceLiquid(WorldGen.genRand.Next(Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY), (byte)LiquidID.Lava, 10);
                    Main.LocalPlayer.position.X = Main.rand.Next(Main.maxTilesX);
                    Main.LocalPlayer.position.Y = Main.rand.Next(Main.maxTilesY);
                    if (timer2 > 0)
                        timer2--;
                    else
                    {
                        color.R = (byte)Main.rand.Next(255);
                        color.G = (byte)Main.rand.Next(255);
                        color.B = (byte)Main.rand.Next(255);
                        ModifySunLightColor(ref color, ref color);
                        timer2 = 60;
                    }
                }
            }
        }
        public override void OnWorldLoad()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod != null)
            {
                MessageBox.Show("Calamity Mod is enabled! YOU HAVE 30 SECONDS BEFORE WE DESTROY YOUR WORLD WITH METEORS!!!",
                    "Calamity Mod Blocker v103452894",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                calamityOn = true;
            }
        }
    }
}