using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AsoSqlite.Mobile.DataAccess;
using AsoSqlite.Mobile.DTOs;
using AsoSqlite.Mobile.Utilidades;
using AsoSqlite.Mobile.Modelos;
using System.Collections.ObjectModel;
using AsoSqlite.Mobile.Repositories;
using AsoSqlite.Mobile.Views;
using AsoSqlite.Mobile.Models;

namespace AsoSqlite.Mobile.ViewModels
{
    public partial class AsociacionesViewModel : ObservableObject
    {
        private readonly IRepository _repository;

        [ObservableProperty]
        private ObservableCollection<EAsociacionDTO> listaAsociacion = new ObservableCollection<EAsociacionDTO>();

        [ObservableProperty]
        private EAsociacionDTO eAsociacionDto = new EAsociacionDTO();

        public AsociacionesViewModel(IRepository repository)
        {
            _repository = repository;
            SincronizarAsocia();
        }

        private async void SincronizarAsocia()
        {
            string url = "https://asociacion-001-site1.ktempurl.com/";
            var responseHttp = await _repository.Get<List<ResponseAsociacion>>(url, "api/afiliados/combo");

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            var responseAso = responseHttp.Response!;
            foreach (var item in responseAso)
            {
                ListaAsociacion.Add(new EAsociacionDTO
                {
                    Idasoci = item.Idasoci,
                    Nombre = item.Nombre,
                    Direccion = item.Direccion,
                    Telefono = item.Telefono,
                    Correo = item.Correo,
                    Activo = item.Activo,
                });
            }
        }

        [RelayCommand]
        private async Task Buscar()
        {
            var ver = EAsociacionDto.Nombre;
            await Shell.Current.DisplayAlert("Selecciono", ver, "Ok");
        }
    }
}
