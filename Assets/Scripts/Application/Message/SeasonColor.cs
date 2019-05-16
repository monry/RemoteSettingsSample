using RemoteSettingsSample.Application.ValueObject;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Application.Message
{
    public struct SeasonColor
    {
        public SeasonColor(SeasonInformation seasonInformation)
        {
            Color = seasonInformation.Color;
        }

        public Color Color { get; }

        public static class Factory
        {
            public class FromSeasonInformation : PlaceholderFactory<SeasonInformation, SeasonColor>
            {
            }
        }
    }
}