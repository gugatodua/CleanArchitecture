namespace Application.CustomExceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException(string message) : base(message)
        {
            
        }
    }
}
