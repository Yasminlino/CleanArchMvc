using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver; 

namespace CleanArchMvc.Domain.Entities;

public abstract class Entity
{
    public int Id {get; set;}
}
