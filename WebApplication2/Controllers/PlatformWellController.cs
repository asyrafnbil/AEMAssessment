using AEMAssessment.Models;
using AEMAssessment.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Platform = AEMAssessment.Models.Platform;

namespace AEMAssessment.Controllers
{
    public class PlatformWellController : Controller
    {
        protected readonly IContextBuilder ContextBuilder;
        private readonly LocalDbContext context;
        public static string baseUrl = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual";

        public PlatformWellController(LocalDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var platformWell = await GetPlatformWellActual();
            return View(platformWell);
        }

        [HttpGet]
        //public async Task<List<Platform>> GetPlatformWell()
        //{
        //    var platformList = await context.Platform.ToListAsync();
        //    return platformList;
        //}

        [HttpGet]
        public async Task<List<PlatformWell>> GetPlatformWellActual()
        {
            List<Platform> listPlatform = new List<Platform>();
            List<Well> listWell = new List<Well>();
            List<PlatformWell> res = new List<PlatformWell>();

            var accessToken = HttpContext.Session.GetString("JWToken");
            accessToken = accessToken?.Replace('"'.ToString(), string.Empty);
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            try
            {
                res = JsonConvert.DeserializeObject<List<PlatformWell>>(jsonStr);
            }
            catch (Exception)
            {
                foreach (var item in res)
                {
                    Platform platform = new Platform();
                }
                return res;
            }


            //if (res.Count > 0)
            //{
            //    await context.PlatformWell.AddRange(res);
            //}


            var platformList = GetPlatformData(res);
            var wellList = GetWellData(res);
            var platformListDb = await GetPlatformDb();
            var wellListDb = await GetWellDb();

            if (platformListDb.Count <= 0 && platformList.Count > 0)
            {
                context.Platform.AddRange(platformList);
                await context.SaveChangesAsync();
            }
            else
            {
                foreach (var item in platformListDb.Where(first => platformList.Any(second => second.Id == first.Id)))
                {
                    item.UpdatedAt = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }

            if (wellListDb.Count <= 0 && wellList.Count > 0)
            {
                context.Well.AddRange(wellList);
                await context.SaveChangesAsync();
            }
            else
            {
                foreach (var item in wellListDb.Where(first => wellList.Any(second => second.Id == first.Id)))
                {
                    item.UpdatedAt = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }

            return res;
        }

        public List<Platform> GetPlatformData(List<PlatformWell> platformWellList)
        {
            var result = new List<Platform>();

            foreach (var item in platformWellList)
            {
                Platform platform = new Platform();
                platform.Id = item.Id;
                platform.UniqueName = item.UniqueName;
                platform.Latitude = item.Latitude;
                platform.Longitude = item.Longitude;
                platform.CreatedAt = item.CreatedAt;
                platform.UpdatedAt = item.UpdatedAt;

                result.Add(platform);
            }
            return result;
        }

        public List<Well> GetWellData(List<PlatformWell> platformWellList)
        {
            var result = new List<Well>();

            foreach (var item in platformWellList)
            {
                List<Well> wellList = item.Well;

                foreach (var well in wellList)
                {
                    Well wellObj = new Well();
                    wellObj.Id = well.Id;
                    wellObj.PlatformId = well.PlatformId;
                    wellObj.UniqueName = well.UniqueName;
                    wellObj.Latitude = well.Latitude;
                    wellObj.Longitude = well.Longitude;
                    wellObj.CreatedAt = well.CreatedAt;
                    wellObj.UpdatedAt = well.UpdatedAt;

                    result.Add(wellObj);
                }
            }
            return result;
        }

        [HttpGet]
        public async Task<List<Platform>> GetPlatformDb()
        {
            var platformListDb = await context.Platform.ToListAsync();

            return platformListDb;
        }

        [HttpGet]
        public async Task<List<Well>> GetWellDb()
        {
            var wellListDb = await context.Well.ToListAsync();

            return wellListDb;
        }
    }
}