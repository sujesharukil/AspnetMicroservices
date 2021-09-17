using Catalog.Common.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public static class MongoConfigurationMapper
    {
        private static readonly ImmutableTypeClassMapConventionConfluenceEdit
            _immutableTypeClassMapConventionConfluenceEdit = new();


        private static readonly ConventionPack _immutableTypeClassMapConventionPackConfluenceEdit =
            new()
            {
                _immutableTypeClassMapConventionConfluenceEdit
            };

        private static void RegisterClassMap<TClass>(Action<BsonClassMap<TClass>> classMapInitializer)
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(TClass)))
            {
                return;
            }

            BsonClassMap.RegisterClassMap(classMapInitializer);
        }

        private static void RegisterConvention(string name, IConventionPack conventions, Func<Type, bool> filter)
        {
            ConventionRegistry.Remove(name);
            ConventionRegistry.Register(name, conventions, filter);
        }

        public static void MapEntities()
        {
            RegisterConvention(
                typeof(Activity).Name,
                new ConventionPack
                {
                    _immutableTypeClassMapConventionConfluenceEdit,
                   new MemberDefaultValueConvention(
                        typeof(long),
                        0)
                },
                t => t == typeof(Activity));

            RegisterClassMap<Activity>
                (
                    cm =>
                    {
                        cm.AutoMap();
                        cm.SetIdMember(cm.GetMemberMap(c => c.Id)
                            .SetIdGenerator(StringObjectIdGenerator.Instance)
                            .SetSerializer(new StringSerializer(BsonType.ObjectId)));

                    }
                );
        }
    }
}
