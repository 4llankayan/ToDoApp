using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Concluidos : ContentPage
    {
        ObservableCollection<ToDo> lista_todos = new ObservableCollection<ToDo>();

        public Concluidos()
        {
            InitializeComponent();

            lst_todos.ItemsSource = lista_todos;

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            lista_todos.Clear();

            Task.Run(async () =>
            {
                List<ToDo> temp = await App.Db.GetAllDone();

                foreach (ToDo t in temp)
                {
                    lista_todos.Add(t);
                }
            });
        }

        private void lst_todos_Refreshing(object sender, EventArgs e)
        {
            lista_todos.Clear();

            Task.Run(async () =>
            {
                List<ToDo> temp = await App.Db.GetAllPendent();

                foreach (ToDo t in temp)
                {
                    lista_todos.Add(t);
                }
            });

            ref_carregando.IsRefreshing = false;
        }

        private void lst_todos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ToDo todo_selecionado = e.SelectedItem as ToDo;

            Navigation.PushAsync(new CreateToDo
            {
                BindingContext = todo_selecionado
            });
        }
    }
}