using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;

namespace SkinningV
{
    class NPCShop : Script
    {

        public NPCShop()
        {
            Tick += OnTick;
        }

        private enum PeltValues
        {
            BadPelt = 100,
            GoodPelt = 350,
            RarePelt = 500,
            PerfectPelt = 1000,
            LegendaryPelt = 5000
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (Main.SkinnedPelts.Count() > 0)
            {
                Function.Call(Hash.DRAW_MARKER, 1, -2507.07f, 3614.72f, 12.41f, 0f, 0f, 0f, 0f, 0f, 0f, 1.6f, 1.6f, 1.5f, 255, 255, 0, 150, false, false, 3, false, false, false);
            }

            if (Game.Player.Character.Position.DistanceTo(new Vector3(-2507.07f, 3614.72f, 13.84f)) < 1f)
            {
                if (Main.SkinnedPelts.Count() > 0)
                {
                    Main.TextBox("Press ~INPUT_CONTEXT~ to sell your skinned pelts.", true);
                }

                if (Game.IsControlJustPressed(2, Control.Context))
                {
                    var HowManyOfThosePelts = Main.SkinnedPelts;

                    int EarnedMoney = 0;

                    if (HowManyOfThosePelts.Contains("Bad Pelt"))
                    {
                        EarnedMoney = EarnedMoney + (int)PeltValues.BadPelt;
                    }
                    if (HowManyOfThosePelts.Contains("Good Pelt"))
                    {
                        EarnedMoney = EarnedMoney + (int)PeltValues.GoodPelt;
                    }
                    if (HowManyOfThosePelts.Contains("Rare Pelt"))
                    {
                        EarnedMoney = EarnedMoney + (int)PeltValues.RarePelt;
                    }
                    if (HowManyOfThosePelts.Contains("Perfect Pelt"))
                    {
                        EarnedMoney = EarnedMoney + (int)PeltValues.PerfectPelt;
                    }
                    if (HowManyOfThosePelts.Contains("Legendary Pelt"))
                    {
                        EarnedMoney = EarnedMoney + (int)PeltValues.LegendaryPelt;
                    }
                    Game.Player.Money = Game.Player.Money + EarnedMoney;
                    Main.TextBox("You have earned a total of~g~ $" + EarnedMoney, true);

                    Main.SkinnedPelts.Clear();
                }
            }
        }
    }
}
