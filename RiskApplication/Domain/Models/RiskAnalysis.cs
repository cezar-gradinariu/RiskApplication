namespace Domain.Models
{
    public class RiskAnalysis
    {
        public bool IsHighPrize { get; set; }
        public bool IsUnusuallyHighStake { get; set; }
        public bool IsHighStake { get; set; }
        public bool HasUnusualWinRate { get; set; }
    }
}