using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Linq;
using System.Reflection;

namespace Catalog.Data
{
    public sealed class ImmutableTypeClassMapConventionConfluenceEdit : ConventionBase, IClassMapConvention
    {
        /// <inheritdoc />
        public void Apply(BsonClassMap classMap)
        {
            var typeInfo = classMap.ClassType.GetTypeInfo();
            if (typeInfo.IsAbstract)
            {
                return;
            }
            if (typeInfo.GetConstructor(Type.EmptyTypes) != null)
            {
                return;
            }
            var properties = typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            var allProperties = typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Any(p => p.CanWrite))
            {
                return; // a type that has any writable properties is not immutable
            }
            var anyConstructorsWereMapped = false;
            foreach (var ctor in typeInfo.GetConstructors())
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length != allProperties.Length)
                {
                    continue; // only consider constructors that have sufficient parameters to initialize all properties
                }
                var matches = parameters
                    .GroupJoin(allProperties,
                        parameter => parameter.Name + parameter.ParameterType.Name,
                        property => property.Name + property.PropertyType.Name,
                        (parameter, props) => new { Parameter = parameter, Properties = props },
                        StringComparer.OrdinalIgnoreCase);
                if (matches.Any(m => m.Properties.Count() != 1))
                {
                    continue;
                }
                classMap.MapConstructor(ctor);
                anyConstructorsWereMapped = true;
            }

            if (!anyConstructorsWereMapped)
            {
                return;
            }

            // if any constructors were mapped by this convention then map all the properties also
            foreach (var property in properties)
            {
                var memberMap = classMap.MapMember(property);
                if (!classMap.IsAnonymous)
                {
                    continue;
                }

                memberMap.SetDefaultValue(
                    memberMap.DefaultValue);
            }
        }
    }
}
