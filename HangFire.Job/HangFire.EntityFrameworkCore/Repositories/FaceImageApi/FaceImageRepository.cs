using HangFire.Domain.FaceImage.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HangFire.EntityFrameworkCore.Repositories.FaceImageApi
{
    public class FaceImageRepository : EfCoreRepository<FaceImageDBContext, Domain.FaceImage.FaceImageApi, long>, IFaceImageRepository
    {
        public FaceImageRepository(IDbContextProvider<FaceImageDBContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// Get All Employee
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.FaceImage.FaceImageApi>> QueryAllEmployeeAsync()
        {
            var sql = string.Empty;
            sql = @"select ROW_NUMBER() over(order by JDate asc) as Id ,* from v_smartpark_emp
                        where LDate IS NULL and FileData IS Not Null 
                        order by JDate";
            return await DbContext.Set<Domain.FaceImage.FaceImageApi>().FromSqlRaw(sql).ToListAsync();
        }


        /// <summary>
        /// Get New Employee 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Domain.FaceImage.FaceImageApi>> QueryNewEmployeeAsync()
        {
            var sql = string.Empty;
            //sql = @"select  ROW_NUMBER() over(order by JDate asc) as Id ,* from v_smartpark_emp
            //             where DateDiff(dd,JDate,getdate()) <= 3
            //             and FileData is not null and LDate is NULL
            //             order by JDate";
            sql = @"select ROW_NUMBER() over(order by JDate asc) as Id ,* from v_smartpark_emp where EmpNumber in ('12200473','12200151')";
            return await DbContext.FaceImageApi.FromSqlRaw(sql).ToListAsync();
        }

        /// <summary>
        /// Get Resigned Employee
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Domain.FaceImage.FaceImageApi>> QueryResignedEmployeeAsync()
        {
            var sql = string.Empty;
            sql = @"select ROW_NUMBER() over(order by JDate asc) as Id ,* from v_smartpark_emp
                         where DateDiff(dd, LDATE, GetDate()) <= 90 and FileData is  not  NULL
                         order by JDate";
            return await DbContext.Set<Domain.FaceImage.FaceImageApi>().FromSqlRaw(sql).ToListAsync();
        }

        /// <summary>
        /// Get Updated Employee
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.FaceImage.FaceImageApi>> QueryUpdatedEmployeeAsync()
        {
            string NDate = DateTime.Now.ToString("yyyy-MM-dd");
            var sql = string.Empty;
            sql = $@"Select ROW_NUMBER() over(order by JDate asc) as Id ,* from v_smartpark_emp 
                         where CONVERT(varchar(10),UTime,120) = '{NDate}'
                         and FileData is not null and LDate is NULL
                         order by JDate";
            return await DbContext.Set<Domain.FaceImage.FaceImageApi>().FromSqlRaw(sql).ToListAsync();
        }

    }
}
