namespace YmtSystem.Repository.Mongodb.Mapping
{
    using System; 

    public class EntityMappingConfigure
    {
        public Type MappType { get; set; }
        public string ToDatabase { get; set; }
        public string ToCollection { get; set; }
    }
}
