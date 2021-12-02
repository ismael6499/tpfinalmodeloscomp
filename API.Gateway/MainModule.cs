using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using API.Gateway;
using API.Gateway.Mappers;
using API.Gateway.Modelos;
using Nancy;
using Nancy.Extensions;
using Nancy.Json;
using Newtonsoft.Json;

namespace ApiGateway
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            string urlCinta = Program.Configuration.GetSection("Cinta").GetSection("url").Value;
            string urlBrazo = Program.Configuration.GetSection("Brazo").GetSection("url").Value;
            string urlPrensa = Program.Configuration.GetSection("Prensa").GetSection("url").Value;
            string urlSensores = Program.Configuration.GetSection("Sensores").GetSection("url").Value;

            Post("/v1/ponerbulto", _ =>
            {
                var bultoJson = Context.Request.Body.AsString();

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.BaseAddress = new Uri(urlCinta);
                        StringContent stringContent = new StringContent(bultoJson, Encoding.UTF8, "application/json");
                        var responseTask = httpClient.PostAsync("/api/ponerbulto", stringContent);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var readResult = readTask.Result;
                        return readResult;
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }
            });

            Get("/v1/encender/{url}", x =>
            {
                string url = x.url;
                if (url.Contains(";") && !url.Contains("https://"))
                {
                    url = "https://" + url.Replace(";", "");
                }else if (!url.Contains("http://"))
                {
                    url = "http://" + url.Replace("http://","");
                }
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.BaseAddress = new Uri(url);
                        var responseTask = httpClient.GetAsync("/api/encender");
                        responseTask.Wait();
                        var result = responseTask.Result;
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var readResult = readTask.Result;
                        return readResult;
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }
            });
            
            Get("/v1/apagar/{url}", x =>
            {
                string url = x.url;
                if (url.Contains(";") && !url.Contains("https://"))
                {
                    url = "https://" + url.Replace(";", "");
                }else if (!url.Contains("http://"))
                {
                    url = "http://" + url.Replace("http://","");
                }
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.BaseAddress = new Uri(url);
                        var responseTask = httpClient.GetAsync("/api/apagar");
                        responseTask.Wait();
                        var result = responseTask.Result;
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var readResult = readTask.Result;
                        return readResult;
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }
            });


            BrazoGateway();
            CintaGateway();
            PilaDeBultosGateway();
            PrensaGateway();
            SensorActivoGateway();
            SensorPasivoGateway();
        }

        private void BrazoGateway()
        {
            Get("/v1/brazo/getall", x =>
            {
                var brazoMapper = new BrazoMapper();
                var lista = brazoMapper.Listar();
                return Response.AsJson(lista);
            });

            Get("/v1/brazo/get/{id}", x =>
            {
                var brazoMapper = new BrazoMapper();
                int id = x.id;
                var entidad = brazoMapper.Get(id);
                return Response.AsJson(entidad);
            });
            Post("/v1/brazo/create", x =>
            {
                var brazoMapper = new BrazoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Brazo>(json);
                brazoMapper.Agregar(entidad);
                return "Creado";
            });

            Post("/v1/brazo/edit", x =>
            {
                var brazoMapper = new BrazoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Brazo>(json);
                brazoMapper.Actualizar(entidad);
                return "Creado";
            });

            Post("/v1/brazo/delete", x =>
            {
                var brazoMapper = new BrazoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Brazo>(json);
                brazoMapper.Eliminar(entidad);
                return "Creado";
            });
        }

        private void CintaGateway()
        {
            Get("/v1/cinta/getall", x =>
            {
                var cintaMapper = new CintaMapper();
                var lista = cintaMapper.Listar();
                return Response.AsJson(lista);
            });

            Get("/v1/cinta/get/{id}", x =>
            {
                var cintaMapper = new CintaMapper();
                int id = x.id;
                var entidad = cintaMapper.Get(id);
                return Response.AsJson(entidad);
            });
            Post("/v1/cinta/create", x =>
            {
                var cintaMapper = new CintaMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Cinta>(json);
                cintaMapper.Agregar(entidad);
                return "Creado";
            });

            Post("/v1/cinta/edit", x =>
            {
                var cintaMapper = new CintaMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Cinta>(json);
                cintaMapper.Actualizar(entidad);
                return "Creado";
            });

            Post("/v1/cinta/delete", x =>
            {
                var cintaMapper = new CintaMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Cinta>(json);
                cintaMapper.Eliminar(entidad);
                return "Creado";
            });
        }

        private void PilaDeBultosGateway()
        {
            Get("/v1/pilaDeBultos/getall", x =>
            {
                var pilaDeBultosMapper = new PilaDeBultosMapper();
                var lista = pilaDeBultosMapper.Listar();
                return Response.AsJson(lista);
            });

            Get("/v1/pilaDeBultos/get/{id}", x =>
            {
                var pilaDeBultosMapper = new PilaDeBultosMapper();
                int id = x.id;
                var entidad = pilaDeBultosMapper.Get(id);
                return Response.AsJson(entidad);
            });
            Post("/v1/pilaDeBultos/create", x =>
            {
                var pilaDeBultosMapper = new PilaDeBultosMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<PilaDeBultos>(json);
                pilaDeBultosMapper.Agregar(entidad);
                return "Creado";
            });

            Post("/v1/pilaDeBultos/edit", x =>
            {
                var pilaDeBultosMapper = new PilaDeBultosMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<PilaDeBultos>(json);
                pilaDeBultosMapper.Actualizar(entidad);
                return "Creado";
            });

            Post("/v1/pilaDeBultos/delete", x =>
            {
                var pilaDeBultosMapper = new PilaDeBultosMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<PilaDeBultos>(json);
                pilaDeBultosMapper.Eliminar(entidad);
                return "Creado";
            });
        }

        private void PrensaGateway()
        {
            Get("/v1/prensa/getall", x =>
            {
                var prensaMapper = new PrensaMapper();
                var lista = prensaMapper.Listar();
                return Response.AsJson(lista);
            });

            Get("/v1/prensa/get/{id}", x =>
            {
                var prensaMapper = new PrensaMapper();
                int id = x.id;
                var entidad = prensaMapper.Get(id);
                return Response.AsJson(entidad);
            });
            Post("/v1/prensa/create", x =>
            {
                var prensaMapper = new PrensaMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Prensa>(json);
                prensaMapper.Agregar(entidad);
                return "Creado";
            });

            Post("/v1/prensa/edit", x =>
            {
                var prensaMapper = new PrensaMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Prensa>(json);
                prensaMapper.Actualizar(entidad);
                return "Creado";
            });

            Post("/v1/prensa/delete", x =>
            {
                var prensaMapper = new PrensaMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<Prensa>(json);
                prensaMapper.Eliminar(entidad);
                return "Creado";
            });
        }

        private void SensorActivoGateway()
        {
            Get("/v1/sensorActivo/getall", x =>
            {
                var sensorActivoMapper = new SensorActivoMapper();
                var lista = sensorActivoMapper.Listar();
                return Response.AsJson(lista);
            });

            Get("/v1/sensorActivo/get/{id}", x =>
            {
                var sensorActivoMapper = new SensorActivoMapper();
                int id = x.id;
                var entidad = sensorActivoMapper.Get(id);
                return Response.AsJson(entidad);
            });
            Post("/v1/sensorActivo/create", x =>
            {
                var sensorActivoMapper = new SensorActivoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<SensorActivo>(json);
                sensorActivoMapper.Agregar(entidad);
                return "Creado";
            });

            Post("/v1/sensorActivo/edit", x =>
            {
                var sensorActivoMapper = new SensorActivoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<SensorActivo>(json);
                sensorActivoMapper.Actualizar(entidad);
                return "Creado";
            });

            Post("/v1/sensorActivo/delete", x =>
            {
                var sensorActivoMapper = new SensorActivoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<SensorActivo>(json);
                sensorActivoMapper.Eliminar(entidad);
                return "Creado";
            });
        }

        private void SensorPasivoGateway()
        {
            Get("/v1/sensorPasivo/getall", x =>
            {
                var sensorPasivoMapper = new SensorPasivoMapper();
                var lista = sensorPasivoMapper.Listar();
                return Response.AsJson(lista);
            });

            Get("/v1/sensorPasivo/get/{id}", x =>
            {
                var sensorPasivoMapper = new SensorPasivoMapper();
                int id = x.id;
                var entidad = sensorPasivoMapper.Get(id);
                return Response.AsJson(entidad);
            });
            Post("/v1/sensorPasivo/create", x =>
            {
                var sensorPasivoMapper = new SensorPasivoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<SensorPasivo>(json);
                sensorPasivoMapper.Agregar(entidad);
                return "Creado";
            });

            Post("/v1/sensorPasivo/edit", x =>
            {
                var sensorPasivoMapper = new SensorPasivoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<SensorPasivo>(json);
                sensorPasivoMapper.Actualizar(entidad);
                return "Creado";
            });

            Post("/v1/sensorPasivo/delete", x =>
            {
                var sensorPasivoMapper = new SensorPasivoMapper();
                string json = Context.Request.Body.AsString();
                var entidad = JsonConvert.DeserializeObject<SensorPasivo>(json);
                sensorPasivoMapper.Eliminar(entidad);
                return "Creado";
            });
        }
    }
}