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
            IndexName = RouteSet.RouteToLowercase ? "index" : "Index";
            ShowName = RouteSet.RouteToLowercase ? "show" : "Show";
            NewName = RouteSet.RouteToLowercase ? "new" : "New";
            CreateName = RouteSet.RouteToLowercase ? "create" : "Create";
            EditName = RouteSet.RouteToLowercase ? "edit" : "Edit";
            UpdateName = RouteSet.RouteToLowercase ? "update" : "Update";
            DeleteName = RouteSet.RouteToLowercase ? "delete" : "Delete";
            DestroyName = RouteSet.RouteToLowercase ? "destroy" : "Destroy";
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
