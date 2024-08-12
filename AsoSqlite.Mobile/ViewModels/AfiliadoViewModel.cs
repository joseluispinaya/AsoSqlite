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
    public partial class AfiliadoViewModel : ObservableObject
    {
        private readonly IRepository _repository;
        private readonly EAfiliadoDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<EAfiliadoDTO> listaAfiliados = new ObservableCollection<EAfiliadoDTO>();

        public AfiliadoViewModel(EAfiliadoDbContext context, IRepository repository)
        {
            _dbContext = context;
            _repository = repository;

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                SincronizarAsocia();
            }

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<EntidadMensajeria<EAfiliadoDTO>>(this, (r, m) =>
            {
                EAfiliadoMensajeRecibido(m.Value);
            });
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
            //List<ResponseAsociacion> responseAso = new List<ResponseAsociacion>();
            //responseAso = responseHttp.Response!;

            foreach (var item in responseAso)
            {
                // Verificar si el Idasoci ya existe en la base de datos local
                var exists = await _dbContext.EAsociaciones.AnyAsync(a => a.Idasoci == item.Idasoci);
                if (!exists)
                {
                    var tbEAsociacion = new EAsociacion
                    {
                        Idasoci = item.Idasoci,
                        Nombre = item.Nombre,
                        Direccion = item.Direccion,
                        Telefono = item.Telefono,
                        Correo = item.Correo,
                        Activo = item.Activo,
                    };
                    _dbContext.EAsociaciones.Add(tbEAsociacion);
                }
                    
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.EAfiliados.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
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
                        EAsociacionId = item.EAsociacionId,
                    });
                }
            }
        }

        private void EAfiliadoMensajeRecibido(EntidadMensaje<EAfiliadoDTO> eafiliadoMensaje)
        {
            var eafiliadoDto = eafiliadoMensaje.EntidadDto!;
            if (eafiliadoMensaje.EsCrear)
            {
                ListaAfiliados.Add(eafiliadoDto);
            }
            else
            {
                var encontrado = ListaAfiliados.First(e => e.IdAfiliado == eafiliadoDto.IdAfiliado);

                encontrado.NroCI = eafiliadoDto.NroCI;
                encontrado.Nombres = eafiliadoDto.Nombres;
                encontrado.Apellidos = eafiliadoDto.Apellidos;
                encontrado.Direccion = eafiliadoDto.Direccion;
                encontrado.Celular = eafiliadoDto.Celular;
                encontrado.Estado = eafiliadoDto.Estado;
                encontrado.EAsociacionId = eafiliadoDto.EAsociacionId;

            }
        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(AddAfiliadoView)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(EAfiliadoDTO eAfiliadoDto)
        {
            var uri = $"{nameof(AddAfiliadoView)}?id={eAfiliadoDto.IdAfiliado}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(EAfiliadoDTO eAfiliadoDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Afiliado?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.EAfiliados
                    .FirstAsync(e => e.IdAfiliado == eAfiliadoDto.IdAfiliado);

                _dbContext.EAfiliados.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaAfiliados.Remove(eAfiliadoDto);
            }
        }
    }
}
