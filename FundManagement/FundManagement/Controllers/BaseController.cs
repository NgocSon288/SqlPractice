using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using FundManagement.Common.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace FundManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TKey> : ControllerBase
    {
        protected readonly ILogger<BaseController<TEntity, TKey>> _logger;
        protected readonly IBaseService<TEntity, TKey> _service;

        public BaseController(ILogger<BaseController<TEntity, TKey>> logger, IBaseService<TEntity, TKey> baseService)
        {
            _logger = logger;
            _service = baseService;
        }

        [HttpGet]
        public virtual ApiResult<IEnumerable<TEntity>> GetAll()
        {
            return new ApiResult<IEnumerable<TEntity>>(_service.GetAll());
        }

        [HttpGet("{id}")]
        public virtual ApiResult<TEntity> GetById(TKey id)
        {
            return new ApiResult<TEntity>(_service.GetById(id));
        }

        [HttpPost]
        public virtual ApiResult<TEntity> Insert([FromBody]TEntity entity)
        {
            try
            {
                return new ApiResult<TEntity>(_service.Insert(entity));
            }
            catch (Exception ex)
            {
                return new ApiResult<TEntity>(false, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public virtual ApiResult<bool> Delete(TKey id)
        {
            try
            {
                _service.RemoveById(id);
                return new ApiResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>(false, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public virtual ApiResult<bool> Update(TKey id, [FromForm] TEntity entity)
        {
            try
            {
                _service.Update(id, entity);
                return new ApiResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>(false, ex.Message);
            }
        }
    }
}
