using EMS.Entities.Dtos;
using EMSBusiness.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementBusiness.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _contextAccessor;
        //private IMemoryCacheService _cache;
        //private readonly ILoggerManager _logger;
        //private readonly IAuthenticationRepository _authRepository;

        public AuthenticationService(IOptions<AppSettings> appSettings, ILoggerManager logger, IHttpContextAccessor contextAccessor,)
        {
            _appSettings = appSettings.Value;
            _contextAccessor = contextAccessor;
            _cache = cache;
            _logger = logger;
            _authRepository = authRepository;
        }
        public Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
        {
            return await _authRepository.Authenticate(model);
        }
    }
}
