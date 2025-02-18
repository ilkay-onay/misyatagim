  using System.Text.RegularExpressions;

    namespace ProductService.Infrastructure.Services;

    public static class Slugify
    {
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

             text = text.ToLowerInvariant();
              text = Regex.Replace(text, @"\s+", "-");
            text = Regex.Replace(text, @"[^\w\-]+", "");
              text = Regex.Replace(text, @"\-\-+", "-");
            text = text.Trim('-');
           
            return text;
        }
    }