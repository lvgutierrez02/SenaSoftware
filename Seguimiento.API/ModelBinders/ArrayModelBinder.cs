using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Seguimiento.API.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext) //Estamos creando un modelo de enlace para el tipo IEnumerable
        {
             if(!bindingContext.ModelMetadata.IsEnumerableType) //tenemos que comprobar si nuestro parámetro es del mismo tipo
            {
                 bindingContext.Result = ModelBindingResult.Failed();
                 return Task.CompletedTask;
             }
                var providedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString(); //extraemos el valor (una cadena de GUIDs separada por comas) Como es de tipo cadena, sólo comprobamos
                                                                                                                
            if (string.IsNullOrEmpty(providedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);  //sólo comprobamos si es nulo o está vacío. Si lo es, devolvemos null como resultado
                return Task.CompletedTask;
            }
            //con la ayuda de reflection, almacenamos el tipo del que consta el IEnumerable GUID
            var genericType =
                bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
                var converter = TypeDescriptor.GetConverter(genericType); //creamos un convertidor a un tipo GUID
            var objectArray = providedValue.Split(new[] { "," }, //array de tipo objeto (objectArray) que consiste de todos los valores GUID que enviamos a la API
                StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            //creamos un array de tipos GUID(guidArray), copiamos todos los valores del objectArray al
            //guidArray, y asignarlo al bindingContext.
            var guidArray = Array.CreateInstance(genericType, objectArray.Length); 
                objectArray.CopyTo(guidArray, 0);
                bindingContext.Model = guidArray;
                bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
                return Task.CompletedTask;
        }
    }
}
