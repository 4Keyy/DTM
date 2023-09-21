using System.Reflection;
using System.Runtime.Serialization;
using DTM.Application.Categories.Queries.GetCategories;
using DTM.Application.Common.Interfaces;
using DTM.Application.Common.Models;
using DTM.Application.Notes.Queries.GetNotes;
using DTM.Application.Todos.Queries.GetTodos;
using DTM.Domain.Entities.Models;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config => config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
        [TestCase(typeof(TodoItem), typeof(LookupDto))]

        [TestCase(typeof(Category), typeof(CategoryDto))]
        [TestCase(typeof(Category), typeof(LookupDto))]

        [TestCase(typeof(Note), typeof(NoteDto))]
        [TestCase(typeof(Note), typeof(LookupDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private static object GetInstanceOf(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null ? Activator.CreateInstance(type) : FormatterServices.GetUninitializedObject(type);
        }
    }
}
