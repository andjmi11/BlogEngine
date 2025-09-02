using BlogAPI.Models;

namespace BlogAPI.Helpers
{
    public class Helper
    {
        public ICollection<BlogTags> ToNormalizedBlogTags(string tags)
        {
            if (string.IsNullOrWhiteSpace(tags))
                return new List<BlogTags>();

            return tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim().ToLower())
                .Distinct()
                .Select(tag => new BlogTags { TagName = tag })
                .ToList();
        }
    }
}
