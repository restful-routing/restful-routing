namespace RestfulRouting.Mappers
{
    public abstract class RoutePaths
    {
        public RoutePaths()
        {
            InitializeDefaults();
        }

        protected abstract void InitializeDefaults();

        public string Index { get; set; }
        public string Show { get; set; }
        public string New { get; set; }
        public string Create { get; set; }
        public string Edit { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public string Destroy { get; set; }
    }

    public class ResourceRoutePaths : RoutePaths
    {
        protected override void InitializeDefaults()
        {
            Index = "{resourcePath}/{indexName}";
            Show = "{resourcePath}";
            New = "{resourcePath}/{newName}";
            Create = "{resourcePath}";
            Edit = "{resourcePath}/{editName}";
            Update = "{resourcePath}";
            Delete = "{resourcePath}/{deleteName}";
            Destroy = "{resourcePath}";
        }
    }

    public class ResourcesRoutePaths : RoutePaths
    {
        protected override void InitializeDefaults()
        {
            Index = "{resourcePath}";
            Create = "{resourcePath}";
            New = "{resourcePath}/{newName}";
            Edit = "{resourcePath}/{id}/{editName}";
            Show = "{resourcePath}/{id}";
            Update = "{resourcePath}/{id}";
            Destroy = "{resourcePath}/{id}";
            Delete = "{resourcePath}/{id}/{deleteName}";
        }
    }
}