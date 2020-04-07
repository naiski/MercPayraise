using TaleWorlds.MountAndBlade;
using HarmonyLib;
using System;

namespace MercPayraise
{
    class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "MercPayraise";

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                new Harmony("mod.bannerlord.mercpayraise").PatchAll();
            }
            catch (Exception ex)
            {
                // TODO: Handle this.
            }
        }

    }
}
