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

        [ObservableProperty]
        private ObservableCollection<EAfiliadoDTO> listaAfiliados = new ObservableCollection<EAfiliadoDTO>();

        [ObservableProperty]
        private bool loadingEsVisible = false;

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

        public async Task AfiliadosAsociaAsync(int id)
        {
            try
            {
                LoadingEsVisible = true;
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    LoadingEsVisible = false;
                    await Shell.Current.DisplayAlert("Error", "Verifique la conexion a Internet", "Ok");
                    return;
                }

                string url = "https://asociacion-001-site1.ktempurl.com/";
                var responseHttp = await _repository.Get<List<AfiliadoResponse>>(url, $"api/afiliados/listaafi/{id}");

                if (responseHttp.Error)
                {
                    LoadingEsVisible = false;
                    var message = await responseHttp.GetErrorMessageAsync();
                    await Shell.Current.DisplayAlert("Error", message, "Ok");
                    return;
                }

                var responseAfili = responseHttp.Response!;
                LoadingEsVisible = false;

                ListaAfiliados.Clear();

                foreach (var item in responseAfili)
                {
                    ListaAfiliados.Add(new EAfiliadoDTO
                    {
                        IdAfiliado = item.IdAfiliado,
                        NroCI = item.NroCI,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Direccion = item.Direccion,
                        Celular = item.Celular,
                        Estado = item.Estado,
                        EAsociacionId = item.Idasoci,
                        EAsociacionNombre = item.AsociacionNom,
                    });
                }
                
            }
            catch (Exception ex)
            {
                LoadingEsVisible = false;
                await Shell.Current.DisplayAlert("Error", $"Error al sincronizar: {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        private async Task AsociacionSeleccionada()
        {
            if (EAsociacionDto != null)
            {
                await AfiliadosAsociaAsync(EAsociacionDto.Idasoci);
                //await Shell.Current.DisplayAlert("Seleccionó", $"Asociación seleccionada: {EAsociacionDto.Nombre}", "Ok");
            }
        }

        //[RelayCommand]
        //private async Task Buscar()
        //{
        //    var ver = EAsociacionDto.Nombre;
        //    await Shell.Current.DisplayAlert("Selecciono", ver, "Ok");
        //}
    }
}
