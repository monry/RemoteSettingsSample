using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Application.Message;

namespace RemoteSettingsSample.Domain.UseCase.Interface.Entity
{
    public interface ISeasonMaster
    {
        bool Exists(Season season);
        SeasonText FindText(Season season);
        SeasonColor FindColor(Season season);
    }
}