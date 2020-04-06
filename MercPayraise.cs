using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace MercPayraise
{
    [HarmonyPatch(typeof(DefaultClanFinanceModel), "CalculateClanIncome")]
    public class MercPayraisePatch
    {
        static int originalMultiplier;

        static void Prefix(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            originalMultiplier = clan.MercenaryAwardMultiplier;
            clan.MercenaryAwardMultiplier *= 10;
        }
        static void Postfix(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            clan.MercenaryAwardMultiplier = originalMultiplier;
        }
    }
}