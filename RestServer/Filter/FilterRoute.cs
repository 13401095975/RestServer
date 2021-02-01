namespace RestServer.Filter
{
    public class FilterRoute
    {
        public string Prefix { get; set; }
        public IFilter Filter { get; set; }

        public FilterRoute() { }
        public FilterRoute(string prefix, IFilter filter) {
            this.Prefix = prefix;
            this.Filter = filter;
        }
    }
}
