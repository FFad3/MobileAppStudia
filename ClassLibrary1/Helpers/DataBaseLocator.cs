using ToDoList.Database;

namespace ToDoList.Core.Helpers
{
    public static class DataBaseLocator
    {
        public static ToDoListDbContext Database { get; set; }
    }
}