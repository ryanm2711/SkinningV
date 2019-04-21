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
    class ShopBlip : Script
    {
        private List<Blip> ButcherBlips = new List<Blip>();

        private bool createdblips;

        public static Blip b;

        public ShopBlip()
        {
            Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!createdblips && Main.SkinnedPelts.Count > 0)
            {
                CreateBlip(new Vector3(-2507.07f, 3614.72f, 12.41f), "Butcher", BlipID.Butcher, false); // Butcher location #1
                createdblips = true;
            }
            else if (createdblips && Main.SkinnedPelts.Count <= 0)
            {
                foreach (Blip blip in ButcherBlips)
                {
                    blip.Remove();
                }
                ButcherBlips.Clear();
            }
        }

        public enum BlipID
        {
            Butcher = 188,
        }

        private void CreateBlip(Vector3 position, string text, BlipID sprite, bool shortrange)
        {
            var b = World.CreateBlip(position);
            Function.Call<int>(Hash.SET_BLIP_SPRITE, b, (int)sprite);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, b, shortrange);
            SetBlipText(text, b);

            ButcherBlips.Add(b);
        }

        private void SetBlipText(string text, Blip blipname)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
            Function.Call((Hash)0x6C188BE134E074AA, text); // This hash is this: ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, blipname);
        }
    }
}
