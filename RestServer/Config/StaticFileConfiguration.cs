using System;

namespace RestServer.Config
{
    public class StaticFileConfiguration
    {
        public string Prefix { get; set; }
        public string BaseRoot { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public bool ShowDirectories { get; set; } = true;

        public StaticFileConfiguration(string Prefix) {
            this.Prefix = Prefix;
        }
        public StaticFileConfiguration(string Prefix, string BaseRoot):this(Prefix)
        {
            this.BaseRoot = BaseRoot;
        }
        public StaticFileConfiguration(string Prefix, string BaseRoot, bool ShowDirectories) : this(Prefix, BaseRoot)
        {
            this.ShowDirectories = ShowDirectories;
        }
    }
}
