using RemoteSettingsSample.Domain.UseCase.Interface.Entity;
using UniRx;
using Zenject;

namespace RemoteSettingsSample.Domain.UseCase.Implement
{
    public class ChangeSeason : IInitializable
    {
        public ChangeSeason(ISeasonMaster seasonMaster, ISeasonState seasonState, SignalBus signalBus)
        {
            SeasonMaster = seasonMaster;
            SeasonState = seasonState;
            SignalBus = signalBus;
        }

        // Entities
        private ISeasonMaster SeasonMaster { get; }
        private ISeasonState SeasonState { get; }

        // Others
        private SignalBus SignalBus { get; }

        void IInitializable.Initialize()
        {
            SeasonState
                .Where(SeasonMaster.Exists)
                .Select(SeasonMaster.FindText)
                .Subscribe(SignalBus.Fire);
            SeasonState
                .Where(SeasonMaster.Exists)
                .Select(SeasonMaster.FindColor)
                .Subscribe(SignalBus.Fire);
        }
    }
}