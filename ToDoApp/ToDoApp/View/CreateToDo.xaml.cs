using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateToDo : ContentPage
    {
        public CreateToDo()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                ToDo todo_selecionado = new ToDo();

                if (BindingContext != null) 
                    todo_selecionado = BindingContext as ToDo;

                ToDo t = new ToDo()
                {
                    Id = todo_selecionado.Id,
                    Name = txt_name.Text,
                    Description = txt_description.Text,
                };

                if (t.Id == 0)
                {
                    await App.Db.Insert(t);
                    await DisplayAlert("Deu certo!", "Afazer inserido", "Ok");
                }
                else
                {
                    await App.Db.Update(t);
                    await DisplayAlert("Deu certo!", "Afazer atualizado", "Ok");
                }

                await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }

        }
    }
}