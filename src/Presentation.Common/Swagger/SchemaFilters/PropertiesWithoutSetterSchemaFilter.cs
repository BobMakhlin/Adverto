using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Presentation.Common.Swagger.SchemaFilters
{
    /// <summary>
    /// Excludes properties, that don't have a set-method (setter), from the schema.
    /// </summary>
    public class PropertiesWithoutSetterSchemaFilter : ISchemaFilter
    {
        #region ISchemaFilter

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            IEnumerable<PropertyInfo> allProperties = context.Type.GetProperties();
            IEnumerable<PropertyInfo> propertiesWithoutSetters = allProperties
                .Where(property => !HasPropertySetMethod(property));

            foreach (PropertyInfo propertyWithoutSetter in propertiesWithoutSetters)
            {
                string propertyNameInSchema = schema.Properties.Keys
                    .SingleOrDefault
                    (
                        key => string.Equals(key, propertyWithoutSetter.Name, StringComparison.OrdinalIgnoreCase)
                    );

                if (propertyNameInSchema != null)
                {
                    schema.Properties.Remove(propertyNameInSchema);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if the specified <paramref name="propertyInfo"/> has a set method.
        /// </summary>
        private bool HasPropertySetMethod(PropertyInfo propertyInfo) => propertyInfo.GetSetMethod() != null;

        #endregion
    }
}