using System.ComponentModel.DataAnnotations;

namespace Hallucinogen_API.Contract
{
    public class EntityBase<T>  where T : struct
    {
        [Key] public T Id { get; set; } = default(T);
    }
}