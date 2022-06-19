namespace Hafnia.Integrations.Shared.Helpers
{
    public static class MimeHelper
    {
        public static string ConvertToExtension(string mimeType)
        {
            return mimeType.ToLower() switch
            {
                // Image/*
                "image/gif" => "gif",
                "image/jpeg" => "jpg",
                "image/jpg" => "jpg",
                "image/png" => "png",

                _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, "No extension defined for provided MimeType")
            };
        }
    }
}
