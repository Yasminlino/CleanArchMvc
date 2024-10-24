using System;

namespace CleanArchMvc.Infra.Data.Configurations;
public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
