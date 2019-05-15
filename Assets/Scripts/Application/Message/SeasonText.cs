using RemoteSettingsSample.Application.ValueObject;
using Zenject;

namespace RemoteSettingsSample.Application.Message
{
    public struct SeasonText
    {
        public SeasonText(string title, string body)
        {
            Title = title;
            Body = body;
        }

        public SeasonText(SeasonInformation seasonInformation)
        {
            Title = seasonInformation.Title;
            Body = seasonInformation.Body;
        }

        public string Title { get; }
        public string Body { get; }

        public static class Factory
        {
            public class FromStrings : PlaceholderFactory<string, string, SeasonText>
            {
            }

            public class FromSeasonInformation : PlaceholderFactory<SeasonInformation, SeasonText>
            {
            }
        }
    }
}