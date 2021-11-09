using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTWebApi.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IoTWebApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class SeismicController : ControllerBase
    {

        public string rootPath = @"https://iotwebapi20211108143620.azurewebsites.net";

        // GET: api/<SeismicController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SeismicController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SeismicController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //bruce test
            string a = "";
        }

        // PUT api/<SeismicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SeismicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        // GET: api/<SeismicController>/Login
        [HttpPost("login")]
        public async Task<string> Login()
        {
            var user = new User { UserName = "testUser", Password = "111" };
            var url = @"http://api.bcsims.ca/User/login";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                

                // HTTP GET
                //HttpResponseMessage response = await client.GetAsync("User/login");
                //if (response.IsSuccessStatusCode)
                //{
                //    string tocken = await response.Content.ReadAsStringAsync();
                //}

                //var myObject = (dynamic)new JsonObject();
                //myObject.userName = "some data";
                //myObject.password = "some more data";

                //var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                //var result = client.PostAsync(url, content).Result;

                var data = new { userName = "Bruce.Wang", password = "V7j1xJqO-9" };
                var json = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

           
                var result = await client.PostAsync(url, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    string tocken = await result.Content.ReadAsStringAsync();
                    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                }


            }

            return "test";
        }


        [HttpGet("recent")]
        public async Task<SeismicResponse> Recent()
        {
            var url = @"http://api.bcsims.ca/Earthquakes/recent-earthquakes";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer LoginTokenHere");


                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string tocken = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(tocken);


                    var res = new SeismicResponse();
                    res.Data = new List<SeismicData>();
                    var d = new SeismicData
                    {
                        Id = 1,
                        HasShakeMap = false,
                        Latitude = 44.90,
                        Longitude = 89.09,
                        Region = "test region",
                        Maganitude = 1.2,

                    };
                    res.Data.Add(d);
                    res.HasError = false;

                    //write the json to a file
                    string file = @"C:\Users\BRWANG\projects\IoT\IMB-sent\Read_VIF\Read_VIF\bin\Debug\net5.0\seismicFile-" + DateTime.Now.Ticks.ToString() +".txt";
                    
                    file = DateTime.Now.Ticks.ToString() + ".txt";

                    using (TextWriter writer = System.IO.File.CreateText(file))
                    {
                        var s = JsonConvert.SerializeObject(res);
                        writer.WriteLine(s);
                       
                    }
                    Upload(file);
                    var result = res as SeismicResponse;
                    return result;
                }

                return null;
            }

           
        }


        [HttpPost("shakemap")]
        public async Task<string> Shakemap()
        {
            var user = new User { UserName = "testUser", Password = "111" };
            var url = @"http://api.bcsims.ca/Earthquakes/shakemap-earthquakes?itemsPerPage=100&page=1";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer LoginTokenHere");


                // HTTP GET
                //HttpResponseMessage response = await client.GetAsync(url);
                //if (response.IsSuccessStatusCode)
                //{
                //    string tocken = await response.Content.ReadAsStringAsync();
                //    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                //}

                //var myObject = (dynamic)new JsonObject();
                //myObject.userName = "some data";
                //myObject.password = "some more data";

                //var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                //var result = client.PostAsync(url, content).Result;

                var data = new { from = "2021-03-15T20:28:30.856Z", to = "2021-03-15T20:28:30.856Z", magnitude = 1 };
                var json = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+


                var result = await client.PostAsync(url, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    string tocken = await result.Content.ReadAsStringAsync();
                    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                }

            }

            return "test";
        }


        [HttpPost("earthquakes")]
        public async Task<string> Earthquakes()
        {
            var user = new User { UserName = "testUser", Password = "111" };
            var url = @"http://api.bcsims.ca/Earthquakes/earthquakes";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer LoginTokenHere");


                // HTTP GET
                //HttpResponseMessage response = await client.GetAsync(url);
                //if (response.IsSuccessStatusCode)
                //{
                //    string tocken = await response.Content.ReadAsStringAsync();
                //    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                //}

                //var myObject = (dynamic)new JsonObject();
                //myObject.userName = "some data";
                //myObject.password = "some more data";

                //var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                //var result = client.PostAsync(url, content).Result;

                var data = new { from = "2021-03-15T20:28:30.856Z", to = "2021-03-15T20:28:30.856Z", magnitude = 1 };
                var json = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+


                var result = await client.PostAsync(url, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    string tocken = await result.Content.ReadAsStringAsync();
                    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                }

            }

            return "test";
        }


        [HttpPost("download/{id}")]
        public async Task<string> Download(int id)
        {
            var user = new User { UserName = "testUser", Password = "111" };
            var url = @"http://api.bcsims.ca/Earthquakes/download/"+ id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer LoginTokenHere");


                // HTTP GET
                //HttpResponseMessage response = await client.GetAsync(url);
                //if (response.IsSuccessStatusCode)
                //{
                //    string tocken = await response.Content.ReadAsStringAsync();
                //    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                //}

                //var myObject = (dynamic)new JsonObject();
                //myObject.userName = "some data";
                //myObject.password = "some more data";

                //var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                //var result = client.PostAsync(url, content).Result;

                var data = new
                {
                    allFiles = false,
                    shakemapReportPdfFile = true,
                    shakemapImageFile = true,
                    shakemapPGAMatlabFile = true,
                    allStructuresCsvFile = true,
                    importantCitiesCsvFile = true,
                    shakeMapGridPGAFile = true,
              
                };
                var json = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+


                var result = await client.PostAsync(url, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    string tocken = await result.Content.ReadAsStringAsync();
                    dynamic returnJson = JsonConvert.DeserializeObject(tocken);
                }

            }

            return "test";
        }



        // POST api/<FileController>
        [HttpPost("upload")]
        public void Upload(string file)
        {
            var filePath = file;// @"C:\Users\BRWANG\projects\IoT\IMB-sent\Read_VIF\Read_VIF\bin\Debug\net5.0\seismicFile.txt";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=imageribhedg6dsazu;AccountKey=qf2yzOpOmzZP6sp4PP93lzF7fMJXXW63ff/+PxjuwRlDKt8Oc7sWsEdmB40X+I5N23H69krCluqGoG9n9I4m5Q==;EndpointSuffix=core.windows.net";
            //var fileName = filePath.Substring(filePath.LastIndexOf('\\'));
            var fileName = file;
            Azure.Storage.Blobs.BlobClient blobClient = new Azure.Storage.Blobs.BlobClient(
            connectionString: connectionString,
            blobContainerName: "seismic",
            blobName: fileName
            );

            blobClient.Upload(filePath);

        }
    }
}
