using ToDoList.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppStudia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorksTaskPage : ContentPage
    {
        public WorksTaskPage()
        {
            InitializeComponent();

            BindingContext = new WorkTasksPageViewModel();
        }
    }
}