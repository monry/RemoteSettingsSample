using System;
using RemoteSettingsSample.Application.Enum;
using UnityEngine;

namespace RemoteSettingsSample.Application.ValueObject
{
    [Serializable]
    public struct SeasonInformation
    {
        public SeasonInformation(Season season, string title, string body, Color color)
        {
            this.season = season;
            this.title = title;
            this.body = body;
            this.color = color;
        }

        [SerializeField] private Season season;
        [SerializeField] private string title;
        [SerializeField] private string body;
        [SerializeField] private Color color;

        public Season Season => season;
        public string Title => title;
        public string Body => body;
        public Color Color => color;
    }
}