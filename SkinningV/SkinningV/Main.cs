using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace SkinningV
{
    public class Main : Script
    {
        private static bool HasGoToButcherTextDisplayed;

        public static List<Ped> SkinnedAnimals = new List<Ped>();

        public static List<string> SkinnedPelts = new List<string>();

        public Main()
        {
            Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            string[] strings = { "Bad Pelt", "Good Pelt", "Rare Pelt", "Perfect Pelt", "Legendary Pelt" };
            Random random = new Random();
            string randomString = strings[random.Next(0, strings.Length)];

            foreach (var DeadAnimal in World.GetNearbyPeds(Game.Player.Character.Position, 30f))
            {
                if (Function.Call<bool>(Hash.IS_ENTITY_TOUCHING_ENTITY, Game.Player.Character, DeadAnimal))
                {
                    if (DeadAnimal.Exists() && DeadAnimal.IsDead && !DeadAnimal.IsHuman && !SkinnedAnimals.Contains(DeadAnimal))
                    {
                        TextBox("Press ~INPUT_CONTEXT~ to skin this animal.", true);

                        if (Game.IsControlJustPressed(2, Control.Context))
                        {
                            Function.Call(Hash.REQUEST_ANIM_DICT, "missarmenian3_gardener");
                            Wait(200);
                            Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "missarmenian3_gardener", "idle_a", 4f, 0f, 3200, 0, 4f, 0, 0);
                            Wait(2000);
                            Function.Call(Hash.STOP_ANIM_TASK, Game.Player.Character, "missarmenian3_gardener", "idle_a", 0);
                            SkinnedAnimals.Add(DeadAnimal);
                            if (randomString == "Bad Pelt")
                            {
                                TextBox("You got a: ~r~" + randomString, true);
                            }
                            else if (randomString == "Good Pelt")
                            {
                                TextBox("You got a: ~g~" + randomString, true);
                            }
                            else if (randomString == "Rare Pelt")
                            {
                                TextBox("You got a: ~b~" + randomString, true);
                            }
                            else
                            {
                                TextBox("You got a: ~y~" + randomString, true);
                            }
                            SkinnedPelts.Add(randomString);
                            HasGoToButcherTextDisplayed = false;
                            Wait(3000);
                            if (!HasGoToButcherTextDisplayed)
                            {
                                TextBox("Go to the ~y~Butcher's ~w~located on your map to sell your pelts.", true);
                                HasGoToButcherTextDisplayed = true;
                            }
                        }
                    }
                }

                if (!DeadAnimal.Exists())
                {
                    SkinnedAnimals.Remove(DeadAnimal); // Delete ped from list when they no longer exist.
                }
            }
        }

        // Global functions for script:

        public static void TextBox(string text, bool emitsound)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call((Hash)0x238FFE5C7B0498A6, 0, 0, emitsound, -1); // The hash being used here is: END_TEXT_COMMAND_DISPLAY_HELP        }
        }
    }
}
