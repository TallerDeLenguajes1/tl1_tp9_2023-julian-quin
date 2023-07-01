using llamadaMonedas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {
        // La misma tiene que mostrar por pantalla todas los precios disponibles
        // luego elija una en particular y muestre sus características
        Monedas Money = new Monedas();
        Money = GetMonedas();
        Precios(Money);
        Console.WriteLine("\nCaracteristica Moneda aletorea\n");
        CaracteristicasMoneda(Money);

    }
    
    private static Monedas GetMonedas()
    {
        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        Monedas monedas = null;
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            monedas = JsonSerializer.Deserialize<Monedas>(responseBody);

                        }

                    }

                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
        }

        return monedas;
    }
    private static void Precios(Monedas money)
    {
        Console.WriteLine("Monedas Precios\n");
        Console.WriteLine("EUR "+money.Bpi.EUR.Tasa);
        Console.WriteLine("GBP "+money.Bpi.GBP.Tasa);
        Console.WriteLine("USD "+money.Bpi.USD.Tasa);
    }

    private static void CaracteristicasMoneda (Monedas money)
    {
        Random Aleatoreo = new Random();
        int numero = Aleatoreo.Next(0,3);
        string [] Criptos = {"EUR","GBP","USD"};
        var Datos = new List<string>();

        switch (Criptos[numero])
        {
            case "EUR":
                mostrado(money.Bpi.EUR.Codigo,money.Bpi.EUR.Simbolo,money.Bpi.EUR.Tasa,money.Bpi.EUR.Description); 
            break;
            case "GBP":
                mostrado(money.Bpi.GBP.Codigo,money.Bpi.GBP.Simbolo,money.Bpi.GBP.Tasa,money.Bpi.GBP.Description); 
            break;
            case "USD":
                mostrado(money.Bpi.USD.Codigo,money.Bpi.USD.Simbolo,money.Bpi.USD.Tasa,money.Bpi.USD.Description); 
            break;
       
        }

        void mostrado(string codigo, string simbolo , string tasa, string descripcion)
        {
            Console.WriteLine("codigo => "+codigo);
            Console.WriteLine("tasa => "+tasa);
            Console.WriteLine($"simbolo => "+simbolo);
            Console.WriteLine("descripcion => "+descripcion);
        }


    }
   
}

