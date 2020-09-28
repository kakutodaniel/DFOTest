using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;
using DFO.Test.Application.Context;
using DFO.Test.Application.Services.Interfaces;
using DFO.Test.Application.Validators;
using DFO.Test.Application.Contracts.User;
using System;

namespace DFO.Test.Application.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly ApiContext _context;
        private readonly UserRequestValidator _validator;
        private readonly IMapper _mapper;

        public UserService(
            ApiContext context,
            UserRequestValidator validator,
            IMapper mapper) : base(context)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserResponse>> Add(UserRequest userRequest)
        {
            var response = new BaseResponse<UserResponse>();
            var results = _validator.Validate(userRequest);

            if (!results.IsValid)
            {
                results.Errors.ToList().ForEach(x => 
                { 
                    response.AddErrors(x.ErrorMessage, HttpStatusCode.BadRequest); 
                });

                return response;
            }

            var user = _mapper.Map<Model.Entities.User>(userRequest);

            await _context.Users.AddAsync(user);

            var save = await SaveChanges();

            if (!save.Item1)
            {
                response.AddErrors(save.Item2, HttpStatusCode.InternalServerError);
                return response;
            }

            var userResponse = _mapper.Map<UserResponse>(user);

            response.AddItems(new[] { userResponse });

            return response;
        }

        public async Task<BaseResponse<UserResponse>> GetAll()
        {
            var response = new BaseResponse<UserResponse>();
            var users = await _context.Users.ToArrayAsync();
            response.AddItems(_mapper.Map<UserResponse[]>(users));

            return response;
        }

        public async Task<BaseResponse<UserResponse>> GetByHashId(string hashId)
        {
            var response = new BaseResponse<UserResponse>();
            var user = _context.Users.FirstOrDefault(x => x.HashId == hashId);
            response.AddItems(user == null ? null : new[] { _mapper.Map<UserResponse>(user) });

            return await Task.FromResult(response);

        }

        public async Task<BaseResponse<UserResponse>> UpdateByHashId(string hashId, UserRequest userRequest)
        {
            var response = new BaseResponse<UserResponse>();

            var user = _context.Users.FirstOrDefault(x => x.HashId == hashId);

            if (user == null)
            {
                response.AddItems(null);
                return response;
            }

            var results = _validator.Validate(userRequest);

            if (!results.IsValid)
            {
                results.Errors.ToList().ForEach(x => 
                { 
                    response.AddErrors(x.ErrorMessage, HttpStatusCode.BadRequest); 
                });

                return response;
            }

            user.Name = userRequest.Name;
            user.Age = userRequest.Age;
            user.Address = userRequest.Address;
            user.UpdateDate = DateTime.Now;

            var save = await SaveChanges();

            if (!save.Item1)
            {
                response.AddErrors(save.Item2, HttpStatusCode.InternalServerError);
                return response;
            }

            var userResponse = _mapper.Map<UserResponse>(user);

            response.AddItems(new[] { userResponse });

            return response;
        }
    }
}
