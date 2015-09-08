using AutoMapper;
using Domain.Models;
using Portal.Models;

namespace Portal
{
    public class AutomapperPortalProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            CreateMap<UnsettledBetWithRiskAnalysis, UnsettledBetAndRiskViewModel>()
                .ForMember(p => p.Customer, opt => opt.MapFrom(t => t.UnsettledBet.Customer))
                .ForMember(p => p.Event, opt => opt.MapFrom(t => t.UnsettledBet.Event))
                .ForMember(p => p.Participant, opt => opt.MapFrom(t => t.UnsettledBet.Participant))
                .ForMember(p => p.Stake, opt => opt.MapFrom(t => t.UnsettledBet.Stake))
                .ForMember(p => p.ToWin, opt => opt.MapFrom(t => t.UnsettledBet.ToWin))
                .ForMember(p => p.IsHighPrize, opt => opt.MapFrom(t => t.RiskAnalysis.IsHighPrize))
                .ForMember(p => p.IsHighStake, opt => opt.MapFrom(t => t.RiskAnalysis.IsHighStake))
                .ForMember(p => p.IsUnusuallyHighStake, opt => opt.MapFrom(t => t.RiskAnalysis.IsUnusuallyHighStake))
                .ForMember(p => p.HasUnusualWinRate, opt => opt.MapFrom(t => t.RiskAnalysis.HasUnusualWinRate));

        }
    }
}