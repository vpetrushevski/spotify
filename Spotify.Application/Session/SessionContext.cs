using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Session
{
    public class SessionContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Method used for instantiating httpContextAccessor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;

        #region PlatformConfiguration
        //public static async Task<List<PlatformConfiguration>> GetPlatformConfiguration(int? value, string program_code, BrosuranceContext db)
        //{
        //    const string sessionKey = "PlatformConfiguration";

        //    List<PlatformConfiguration> platformConfigurations = _httpContextAccessor.HttpContext.Session.GetComplexData<List<PlatformConfiguration>>(sessionKey);
        //    if (platformConfigurations == null)
        //    {
        //        platformConfigurations = await db.PlatformConfigurations.ToListAsync();

        //        _httpContextAccessor.HttpContext.Session.SetComplexData(sessionKey, platformConfigurations);
        //    }

        //    return platformConfigurations.Where(x => x.ProgramCode == program_code || x.Value == value).ToList();
        //}

        #endregion
    }
}
