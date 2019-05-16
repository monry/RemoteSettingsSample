using System.Drawing;
using RemoteSettingsSample.Application.ValueObject;
using Zenject;

namespace RemoteSettingsSample.Application.Message
{
    public struct SeasonColor
    {
        public SeasonColor(Color color)
        {
            Color = color;
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