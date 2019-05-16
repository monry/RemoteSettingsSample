using System.Collections.Generic;
using System.Linq;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Application.Message;
using RemoteSettingsSample.Application.ValueObject;
using RemoteSettingsSample.Domain.UseCase.Interface.Entity;

namespace RemoteSettingsSample.Domain.Entity.Implement
{
    public class SeasonMaster : ISeasonMaster
    {
        public SeasonMaster(IEnumerable<SeasonInformation> seasonInformations, SeasonText.Factory.FromSeasonInformation seasonTextFactory, SeasonColor.Factory.FromSeasonInformation seasonColorFactory)
        {
            SeasonInformations = seasonInformations;
            SeasonTextFactory = seasonTextFactory;
            SeasonColorFactory = seasonColorFactory;
        }

        private IEnumerable<SeasonInformation> SeasonInformations { get; }
        private SeasonText.Factory.FromSeasonInformation SeasonTextFactory { get; }
        private SeasonColor.Factory.FromSeasonInformation SeasonColorFactory { get; }

        bool ISeasonMaster.Exists(Season season) =>
            SeasonInformations.Any(x => x.Season == season);

        SeasonText ISeasonMaster.FindText(Season season) =>
            SeasonTextFactory.Create(SeasonInformations.FirstOrDefault(x => x.Season == season));

        SeasonColor ISeasonMaster.FindColor(Season season) =>
            SeasonColorFactory.Create(SeasonInformations.FirstOrDefault(x => x.Season == season));
    }
}