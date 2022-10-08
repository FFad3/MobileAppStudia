using ToDoList.Core.Helpers;
using ToDoList.Database;
using Xamarin.Forms;

namespace MobileAppStudia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var database = new ToDoListDbContext();
            database.Database.EnsureDeleted();
            database.Database.EnsureCreated();
            DataBaseLocator.Database = database;
            MainPage = new WorksTaskPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}