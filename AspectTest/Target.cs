using System.Text;
using AspectTest.Aspects;
using AspectTest.Attributes;
using AspectTest.Entities;

namespace AspectTest
{
    [TargetType(typeof(Vehicle))]
    [InsertProperty]
    public partial class Target
    {
        public StringBuilder ListElements()
        {
            var sb = new StringBuilder();

            _elements.Add(new Vehicle(Guid.NewGuid(), "Ford Fiesta"));

            foreach (var item in _elements)
            {
                sb.Append($"Id: {item.Id}, Name: {item.Name}");
            }

            return sb;
        }
    }
}
