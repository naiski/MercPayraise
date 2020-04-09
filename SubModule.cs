using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace MercPayraise
{
    class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "MercPayraise";

        protected override void OnSubModuleLoad()
        {
            MercPayraiseConfig.Initialize();
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("MercPayraiseConfig", new TextObject("Mercenary Payraise Options"), 9998, delegate
            {
                ScreenManager.PushScreen(new MercPayraiseGauntletScreen());
            }, false));
            
            try
            {
                new Harmony("mod.bannerlord.mercpayraise").PatchAll();
            }
            catch (Exception e)
            {
                // TODO: Handle this.
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            // Check that the game type is a campaign and return if it isn't.
            if (game.GameType as Campaign == null)
            {
                return;
            }
            ((CampaignGameStarter)gameStarterObject).AddBehavior(new ResetMercenaryAward());
        }
    }
}
