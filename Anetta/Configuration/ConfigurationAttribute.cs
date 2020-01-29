using System;

namespace Anetta.Configuration
{
    public class ConfigurationAttribute : Attribute
    {
        public string Section { get; }

        public ConfigurationAttribute()
        {
        }

        public ConfigurationAttribute(string section)
        {
            Section = section;
        }
    }
}