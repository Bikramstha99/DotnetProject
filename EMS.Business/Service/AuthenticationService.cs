using EMS.Entities.Dtos;
using EMS.Repository.Interface;
using EMSBusiness.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
        private readonly IAuthenticationRepository _authRepository;

        public AuthenticationService(IAuthenticationRepository authRepository, IOptions<AppSettings> appSettings, IHttpContextAccessor contextAccessor)
        {
            _appSettings = appSettings.Value;
            _contextAccessor = contextAccessor;
            _authRepository = authRepository;
        }
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
        {
            return await _authRepository.Authenticate(model);
        }
    }
}
