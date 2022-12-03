using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            Title = "Seus afazeres";

            NavigationPage navigationPage = new NavigationPage(new Concluidos());
            navigationPage.Title = "Itens concluídos";
            Children.Add(new Listagem());
            Children.Add(navigationPage);
        }
    }
}