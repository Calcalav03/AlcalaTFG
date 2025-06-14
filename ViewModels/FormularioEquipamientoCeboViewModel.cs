﻿using AlcalaTFG.models;
using AlcalaTFG.Models;
using AlcalaTFG.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class FormularioEquipamientoCeboViewModel:ObservableObject
    {
        [ObservableProperty]
        public string tipoCebo;

        [ObservableProperty]
        public string descripcion;

        
        [RelayCommand]
        public async Task CrearCebo()
        {
            string message = "Ocurrió un error al crear el cebo: ";
            string bien = "Cebo creado correctamente!";

            try
            {
                if (string.IsNullOrWhiteSpace(TipoCebo) || string.IsNullOrWhiteSpace(Descripcion))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El tipo de cebo y la descripción son obligatorios", "Aceptar");
                    return;
                }

                var ceboDto = new CeboDTO(
                    TipoCebo,
                    Descripcion
                );

               
                var request = new RequestModel
                {
                    Data = ceboDto,
                    Method = "POST",
                    Route = "http://localhost:8089/jpa/crearCebo"
                };

               
                ResponseModel response = await APIService.ExecuteRequestJPA(request);

                
                await App.Current.MainPage.DisplayAlert("Mensaje", bien, "Aceptar");



                LimpiarFormularioCebo();

               
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", message + ex.Message, "Aceptar");
                Debug.WriteLine(message + ex.Message);
            }
        }

        [RelayCommand]
        public void LimpiarFormularioCebo()
        {
            TipoCebo= string.Empty;
            Descripcion = string.Empty;
        }

        [ObservableProperty]
        public string tipoEquip;

        [ObservableProperty]
        public string marca;


        [ObservableProperty]
        public string modelo;

        
        [RelayCommand]
        public async Task CrearEquipamiento()
        {
            string message = "Ocurrió un error al crear el Equipamiento: ";
            string bien = "Equipamiento creado correctamente!";

            try
            {
                if (string.IsNullOrWhiteSpace(TipoEquip) || string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(Modelo))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El tipo de equipamiento , la marca y el modelo son obligatorios", "Aceptar");
                    return;
                }

                var equipamientoDto = new EquipamientoDTO(
                    TipoEquip,
                    Marca,
                    Modelo
                );

                
                var request = new RequestModel
                {
                    Data = equipamientoDto,
                    Method = "POST",
                    Route = "http://localhost:8089/jpa/crearEquipamiento"
                };

                
                ResponseModel response = await APIService.ExecuteRequestJPA(request);

                
                await App.Current.MainPage.DisplayAlert("Mensaje", bien, "Aceptar");



                LimpiarFormularioEquip();


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", message + ex.Message, "Aceptar");
                Debug.WriteLine(message + ex.Message);
            }
        }

        [RelayCommand]
        public void LimpiarFormularioEquip()
        {
            TipoEquip = string.Empty;
            Marca = string.Empty;
            Modelo = string.Empty;
            
        }

        [RelayCommand]
        public async Task AMenu()
        {
            await Shell.Current.GoToAsync("//MenuPrincipal");
        }


    }
}
