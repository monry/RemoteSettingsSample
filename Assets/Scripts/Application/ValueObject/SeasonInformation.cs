using System;
using RemoteSettingsSample.Application.Enum;
using UnityEngine;

namespace RemoteSettingsSample.Application.ValueObject
{
    [Serializable]
    public struct SeasonInformation
    {
        // Avoid CS0649 warnings
        private SeasonInformation(Season season, string title, string body)
        {
            this.season = season;
            this.title = title;
            this.body = body;
        }

        [SerializeField] private Season season;
        [SerializeField] private string title;
        [SerializeField] private string body;

        public Season Season => season;
        public string Title => title;
        public string Body => body;
    }
}