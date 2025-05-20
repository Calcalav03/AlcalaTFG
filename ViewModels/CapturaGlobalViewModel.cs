using AlcalaTFG.models;
using AlcalaTFG.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class CapturaGlobalViewModel:ObservableObject
    {



        ////METODO PARA OBTENER LAS FORMAS
        //[RelayCommand]
        //public async Task RequestFormas()
        //{
        //    RequestModel requestModel = new RequestModel
        //    {
        //        Method = "GET",
        //        Route = "http://erciapps.sytes.net:11002/vapers/mostrarFormas",
        //        Data = string.Empty
        //    };


        //    ResponseModel response = await APIService.ExecuteRequest(requestModel);

        //    if (response.Success.Equals(0))
        //    {
        //        try
        //        {
        //            ListaFormas = JsonConvert.DeserializeObject<ObservableCollection<FormaInfo>>(response.Data.ToString());
        //        }
        //        catch (Exception ex) { }
        //    }
        //}


    }
}
