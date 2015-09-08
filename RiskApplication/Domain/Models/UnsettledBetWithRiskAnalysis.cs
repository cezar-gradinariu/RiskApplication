namespace Domain.Models
{
    public class UnsettledBetWithRiskAnalysis
    {
        public RiskAnalysis RiskAnalysis { get; set; }
        public UnsettledBet UnsettledBet { get; set; }
    }
}