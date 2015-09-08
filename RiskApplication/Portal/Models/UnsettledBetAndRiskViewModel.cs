namespace Portal.Models
{
    public class UnsettledBetAndRiskViewModel
    {
        public int Customer { get; set; }
        public int Event { get; set; }
        public int Participant { get; set; }
        public int Stake { get; set; }
        public int ToWin { get; set; }
        public bool IsHighPrize { get; set; }
        public bool IsUnusuallyHighStake { get; set; }
        public bool IsHighStake { get; set; }
        public bool HasUnusualWinRate { get; set; }
    }
}