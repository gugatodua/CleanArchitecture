namespace Application
{
    public static class ImageValidator
    {
        private static List<string> _allowedExtensions;

        public static void Initialize(List<string> allowedExtensions)
        {
            _allowedExtensions = allowedExtensions ?? throw new ArgumentNullException(nameof(allowedExtensions));
        }

        public static bool IsAllowedType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return _allowedExtensions.Contains(extension);
        }
    }
}