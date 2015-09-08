namespace Domain.Models
{
    public class UnsettledBetWithRiskAnalysis
    {
        public double WinningPercentage { get; set; }
        public RiskAnalysis RiskAnalysis { get; set; }
        public UnsettledBet UnsettledBet { get; set; }
    }
}