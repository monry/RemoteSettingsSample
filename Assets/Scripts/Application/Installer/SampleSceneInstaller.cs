using System.Collections.Generic;
using RemoteSettingsSample.Application.ValueObject;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Application.Installer
{
    public class SampleSceneInstaller : MonoInstaller<SampleSceneInstaller>
    {
        [SerializeField] private List<SeasonInformation> seasonTexts = new List<SeasonInformation>();
        private IEnumerable<SeasonInformation> SeasonTexts => seasonTexts;

        public override void InstallBindings()
        {
            Container.BindInstance(SeasonTexts);
        }
    }
}