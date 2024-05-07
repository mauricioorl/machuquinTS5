using rtoasa5.Models;

namespace rtoasa5.Views;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}
    private Persona selectedPersona;

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {

        if (selectedPersona == null)
        {
            Persona newPersona = new Persona
            {
                Name = txtPersona.Text,

            };
            lblStatus.Text = "";
            App.PersonRepo.AddNewPerson(newPersona);
            lblStatus.Text = App.PersonRepo.statusMessage;
            // Actualizar la lista de personas después de la eliminación
            
        }
        else
        {
            selectedPersona.Name = txtPersona.Text;
            lblStatus.Text = "";
            App.PersonRepo.AddNewPerson(selectedPersona);
            lblStatus.Text = App.PersonRepo.statusMessage;
            selectedPersona = null; // Resetea el contacto seleccionado después de la actualización
                                    // Actualizar la lista de personas después de la eliminación
            

        }
        
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people=App.PersonRepo.GetAllPeople();
        Listapersonas.ItemsSource = people;
    }

    private async void btnEditar_Clicked(object sender, EventArgs e)
    {
        selectedPersona = (sender as Button).CommandParameter as Persona;
        txtPersona.Text = selectedPersona.Name;
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        // Obtener la persona seleccionada en el CollectionView
        selectedPersona = (sender as Button).CommandParameter as Persona;
        txtPersona.Text = selectedPersona.Name;

        if (selectedPersona != null)
        {
                       
                int personIdToDelete = selectedPersona.Id;
                App.PersonRepo.DeletePerson(personIdToDelete); 
                lblStatus.Text = App.PersonRepo.statusMessage;
                        

            
            
        }
        else
        {
            // Manejar el caso en que no se ha seleccionado ninguna persona
            await MostrarMensajeError("Debes seleccionar una persona de la lista");
        }
    }

    private async Task MostrarMensajeError(string mensaje)
    {
        await App.Current.MainPage.DisplayAlert("Error", mensaje, "Aceptar");
    }
}