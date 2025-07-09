using Azure;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessManagementSystem.Repositories
{
    public class BasicConfigurationRepository: GenericRepository<BasicConfiguration>, IBasicConfiguration
    {
        private readonly ApplicationDBContext _db;
        public ResponseDto<BasicConfiguration> _responseDto;
        public BasicConfigurationRepository(ApplicationDBContext db) : base(db) 
        {
            _responseDto = new ResponseDto<BasicConfiguration>();
            _db = db; 
        }
        public async Task<ResponseDto<BasicConfiguration>> UpdateBasicConfigurationDetail(BasicConfiguration basicConfiguration)
        {
            try
            {
                var item =await _db.BasicConfigurations.FirstOrDefaultAsync(x => x.Id == basicConfiguration.Id);
                if (item == null)
                {
                    _db.Entry(item).CurrentValues.SetValues(basicConfiguration);
                    _db.Entry(item).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    _responseDto.Data = basicConfiguration;
                }
                else
                {
                    _responseDto.Message = "Item not found";
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Data = null;
                }
                
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = basicConfiguration;
            }
            return _responseDto;
        }
    }
}
