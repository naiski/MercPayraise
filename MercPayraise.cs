using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using System;
using System.Reflection;

namespace MercPayraise
{
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
            __result = __result * 10;
        }
    }

    // Patch CalculateClanIncome to fix a bug where the contract payment would be calculated as zero.
    [HarmonyPatch(typeof(DefaultClanFinanceModel), "CalculateClanIncome")]
    public class MercPayraisePatch
    {
        static void Postfix(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            if (clan == Clan.PlayerClan && clan.Influence * 0.1f < 1)
            {
                goldChange.Add(clan.MercenaryAwardMultiplier, new TextObject("{=qcaaJLhx}Mercenary Contract", null));
            }
        }
    }
}