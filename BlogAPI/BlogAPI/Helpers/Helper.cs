using BlogAPI.Models;

namespace BlogAPI.Helpers
{
    public class Helper
    {
        public ICollection<BlogTags> ToNormalizedBlogTags(List<string> tags)
        {
            return tags.Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim().ToLower())
                .Distinct()
                .Select(tag => new BlogTags { TagName = tag })
                .ToList() ?? new List<BlogTags>();
        }
    }
}
