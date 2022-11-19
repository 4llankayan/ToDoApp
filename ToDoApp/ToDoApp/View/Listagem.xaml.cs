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
            InitializeComponent();

            lst_todos.ItemsSource = lista_todos;
        }

        private void ToolbarItem_Clicked_CreateToDo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateToDo());
        }

        protected override void OnAppearing()
        {
            if (lista_todos.Count == 0)
            {
                Task.Run(async () =>
                {
                    List<ToDo> temp = await App.Db.GetAllPendent();

                    foreach (ToDo t in temp)
                    {
                        lista_todos.Add(t);
                    }
                });
            }
        }

        private async void MenuItem_Clicked_Concluir(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;

            ToDo todo_selecionado = (ToDo)disparador.BindingContext;

            string msg = "Concluir o afazer: " + todo_selecionado.Name + " ?";

            bool confirmacao = await DisplayAlert("Tem Certeza?", msg, "Sim", "Não");

            if (confirmacao)
            {
                await App.Db.Update(todo_selecionado);
            }
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {

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