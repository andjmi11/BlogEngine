namespace BlogApp.Components.Helpers
{
    public static class Utilities
    {
        private static readonly string[] _colorClasses = new string[] { "primary", "success", "info", "danger", "warning", "dark" };
        public static string GetRandomColorClasses() =>
            _colorClasses.OrderBy(c => Guid.NewGuid()).First();
    }
}
