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
    public partial class Listagem : ContentPage
    {
        ObservableCollection<ToDo> lista_todos = new ObservableCollection<ToDo>();

        public Listagem()
        {
            Title = "Lista de afazeres";

            InitializeComponent();
            lst_todos.ItemsSource = lista_todos;

            NavigationPage.SetHasNavigationBar(this, true);
        }

        private void ToolbarItem_Clicked_CreateToDo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateToDo());
        }

        protected override void OnAppearing()
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
        }

        private async void MenuItem_Clicked_Concluir(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;

            ToDo todo_selecionado = (ToDo)disparador.BindingContext;

            string msg = "Concluir o afazer: " + todo_selecionado.Name + " ?";

            bool confirmacao = await DisplayAlert("Tem Certeza?", msg, "Sim", "Não");

            if (confirmacao)
            {
                todo_selecionado.Done = true;
                await App.Db.Update(todo_selecionado);

                lista_todos.Remove(todo_selecionado);

                var tab = this.Parent as TabbedPage;
                tab.CurrentPage = tab.Children[1];
            }
        }

        private void MenuItem_Clicked_Editar(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;

            ToDo todo_selecionado = (ToDo)disparador.BindingContext;


            Navigation.PushAsync(new CreateToDo
            {
                BindingContext = todo_selecionado
            });
        }

        private void lst_todos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ToDo todo_selecionado = e.SelectedItem as ToDo;

                lst_todos.SelectedItem = null;

                Navigation.PushAsync(new CreateToDo
                {
                    BindingContext = todo_selecionado
                });
            }
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

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;

            lista_todos.Clear();

            Task.Run(async () =>
            {
                List<ToDo> temp = await App.Db.Search(q);

                foreach (ToDo t in temp)
                {
                    lista_todos.Add(t);
                }
            });
        }
    }
}