using System.Collections.Generic;
using Application.Installer;
using RemoteSettingsSample.Application.ValueObject;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Application.Installer.Scene
{
    public class SampleSceneInstaller : MonoInstaller<SampleSceneInstaller>
    {
        [SerializeField] private List<SeasonInformation> seasonTexts = new List<SeasonInformation>();
        private IEnumerable<SeasonInformation> SeasonTexts => seasonTexts;

        public override void InstallBindings()
        {
            SeasonInstaller.Install(Container);

            // ValueObjects
            Container.BindInstance(SeasonTexts);
        }
    }
}