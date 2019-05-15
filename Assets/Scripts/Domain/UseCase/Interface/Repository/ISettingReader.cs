using RemoteSettingsSample.Application.Enum;

namespace RemoteSettingsSample.Domain.UseCase.Interface.Repository
{
    public interface ISettingReader
    {
        Season ReadSeason();
    }
}