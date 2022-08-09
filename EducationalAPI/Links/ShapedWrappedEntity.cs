namespace EducationalAPI.Links
{
        public class ShapedWrappedEntity
        {
            public ShapedWrappedEntity()
            {
                WrappedEntity = new WrappedEntity();
            }

            public int Id { get; set; }
            public WrappedEntity WrappedEntity { get; set; }
        }
}
