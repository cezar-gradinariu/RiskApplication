namespace Domain.Models
{
    public class CustomerStatics
    {
        public int Customer { get; set; }
        public decimal AverageStake { get; set; }
        public int TotalBets { get; set; }
        public int TotalBetsWon { get; set; }

        public double WinningPercentage
        {
            get
            {
                return TotalBets == 0
                    ? 0
                    : TotalBetsWon*100.0/TotalBets;
            }
        }
    }
}