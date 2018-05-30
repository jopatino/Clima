using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clima.ViewModel
{
    public class WeatherViewModelPage:NotificableViewModel
    {
        #region Atributos
        private string ubicacion;
        private string pais;
        private string resultTerm;
        private string region;
        private string ultimaActualizacion;
        private string clima;
        private string temperatura;
        private ImageSource imagen;

        #endregion

        #region Propiedades
        public ImageSource Imagen
        {
            get { return imagen; }
            set { SetValue(ref imagen, value); }
        }


        public string Temperatura
        {
            get { return temperatura; }
            set { SetValue(ref temperatura, value); }
        }


        public string Clima
        {
            get { return clima; }
            set { SetValue(ref clima, value); }
        }


        public string UltimaActualizacion
        {
            get { return ultimaActualizacion; }
            set { SetValue(ref ultimaActualizacion, value); }
        }


        public string Region
        {
            get { return region; }
            set { SetValue(ref region, value); }
        }


        public string ResultTerm
        {
            get { return resultTerm; }
            set { SetValue(ref resultTerm, value); }
        }


        public string Pais
        {
            get { return pais; }
            set { SetValue(ref pais, value); }
        }


        public string Ubicacion
        {
            get { return ubicacion; }
            set { SetValue(ref ubicacion, value); }
        }
        #endregion

        #region Comandos
        public  ICommand BuscarCommand
        {
            get
            {
                return new RelayCommand(Buscar);
            }
        }

        
        #endregion

        #region Constructores
        public WeatherViewModelPage()
        {

        }

        #endregion

        #region Métodos
        private async void Buscar()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(ObtenerUrl());
            var response = await cliente.GetAsync(cliente.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var weatherModel = Weather.FromJson(jsonResult);
            SetValues(weatherModel);
        }

       

        private string ObtenerUrl()
        {
            string serviceUrl = $"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22{ResultTerm}%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            return serviceUrl;
        }
        #endregion


    }
}
