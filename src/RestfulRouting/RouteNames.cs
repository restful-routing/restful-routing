namespace RestfulRouting
{
    public class RouteNames
    {
        public RouteNames()
        {
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            IndexName = RouteSet.UseLowercase ? "index" : "Index";
            ShowName = RouteSet.UseLowercase ? "show" : "Show";
            NewName = RouteSet.UseLowercase ? "new" : "New";
            CreateName = RouteSet.UseLowercase ? "create" : "Create";
            EditName = RouteSet.UseLowercase ? "edit" : "Edit";
            UpdateName = RouteSet.UseLowercase ? "update" : "Update";
            DeleteName = RouteSet.UseLowercase ? "delete" : "Delete";
            DestroyName = RouteSet.UseLowercase ? "destroy" : "Destroy";
        }

        public string IndexName { get; set; }

        public string ShowName { get; set; }

        public string CreateName { get; set; }

        public string UpdateName { get; set; }

        public string DestroyName { get; set; }

        public string NewName { get; set; }

        public string EditName { get; set; }

        public string DeleteName { get; set; }
    }
}
