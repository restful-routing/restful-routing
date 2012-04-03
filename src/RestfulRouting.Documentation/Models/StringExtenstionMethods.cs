namespace RestfulRouting.Documentation.Models {
    public static class StringExtenstionMethods {
        public static string With(this string format, params object[] args) {
            return string.Format(format, args);
        }
    }
}