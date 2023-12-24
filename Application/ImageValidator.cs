namespace Application
{
    public static class ImageValidator
    {
        public static bool IsAllowedType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            return AllowedExtensions.Contains(ext.ToLower());
        }

        private static readonly List<string> AllowedExtensions = new List<string>
        {
            ".png",
            ".jpg"
        };
    }
}