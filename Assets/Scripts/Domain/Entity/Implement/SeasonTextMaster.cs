using System.Collections.Generic;
using System.Linq;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Application.Message;
using RemoteSettingsSample.Application.ValueObject;
using RemoteSettingsSample.Domain.UseCase.Interface.Entity;

namespace RemoteSettingsSample.Domain.Entity.Implement
{
    public class SeasonTextMaster : ISeasonTextMaster
    {
        public SeasonTextMaster(IEnumerable<SeasonInformation> seasonTexts, SeasonText.Factory.FromSeasonInformation seasonTextFactory)
        {
            SeasonTexts = seasonTexts;
            SeasonTextFactory = seasonTextFactory;
        }

        private IEnumerable<SeasonInformation> SeasonTexts { get; }
        private SeasonText.Factory.FromSeasonInformation SeasonTextFactory { get; }

        bool ISeasonTextMaster.Exists(Season season) =>
            SeasonTexts.Any(x => x.Season == season);

        SeasonText ISeasonTextMaster.FindText(Season season) =>
            SeasonTextFactory.Create(SeasonTexts.FirstOrDefault(x => x.Season == season));
    }
}