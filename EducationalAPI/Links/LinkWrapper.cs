namespace EducationalAPI.Links
{
    public class LinkWrapper<T> : LinkBase
    {
        public IEnumerable<T> Value { get; set; } = new List<T>();

        public LinkWrapper()
        {
        }

        public LinkWrapper(IEnumerable<T> value)
        {
            Value = value;
        }
    }
}
