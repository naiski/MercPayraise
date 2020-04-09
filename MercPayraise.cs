using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using Helpers;
using System;
using System.Reflection;

namespace MercPayraise
{
    class ResetMercenaryAward : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
        }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            if (Clan.PlayerClan.IsUnderMercenaryService)
            {
                Clan.PlayerClan.MercenaryAwardMultiplier = FactionHelper.GetMercenaryAwardFactorToJoinKingdom(Clan.PlayerClan, Clan.PlayerClan.Kingdom, false) * MercPayraiseConfig.MercPayMultiplierValue;
            }
        }

        public override void SyncData(IDataStore dataStore) { }
    }
    // Patch GetMercenaryAwardFactor to multiply the player's payment for working as a mercenary.
    [HarmonyPatch]
    public class LordConversationsCampaignBehaviorPatch
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method("SandBox.LordConversationsCampaignBehavior:GetMercenaryAwardFactor");
        }

        static void Postfix(ref int __result)
        {
            __result = __result * MercPayraiseConfig.MercPayMultiplierValue;
        }
    }
}